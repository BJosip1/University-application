using Application.Interfaces.Repositories;
using Application.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Application.Repositories
{
    public class ProfessorRepository:IProfessorRepository
    {
        private readonly IApplicationDbContext _dbContext;

        public ProfessorRepository(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Professor>> GetProfessors()
        {
            var professors = await _dbContext.Professors
                //.Include(p => p.User)
                .Include(p => p.TeachingCourses)
                .AsNoTracking()
                .ToListAsync();
            return professors;
        }

        public async Task<Professor> GetProfessorById(int id)
        {
            var professor = await _dbContext.Professors
                //.Include(p => p.User)
                .Include(p => p.TeachingCourses)
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id);

            if (professor == null)
                throw new KeyNotFoundException();

            return professor;
        }

        public void CreateProfessor(Professor professor)
        {
            _dbContext.Professors.Add(professor);
        }

        public async Task UpdateProfessor(Professor professor)
        {
            var oldProfessor = await _dbContext.Professors.FindAsync(professor.Id)
                    ?? throw new KeyNotFoundException($"Professor {professor.Id} not found.");

            oldProfessor.Name = professor.Name;
            oldProfessor.Surname = professor.Surname;
            oldProfessor.Email = professor.Email;
            oldProfessor.Department = professor.Department;
            oldProfessor.HireDate = professor.HireDate;
        }

        public async Task DeleteProfessor(int id)
        {
            var professor = await _dbContext.Professors.FindAsync(id);
            if (professor == null)
                throw new KeyNotFoundException();

            _dbContext.Professors.Remove(professor);
        }

        public async Task AssignProfessorToCourse(int professorId, int courseId)
        {
            var professor = await _dbContext.Professors.Include(p => p.TeachingCourses).FirstOrDefaultAsync(p => p.Id == professorId);

            if (professor == null)
                throw new KeyNotFoundException($"Professor {professorId} not found.");

            var course = await _dbContext.Courses.FindAsync(courseId);
            if (course == null)
                throw new KeyNotFoundException($"Course {courseId} not found.");

            if (!professor.TeachingCourses.Any(c => c.Id == courseId))
            {
                professor.TeachingCourses.Add(course);
            }
        }
    }
}

