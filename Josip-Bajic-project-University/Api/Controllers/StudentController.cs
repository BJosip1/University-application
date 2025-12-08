using Application.DTOs;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        //private StudentRepository _studentRepository;
       
        //public StudentController(StudentRepository studentRepository) 
        //{
        //    _studentRepository=studentRepository;
        //}

        [HttpGet]
        public IEnumerable<Student> GetAllStudents() 
        {
            return Enumerable.Empty<Student>();
        }

        [HttpGet("{id}")]
        public Student GetStudentById(int id) 
        {
            return new Student();
        }

        [HttpPost("new")]
        public int AddStudent([FromBody] PostStudentDTO newStudent)
        {
            return 0;
        }

        [HttpPut("edit/{id}")]
        public int EditStudent(int id, [FromBody] PostStudentDTO student)
        {
            return 0;
        }

        [HttpDelete("delete/{id}")]
        public int DeleteStudent([FromRoute] int id)
        {
            return 0; 
        }

    }
}
