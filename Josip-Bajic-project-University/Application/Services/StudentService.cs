using Application.Common;
using Application.DTOs;
using Application.Interfaces;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
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

        public async Task<Result<IEnumerable<GetStudentDTO>>> GetAllStudents()
        {
            var students = await _studentRepository.GetStudents();
            var studentDTOs = students.Select(p => MapToDTO(p));
            return Result<IEnumerable<GetStudentDTO>>.Success(studentDTOs);
        }

        public async Task<Result<GetStudentDTO>> GetStudentById(int id)
        {
            var student = await _studentRepository.GetStudentById(id);
            var studentDTO = MapToDTO(student);
            return Result<GetStudentDTO>.Success(studentDTO);
        }

        public async Task<Result<object>> AddStudent(PostStudentDTO student)
        {
            var studentEntity = student.ToModel();

            var validationResult = await CreateOrUpdateValidation(studentEntity);
            if (!validationResult.IsSuccess)
                return Result<object>.Failure(validationResult.ValidationItems);

            _studentRepository.CreateStudent(studentEntity);
            await _unitOfWork.SaveChangesAsync();
            return Result<object>.Success();
        }

        public async Task<Result<object>> UpdateStudent(PutStudentDTO student)
        {
            var studentEntity = student.ToModel();

            var validationResult = await CreateOrUpdateValidation(studentEntity);
            if (!validationResult.IsSuccess)
                return Result<object>.Failure(validationResult.ValidationItems);

            await _studentRepository.UpdateStudent(studentEntity);
            await _unitOfWork.SaveChangesAsync();

            return Result<object>.Success();
        }

        public async Task<Result<object>> DeleteStudent(int id)
        {
            await _studentRepository.DeleteStudent(id);
            await _unitOfWork.SaveChangesAsync();
            return Result<object>.Success();
        }

        public async Task<Result<object>> EnrollStudentInCourse(StudentCourseDTO enrollmentDto)
        {

                await _studentRepository.AssignCourseToStudent(
                    enrollmentDto.StudentsId,
                    enrollmentDto.EnrolledCoursesId);

                await _unitOfWork.SaveChangesAsync();

                return Result<object>.Success();

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

        private async Task<ValidationResult> CreateOrUpdateValidation(Student student)
        {
            var result = new ValidationResult();
            if (string.IsNullOrWhiteSpace(student.Name))
                result.ValidationItems.Add("Student name is required.");
            if (student.Name.Length > 100)
                result.ValidationItems.Add("Student name cannot exceed 100 characters.");

            if (string.IsNullOrWhiteSpace(student.Surname))
                result.ValidationItems.Add("Student surname is required.");
            if (student.Surname.Length > 100)
                result.ValidationItems.Add("Student surname cannot exceed 100 characters.");

            if (string.IsNullOrWhiteSpace(student.Email))
                result.ValidationItems.Add("Student email is required.");
            if (student.Email.Length > 150)
                result.ValidationItems.Add("Student email cannot exceed 150 characters.");
            if (!await IsEmailUnique(student.Email, student.Id))
                result.ValidationItems.Add("Student email must be unique.");

            if (string.IsNullOrWhiteSpace(student.Major))
                result.ValidationItems.Add("Student major is required.");
            if (student.Major.Length > 100)
                result.ValidationItems.Add("Student major cannot exceed 100 characters.");

            if (!student.BirthDate.HasValue)
                result.ValidationItems.Add("Student birth date is required.");
            if (student.BirthDate.Value > DateTime.Now)
                result.ValidationItems.Add("Birth date cannot be in the future.");

            if (student.EnrollmentDate==DateTime.MinValue)
                result.ValidationItems.Add("Student enrollment date is required.");
            if (student.EnrollmentDate > DateTime.Now)
                result.ValidationItems.Add("Enrollment date cannot be in the future.");

            if (!student.ProgramTypeId.HasValue)
                result.ValidationItems.Add("Student program type is required."); 
            if (student.ProgramTypeId > 6 || student.ProgramTypeId < 1)
                result.ValidationItems.Add("Program Type ID can not be greater than 6 and less than 1.");

            return result;
        }

        private async Task<bool> IsEmailUnique(string email, int? studentId = null)
        {
            var students = await _studentRepository.GetStudents();
            var existingStudent = students.FirstOrDefault(p => p.Email == email && (!studentId.HasValue || p.Id != studentId));
            return existingStudent == null;
        }
    }
}
