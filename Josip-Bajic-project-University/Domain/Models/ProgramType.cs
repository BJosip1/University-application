using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class ProgramType
    {
        public int Id { get; set; }
        public string Title { get; set; }

        #region Reverse Navigation Properties
        public ICollection<Student> Students{get;set;}=new List<Student>();
        #endregion
    }
}
