using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
    public interface IStudentRepository
    {
        Task<IEnumerable<Student?>> GetStudents();
        Task<Student> GetStudentById(int id);
        void CreateStudent(Student student);
        Task UpdateStudent(Student student);
        Task DeleteStudent(int id);
        Task EnrollStudentInCourse(int studentId, int courseId);
    }
}
