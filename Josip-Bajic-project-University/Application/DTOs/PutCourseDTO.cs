using Domain.Models;

namespace Application.DTOs
{
    public class PutCourseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CourseCode { get; set; }
        public string? Description { get; set; }

        public Course ToModel()
        {
            return new Course
            {
                Id = Id,
                Name = Name,
                CourseCode = CourseCode,
                Description = Description
            };
        }
    }
}
