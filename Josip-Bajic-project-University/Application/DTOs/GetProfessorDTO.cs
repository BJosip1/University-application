namespace Application.DTOs
{
    public class GetProfessorDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Department { get; set; }
        public DateTime? HireDate { get; set; }
        //public int? UserId { get; set; }
        //public User User { get; set; }
        public List<GetCourseDTO> TeachingCourses { get; set; } = new List<GetCourseDTO>();
    }
}
