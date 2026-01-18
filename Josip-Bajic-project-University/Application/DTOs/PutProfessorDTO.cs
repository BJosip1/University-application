using Domain.Models;

namespace Application.DTOs
{
    public class PutProfessorDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Department { get; set; }
        public DateTime HireDate { get; set; }

        public Professor ToModel()
        {
            return new Professor
            {
                Id = Id,
                Name = Name,
                Surname = Surname,
                Email = Email,
                Department = Department,
                HireDate = HireDate
            };
        }
    }
}
