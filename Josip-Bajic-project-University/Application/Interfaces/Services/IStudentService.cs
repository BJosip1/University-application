using Application.DTOs;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services
{
    public interface IStudentService
    {
        Task<IEnumerable<GetStudentDTO>> GetAllStudents();
        Task<GetStudentDTO> GetStudentById(int id);
        Task<string> AddStudent(PostStudentDTO student);
        Task<string> UpdateStudent(PutStudentDTO student);
        Task<string> DeleteStudent(int id);
        Task<string> EnrollStudentInCourse(StudentCourseDTO enrollmentDto);
    }
}
