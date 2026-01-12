using Application.Interfaces;
using Application.Interfaces.Repositories;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Application.Repositories
{
    public class CourseRepository:ICourseRepository
    {
        private readonly IApplicationDbContext _dbContext;

        public CourseRepository(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Course>> GetAllCourses()
        {
            return await _dbContext.Courses.AsNoTracking().ToListAsync();
        }

        public async Task<Course> GetCourseById(int id)
        {
            var course = await _dbContext.Courses.Include(c => c.Students).Include(c => c.Professors).FirstOrDefaultAsync(c => c.Id == id);

            if (course == null)
                throw new KeyNotFoundException();

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

