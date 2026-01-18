using Application.DTOs;
using Application.Interfaces;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Repositories;
using Domain.Models;

namespace Application.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IProgramTypeRepository _programTypeRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly IUnitOfWork _unitOfWork;

        public StudentService(IStudentRepository studentRepository, IProgramTypeRepository programTypeRepository, ICourseRepository courseRepository, IUnitOfWork unitOfWork)
        {
            _studentRepository = studentRepository;
            _programTypeRepository = programTypeRepository;
            _courseRepository = courseRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<GetStudentDTO>> GetAllStudents()
        {
            var students = await _studentRepository.GetStudents();
            return students.Select(s => MapToDTO(s)).ToList();
        }

        public async Task<GetStudentDTO> GetStudentById(int id)
        {
            var student = await _studentRepository.GetStudentById(id);
            return MapToDTO(student);
        }

        public async Task<string> AddStudent(PostStudentDTO student)
        {
            var studentEntity = student.ToModel();

            var validationResult = await CreateOrUpdateValidation(studentEntity);
            if (validationResult != null)
                return validationResult;

            _studentRepository.CreateStudent(studentEntity);
            await _unitOfWork.SaveChangesAsync();
            return $"Student successfully created with Id: {studentEntity.Id}";
        }

        public async Task<string> UpdateStudent(PutStudentDTO student)
        {
            var studentEntity = student.ToModel();

            var validationResult = await CreateOrUpdateValidation(studentEntity);
            if (validationResult != null)
                return validationResult;

            await _studentRepository.UpdateStudent(studentEntity);
            await _unitOfWork.SaveChangesAsync();
            return $"Successfully updated student with Id: {studentEntity.Id}";
        }

        public async Task<string> DeleteStudent(int id)
        {
            await _studentRepository.DeleteStudent(id);
            await _unitOfWork.SaveChangesAsync();
            return $"Successfully deleted student with Id: {id}";
        }

        public async Task<string> EnrollStudentInCourse(StudentCourseDTO enrollmentDto)
        {

                await _studentRepository.EnrollStudentInCourse(
                    enrollmentDto.StudentsId,
                    enrollmentDto.EnrolledCoursesId);

                await _unitOfWork.SaveChangesAsync();

                return $"Student {enrollmentDto.StudentsId} successfully enrolled in course {enrollmentDto.EnrolledCoursesId}";
            
        }

        private GetStudentDTO MapToDTO(Student student)
        {
            if (student == null)
                return null;

            return new GetStudentDTO
            {
                Id = student.Id,
                Name = student.Name,
                Surname = student.Surname,
                Email = student.Email,
                Major = student.Major,
                BirthDate = student.BirthDate,
                EnrollmentDate = student.EnrollmentDate,
                IsActive = student.IsActive,
                //UserId = student.UserId,
                ProgramTypeId = student.ProgramTypeId,
                //User = student.User,
                ProgramType = student.ProgramType != null ? new GetProgramTypeDTO
                {
                    Id = student.ProgramType.Id,
                    Title = student.ProgramType.Title
                } : null,
                EnrolledCourses = student.EnrolledCourses?.Select(c => new GetCourseDTO
                {
                    Id = c.Id,
                    Name = c.Name,
                    CourseCode = c.CourseCode,
                    Description = c.Description
                }).ToList() ?? new List<GetCourseDTO>()
            };
        }

        private async Task<string> CreateOrUpdateValidation(Student student)
        {
            if (string.IsNullOrWhiteSpace(student.Name))
                return "Name is required.";
            if (student.Name.Length > 100)
                return "Student name cannot exceed 100 characters.";
            if (string.IsNullOrWhiteSpace(student.Surname))
                return "Surname is required.";
            if (student.Surname.Length > 100)
                return "Student surname cannot exceed 100 characters.";
            if (string.IsNullOrWhiteSpace(student.Email))
                return "Email is required.";
            if (student.Email.Length > 150)
                return "Student email cannot exceed 150 characters.";
            if (!await IsEmailUnique(student.Email, student.Id))
                return "Student email must be unique.";
            if (string.IsNullOrWhiteSpace(student.Major))
                return "Major is required.";
            if (student.Major.Length > 100)
                return "Student major cannot exceed 100 characters.";

            if (!student.BirthDate.HasValue)
                return "Student birth date is required.";
            if (student.BirthDate.Value > DateTime.Now)
                return "Birth date cannot be in the future.";

            if (student.EnrollmentDate==DateTime.MinValue)
                return "Student enrollment date is required.";
            if (student.EnrollmentDate > DateTime.Now)
                return "Enrollment date cannot be in the future.";
            if (!student.ProgramTypeId.HasValue)
                return "Program Type is required.";
            if (student.ProgramTypeId > 6 || student.ProgramTypeId < 1)
                return "Program Type ID can not be greater than 6 and less than 1.";

            return null;
        }

        private async Task<bool> IsEmailUnique(string email, int? studentId = null)
        {
            var students = await _studentRepository.GetStudents();
            var existingStudent = students.FirstOrDefault(p => p.Email == email && (!studentId.HasValue || p.Id != studentId));
            return existingStudent == null;
        }
    }
}
