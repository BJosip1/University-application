using Application.DTOs;
using Application.Common;
using Application.Interfaces;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Domain.Models;


namespace Application.Services
{
    public class ProfessorService : IProfessorService
    {
        private readonly IProfessorRepository _professorRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ProfessorService(IProfessorRepository professorRepository, ICourseRepository courseRepository, IUnitOfWork unitOfWork)
        {
            _professorRepository = professorRepository;
            _courseRepository = courseRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<IEnumerable<GetProfessorDTO>>> GetAllProfessors()
        {
            var professors = await _professorRepository.GetProfessors();
            var professorDTOs = professors.Select(p => MapToDTO(p));
            return Result<IEnumerable<GetProfessorDTO>>.Success(professorDTOs);
        }

        public async Task<Result<GetProfessorDTO>> GetProfessorById(int id)
        {
            var professor = await _professorRepository.GetProfessorById(id);
            var professorDTO = MapToDTO(professor);
            return Result<GetProfessorDTO>.Success(professorDTO);
        }

        public async Task<Result<object>> AddProfessor(PostProfessorDTO professor)
        {
            var professorEntity = professor.ToModel();
            var validationResult =  await CreateOrUpdateValidation(professorEntity);
            if (!validationResult.IsSuccess)
                return Result<object>.Failure(validationResult.ValidationItems);

            _professorRepository.CreateProfessor(professorEntity);
            await _unitOfWork.SaveChangesAsync();
            return Result<object>.Success();
        }

        public async Task<Result<object>> UpdateProfessor(PutProfessorDTO professor)
        {
            var professorEntity = professor.ToModel();
            var validationResult =await CreateOrUpdateValidation(professorEntity);
            if (!validationResult.IsSuccess)
                return Result<object>.Failure(validationResult.ValidationItems);

            await _professorRepository.UpdateProfessor(professorEntity);
            await _unitOfWork.SaveChangesAsync();

            return Result<object>.Success();
        }

        public async Task<Result<object>> DeleteProfessor(int id)
        {
            await _professorRepository.DeleteProfessor(id);
            await _unitOfWork.SaveChangesAsync();

            return Result<object>.Success();
        }

        public async Task<Result<object>> AssignProfessorToCourse(ProfessorCourseDTO assignmentDto)
        {
           
                await _professorRepository.AssignCourseToProfessor(
                    assignmentDto.ProfessorsId,
                    assignmentDto.TeachingCoursesId);

                await _unitOfWork.SaveChangesAsync();

                return Result<object>.Success();

        }

        private GetProfessorDTO MapToDTO(Professor professor)
        {
            if (professor == null)
                return null;

            return new GetProfessorDTO
            {
                Id = professor.Id,
                Name = professor.Name,
                Surname = professor.Surname,
                Email = professor.Email,
                Department = professor.Department,
                HireDate = professor.HireDate,
                //UserId = professor.UserId,
                //User = professor.User,
                TeachingCourses = professor.TeachingCourses?.Select(c => new GetCourseDTO
                {
                    Id = c.Id,
                    Name = c.Name,
                    CourseCode = c.CourseCode,
                    Description = c.Description
                }).ToList() ?? new List<GetCourseDTO>()
            };
        }

        private async Task<ValidationResult> CreateOrUpdateValidation(Professor professor)
        {
            var result = new ValidationResult();
            if (string.IsNullOrWhiteSpace(professor.Name))
                result.ValidationItems.Add("Professor name is required.");
            if (professor.Name.Length > 100)
                result.ValidationItems.Add("Professor name cannot exceed 100 characters.");

            if (string.IsNullOrWhiteSpace(professor.Surname))
                result.ValidationItems.Add("Professor surname is required.");
            if (professor.Surname.Length > 100)
                result.ValidationItems.Add("Professor surname cannot exceed 100 characters.");

            if (string.IsNullOrWhiteSpace(professor.Email))
                result.ValidationItems.Add("Professor email is required.");
            if (professor.Email.Length > 150)
                result.ValidationItems.Add("Professor email cannot exceed 150 characters.");
            if (!await IsEmailUnique(professor.Email, professor.Id))
                result.ValidationItems.Add("Professor email must be unique.");

            if (string.IsNullOrWhiteSpace(professor.Department))
                result.ValidationItems.Add("Professor department is required.");
            if (!string.IsNullOrWhiteSpace(professor.Department) && professor.Department.Length > 100)
                result.ValidationItems.Add("Department name cannot exceed 100 characters.");

            if (!professor.HireDate.HasValue)
                result.ValidationItems.Add("Professor hire date is required.");
            if (professor.HireDate.Value > DateTime.Now)
                result.ValidationItems.Add("Hire date cannot be in the future.");

            return result;
        }

        private async Task<bool> IsEmailUnique(string email, int? professorId = null)
        {
            var professors = await _professorRepository.GetProfessors();
            var existingProfessor = professors.FirstOrDefault(p => p.Email == email && (!professorId.HasValue || p.Id != professorId));
            return existingProfessor == null;
        }
    }
}
