using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class GetStudentDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Major { get; set; }
        public DateTime? BirthDate { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public bool IsActive { get; set; }
        //public int? UserId { get; set; }        
        public int? ProgramTypeId { get; set; }
        //public User User { get; set; }
        public GetProgramTypeDTO ProgramType { get; set; }
        public List<GetCourseDTO> EnrolledCourses { get; set; } = new List<GetCourseDTO>();
    }
}
