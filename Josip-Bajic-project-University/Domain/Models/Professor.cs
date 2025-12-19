using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Professor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string? Department { get; set; }
        public DateTime? HireDate { get; set; }
        
        public int? UserId { get; set; }    

        #region Navigation Properties
        public User? User { get; set; }
        #endregion

        #region Reverse Navigation Properties
        public ICollection<Course> TeachingCourses { get; set; } = new List<Course>();
        #endregion
    }
}
