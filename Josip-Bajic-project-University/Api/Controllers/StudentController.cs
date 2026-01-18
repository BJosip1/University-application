using Application.DTOs;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Repositories;
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
        private readonly IProgramTypeRepository _programTypeRepository;

        public StudentController(IStudentService studentService, IProgramTypeRepository programTypeRepository)
        {
            _studentService = studentService;
            _programTypeRepository = programTypeRepository;
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

        [HttpGet("program-types")]
        public async Task<IActionResult> GetProgramTypes()
        {
            var programTypes = await _programTypeRepository.GetAllProgramTypes();
            var result = programTypes.Select(pt => new GetProgramTypeDTO
            {
                Id = pt.Id,
                Title = pt.Title
            });
            return Ok(result);
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
            try
            {
                var result = await _studentService.EnrollStudentInCourse(enrollmentDto);
                return Ok(result);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
