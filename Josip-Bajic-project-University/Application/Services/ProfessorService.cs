using Application.DTOs;
using Application.Interfaces;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Repositories;
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

        public async Task<IEnumerable<GetProfessorDTO>> GetAllProfessors()
        {
            var professors = await _professorRepository.GetProfessors();
            return professors.Select(p => MapToDTO(p)).ToList();
        }

        public async Task<GetProfessorDTO> GetProfessorById(int id)
        {
            var professor = await _professorRepository.GetProfessorById(id);
            return MapToDTO(professor);
        }

        public async Task<string> AddProfessor(PostProfessorDTO professor)
        {
            var professorEntity = professor.ToModel();
            var validationResult = await CreateValidation(professorEntity);
            if (validationResult != null)
                return validationResult;

            _professorRepository.CreateProfessor(professorEntity);
            await _unitOfWork.SaveChangesAsync();
            return $"Professor successfully created with Id: {professorEntity.Id}";
        }

        public async Task<string> UpdateProfessor(PutProfessorDTO professor)
        {
            var professorEntity = professor.ToModel();
            var validationResult = await UpdateValidation(professorEntity);
            if (validationResult != null)
                return validationResult;

            await _professorRepository.UpdateProfessor(professorEntity);
            await _unitOfWork.SaveChangesAsync();
            return $"Successfully updated professor with Id: {professorEntity.Id}";
        }

        public async Task<string> DeleteProfessor(int id)
        {
            await _professorRepository.DeleteProfessor(id);
            await _unitOfWork.SaveChangesAsync();
            return $"Successfully deleted professor with Id: {id}";
        }

        public async Task<string> AssignProfessorToCourse(ProfessorCourseDTO assignmentDto)
        {
            try
            {
                await _professorRepository.AssignProfessorToCourse(
                    assignmentDto.ProfessorsId,
                    assignmentDto.TeachingCoursesId);

                await _unitOfWork.SaveChangesAsync();

                return $"Professor {assignmentDto.ProfessorsId} successfully assigned to course {assignmentDto.TeachingCoursesId}";
            }
            catch (KeyNotFoundException ex)
            {
                return ex.Message;
            }
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


        private async Task<string> CreateValidation(Professor professor)
        {
            if (string.IsNullOrWhiteSpace(professor.Name))
                return "Professor name is required.";
            if (string.IsNullOrWhiteSpace(professor.Surname))
                return "Professor surname is required.";
            if (string.IsNullOrWhiteSpace(professor.Email))
                return "Professor email is required.";
            if (!string.IsNullOrWhiteSpace(professor.Department) && professor.Department.Length > 100)
                return "Department name cannot exceed 100 characters.";
            if (professor.HireDate.HasValue && professor.HireDate.Value > DateTime.Now)
                return "Hire date cannot be in the future.";

            return null;
        }

        private async Task<string> UpdateValidation(Professor professor)
        {
            try
            {
                var existingProfessor = await _professorRepository.GetProfessorById(professor.Id);
                if (existingProfessor == null)
                    return "Professor not found.";
            }
            catch (KeyNotFoundException)
            {
                return "Professor not found.";
            }

            if (string.IsNullOrWhiteSpace(professor.Name))
                return "Name is required.";
            if (string.IsNullOrWhiteSpace(professor.Surname))
                return "Surname is required.";
            if (string.IsNullOrWhiteSpace(professor.Email))
                return "Email is required.";
            if (!string.IsNullOrWhiteSpace(professor.Department) && professor.Department.Length > 100)
                return "Department name cannot exceed 100 characters.";
            if (professor.HireDate.HasValue && professor.HireDate.Value > DateTime.Now)
                return "Hire date cannot be in the future.";

            return null;
        }
    }
}
