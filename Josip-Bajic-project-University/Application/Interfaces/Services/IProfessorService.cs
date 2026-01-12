using Application.DTOs;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services
{
    public interface IProfessorService
    {
        Task<IEnumerable<GetProfessorDTO>> GetAllProfessors();
        Task<GetProfessorDTO> GetProfessorById(int id);
        Task<string> AddProfessor(PostProfessorDTO professor);
        Task<string> UpdateProfessor(PutProfessorDTO professor);
        Task<string> DeleteProfessor(int id);
        Task<string> AssignProfessorToCourse(ProfessorCourseDTO assignmentDto);
    }
}
