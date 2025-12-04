using Application.DTOs;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UniversityController : ControllerBase
    {
        //private StudentRepository _studentRepository;
        //private ProfessorRepository _profesorRepository;

        //public UniversityController(StudentRepository studentRepository, ProfessorRepository profesorRepository) 
        //{
        //    _studentRepository=studentRepository;
        //    _professorRepository=profesorRepository;
        //}

        [HttpGet("students")]
        public IEnumerable<Student> GetAllStudents() 
        {
            return Enumerable.Empty<Student>();
        }

        [HttpGet("students/{id}")]
        public Student GetStudentById(int id) 
        {
            return new Student();
        }

        [HttpPost("students/new")]
        public int AddStudent([FromBody] PostStudentDTO newStudent)
        {
            return 0;
        }

        [HttpPut("students/edit/{id}")]
        public int EditStudent(int id, [FromBody] PostStudentDTO student)
        {
            return 0;
        }

        [HttpDelete("students/delete/{id}")]
        public int DeleteStudent([FromRoute] int id)
        {
            return 0; 
        }

        [HttpGet("professors")]
        public IEnumerable<Professor> GetAllProfessors()
        {
            return Enumerable.Empty<Professor>();
        }

        [HttpGet("professors/{id}")]
        public Professor GetProfessorById(int id)
        {
            return new Professor();
        }

        [HttpPost("professors/new")]
        public int AddProfessor([FromBody] PostProfessorDTO newProfessor)
        {
            return 0;
        }

        [HttpPut("professors/edit/{id}")]
        public int EditProfessor(int id, [FromBody] PostProfessorDTO professor)
        {
            return 0;
        }

        [HttpDelete("professors/delete/{id}")]
        public int DeleteProfessor([FromRoute] int id)
        {
            return 0;
        }
    }
}
