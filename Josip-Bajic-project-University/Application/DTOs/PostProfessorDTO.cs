using Domain.Models;

namespace Application.DTOs
{
    public class PostProfessorDTO
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Department { get; set; }
        public DateTime HireDate { get; set; }

        public Professor ToModel()
        {
            return new Professor
            {
                Name = Name,
                Surname = Surname,
                Email = Email,
                Department = Department,
                HireDate = HireDate
            };
        }
    }
}
