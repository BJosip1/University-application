using Application.DTOs;
using Application.Interfaces;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Repositories;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

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
            var validationResult = await CreateOrUpdateValidation(professorEntity);
            if (validationResult != null)
                return validationResult;

            _professorRepository.CreateProfessor(professorEntity);
            await _unitOfWork.SaveChangesAsync();
            return $"Professor successfully created with Id: {professorEntity.Id}";
        }

        public async Task<string> UpdateProfessor(PutProfessorDTO professor)
        {
            var professorEntity = professor.ToModel();
            var validationResult = await CreateOrUpdateValidation(professorEntity);
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
           
                await _courseRepository.AssignCourseToProfessor(
                    assignmentDto.ProfessorsId,
                    assignmentDto.TeachingCoursesId);

                await _unitOfWork.SaveChangesAsync();

                return $"Course {assignmentDto.TeachingCoursesId} successfully assigned to professor {assignmentDto.ProfessorsId}";
            
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

        private async Task<string> CreateOrUpdateValidation(Professor professor)
        {
            if (string.IsNullOrWhiteSpace(professor.Name))
                return "Professor name is required.";
            if (professor.Name.Length > 100)
                return "Professor name cannot exceed 100 characters.";
            if (string.IsNullOrWhiteSpace(professor.Surname))
                return "Professor surname is required.";
            if (professor.Surname.Length > 100)
                return "Professor surname cannot exceed 100 characters.";
            if (string.IsNullOrWhiteSpace(professor.Email))
                return "Professor email is required.";
            if (professor.Email.Length > 150)
                return "Professor email cannot exceed 150 characters.";
            if (!await IsEmailUnique(professor.Email, professor.Id))
                return "Professor email must be unique.";
            if (string.IsNullOrWhiteSpace(professor.Department))
                return "Professor department is required.";
            if (!string.IsNullOrWhiteSpace(professor.Department) && professor.Department.Length > 100)
                return "Department name cannot exceed 100 characters.";
            if (!professor.HireDate.HasValue)
                return "Professor hire date is required.";
            if (professor.HireDate.Value > DateTime.Now)
                return "Hire date cannot be in the future.";

            return null;
        }

        private async Task<bool> IsEmailUnique(string email, int? professorId = null)
        {
            var professors = await _professorRepository.GetProfessors();
            var existingProfessor = professors.FirstOrDefault(p => p.Email == email && (!professorId.HasValue || p.Id != professorId));
            return existingProfessor == null;
        }
    }
}
