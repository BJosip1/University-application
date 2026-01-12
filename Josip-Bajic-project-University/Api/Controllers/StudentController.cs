using Application.DTOs;
using Application.Interfaces.Services;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet("allStudents")]
        public async Task<IActionResult> GetAllStudents()
        {
            var result = await _studentService.GetAllStudents();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStudentById(int id)
        {
            try
            {
                var result = await _studentService.GetStudentById(id);
                return Ok(result);
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Student with ID {id} not found.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostStudent([FromBody] PostStudentDTO student)
        {
            var result = await _studentService.AddStudent(student);
            if (result.Contains("required") || result.Contains("not found"))
                return BadRequest(result);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> EditStudent([FromBody] PutStudentDTO student)
        {
            var result = await _studentService.UpdateStudent(student);
            if (result.Contains("required") || result.Contains("not found"))
                return BadRequest(result);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            try
            {
                var result = await _studentService.DeleteStudent(id);
                return Ok(result);
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Student with ID {id} not found.");
            }
        }


        [HttpPost("enroll")]
        public async Task<IActionResult> EnrollStudent([FromBody] StudentCourseDTO enrollmentDto)
        {
            var result = await _studentService.EnrollStudentInCourse(enrollmentDto);

            if (result.Contains("not found"))
                return NotFound(result);

            return Ok(result);
        }
    }
}
