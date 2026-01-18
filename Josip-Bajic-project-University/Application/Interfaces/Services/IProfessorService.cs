using Application.Common;
using Application.DTOs;

namespace Application.Interfaces.Services
{
    public interface IProfessorService
    {
        Task<Result<IEnumerable<GetProfessorDTO>>> GetAllProfessors();
        Task<Result<GetProfessorDTO>> GetProfessorById(int id);
        Task<Result<object>> AddProfessor(PostProfessorDTO professor);
        Task<Result<object>> UpdateProfessor(PutProfessorDTO professor);
        Task<Result<object>> DeleteProfessor(int id);
        Task<Result<object>> AssignProfessorToCourse(ProfessorCourseDTO assignmentDto);
    }
}
