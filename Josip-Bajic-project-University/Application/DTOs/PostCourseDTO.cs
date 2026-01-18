using Domain.Models;

namespace Application.DTOs
{
    public class PostCourseDTO
    {
        public string Name { get; set; }
        public string CourseCode { get; set; }
        public string? Description { get; set; }

        public Course ToModel()
        {
            return new Course
            {
                Name = Name,
                CourseCode = CourseCode,
                Description = Description
            };
        }
    }
}
