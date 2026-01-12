using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
    public interface IProfessorRepository
    {
        Task<IEnumerable<Professor>> GetProfessors();
        Task<Professor> GetProfessorById(int id);
        void CreateProfessor(Professor professor);
        Task UpdateProfessor(Professor professor);
        Task DeleteProfessor(int id);
        Task AssignProfessorToCourse(int professorId, int courseId);
    }
}
