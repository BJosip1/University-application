using Application.Interfaces;
using Application.Interfaces.Repositories;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Application.Repositories
{
    public class StudentRepository: IStudentRepository
    {
        private readonly IApplicationDbContext _dbContext;

        public StudentRepository(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<IEnumerable<Student>> GetStudents()
        {
            var student = await _dbContext.Students
                //.Include(s => s.User)
                .Include(s=>s.ProgramType)
                .Include(s => s.EnrolledCourses)
                .AsNoTracking()
                .ToListAsync();

            return student;
        }

        public async Task<Student> GetStudentById(int id)
        {
            var student = await _dbContext.Students
                //.Include(s => s.User)
                .Include(s => s.ProgramType)
                .Include(s => s.EnrolledCourses)
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Id == id); 

            if (student == null)
                throw new KeyNotFoundException();

            return student;
        }

        public void CreateStudent(Student student)
        {
            _dbContext.Students.Add(student);
        }

        public async Task UpdateStudent(Student student)
        {
            var oldStudent = await _dbContext.Students.FindAsync(student.Id)
                    ?? throw new KeyNotFoundException($"Student {student.Id} not found.");

            oldStudent.Name = student.Name;
            oldStudent.Surname = student.Surname;
            oldStudent.Email = student.Email;
            oldStudent.Major = student.Major;
            oldStudent.BirthDate = student.BirthDate;
            oldStudent.EnrollmentDate = student.EnrollmentDate;
            oldStudent.IsActive = student.IsActive;
            oldStudent.ProgramTypeId = student.ProgramTypeId;
        }

        public async Task DeleteStudent(int id)
        {
            var student = await _dbContext.Students.FindAsync(id);

            if (student == null)
                throw new KeyNotFoundException();

            _dbContext.Students.Remove(student);
        }


        public async Task EnrollStudentInCourse(int studentId, int courseId)
        {
            var student = await _dbContext.Students.Include(s => s.EnrolledCourses).FirstOrDefaultAsync(s => s.Id == studentId);

            if (student == null)
                throw new KeyNotFoundException($"Student {studentId} not found.");

            var course = await _dbContext.Courses.FindAsync(courseId);
            if (course == null)
                throw new KeyNotFoundException($"Course {courseId} not found.");
            if (!student.EnrolledCourses.Any(c => c.Id == courseId))
            {
                student.EnrolledCourses.Add(course);
            }
        }
    }
}
