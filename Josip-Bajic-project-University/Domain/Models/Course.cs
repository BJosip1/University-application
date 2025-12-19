using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CourseCode { get; set; }
        public string? Description { get; set; }

        #region Reverse Navigation Properties
        public ICollection<Student> Students { get; set; }  = new List<Student>();
        public ICollection<Professor> Professors { get; set; } = new List<Professor>();
        #endregion
    }
}
