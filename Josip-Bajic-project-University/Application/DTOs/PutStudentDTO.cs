using Domain.Models;

namespace Application.DTOs
{
    public class PutStudentDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Major { get; set; }
        public DateTime? BirthDate { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public bool IsActive { get; set; }
        public int? ProgramTypeId { get; set; }

        public Student ToModel()
        {
            return new Student
            {
                Id = Id,
                Name = Name,
                Surname = Surname,
                Email = Email,
                Major = Major,
                BirthDate = BirthDate,
                EnrollmentDate = EnrollmentDate,
                IsActive = IsActive,
                ProgramTypeId = ProgramTypeId
            };
        }
    }
}
