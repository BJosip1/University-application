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
        Task<IEnumerable<Course>> GetAllCourses();
        Task<Course?> GetCourseById(int id);
        Task<string> AddCourse(PostCourseDTO course);
        Task<string> UpdateCourse(PutCourseDTO course);
        Task<string> DeleteCourse(int id);
    }
}
