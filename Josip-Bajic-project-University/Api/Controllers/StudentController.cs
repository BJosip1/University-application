using Api.Common;
using Application.Common;
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
            return this.HandleResult(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStudentById(int id)
        {
            var result = await _studentService.GetStudentById(id);
            return this.HandleResult(result);
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

            return this.HandleResult(Result<IEnumerable<GetProgramTypeDTO>>.Success(result));
        }

        [HttpPost]
        public async Task<IActionResult> PostStudent([FromBody] PostStudentDTO student)
        {
            var result = await _studentService.AddStudent(student);
            return this.HandleResult(result);
        }

        [HttpPut]
        public async Task<IActionResult> EditStudent([FromBody] PutStudentDTO student)
        {
            var result = await _studentService.UpdateStudent(student);
            return this.HandleResult(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
                var result = await _studentService.DeleteStudent(id);
                return this.HandleResult(result);
        }


        [HttpPost("enroll")]
        public async Task<IActionResult> EnrollStudent([FromBody] StudentCourseDTO enrollmentDto)
        {
                var result = await _studentService.EnrollStudentInCourse(enrollmentDto);
                return this.HandleResult(result);
        }
    }
}
