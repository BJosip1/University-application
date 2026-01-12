using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
