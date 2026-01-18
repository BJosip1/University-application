using Application.Common;
using Application.DTOs;

namespace Application.Interfaces.Services
{
    public interface IStudentService
    {
        Task<Result<IEnumerable<GetStudentDTO>>> GetAllStudents();
        Task<Result<GetStudentDTO>> GetStudentById(int id);
        Task<Result<object>> AddStudent(PostStudentDTO student);
        Task<Result<object>> UpdateStudent(PutStudentDTO student);
        Task<Result<object>> DeleteStudent(int id);
        Task<Result<object>> EnrollStudentInCourse(StudentCourseDTO enrollmentDto);
    }
}
