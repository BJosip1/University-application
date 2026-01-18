using Application.Common;
using Application.DTOs;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services
{
    public interface ICourseService
    {
        Task<Result<IEnumerable<GetCourseDTO>>> GetAllCourses();
        Task<Result<GetCourseDTO>> GetCourseById(int id);
        Task<Result<object>> AddCourse(PostCourseDTO course);
        Task<Result<object>> UpdateCourse(PutCourseDTO course);
        Task<Result<object>> DeleteCourse(int id);
    }
}
