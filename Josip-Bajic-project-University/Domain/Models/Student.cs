using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Major { get; set; }
        public DateTime? BirthDate { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public bool IsActive { get; set; }
        
        public int? UserId { get; set; }
        public int? ProgramTypeId { get; set; }

        #region Navigation Properties
        public User? User { get; set; }
        public ProgramType? ProgramType { get; set; }
        #endregion

        #region Reverse Navigation Properties
        public ICollection<Course> EnrolledCourses { get; set; } = new List<Course>();
        #endregion
    }
}
