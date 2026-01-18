using Application.Interfaces;
using Application.Interfaces.Repositories;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.Repositories
{
    public class CourseRepository:ICourseRepository
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly ILogger<CourseRepository> _logger;

        public CourseRepository(IApplicationDbContext dbContext, ILogger<CourseRepository> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }
        
        public async Task<IEnumerable<Course>> GetAllCourses()
        {
            var course= await _dbContext.Courses.AsNoTracking().ToListAsync();
            return course;
        }

        public async Task<Course> GetCourseById(int id)
        {
            var course = await _dbContext.Courses.Include(c => c.Students).Include(c => c.Professors).AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);

            if (course == null)
                throw new KeyNotFoundException($"Cannot find course with Id: {id}");

            return course;
        }

        public void CreateCourse(Course course)
        {
            _dbContext.Courses.Add(course);
        }

        public async Task UpdateCourse(Course course)
        {
            var oldCourse = await _dbContext.Courses.FindAsync(course.Id)
                ?? throw new KeyNotFoundException($"Course {course.Id} not found.");

            oldCourse.Name = course.Name;
            oldCourse.CourseCode = course.CourseCode;
            oldCourse.Description = course.Description;
        }

        public async Task DeleteCourse(int id)
        {
            var course = await _dbContext.Courses.FindAsync(id);

            if (course == null)
                throw new KeyNotFoundException();

            _dbContext.Courses.Remove(course);
        }
    }
}

