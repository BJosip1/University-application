using Application.DTOs;
using Domain.Models;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfessorController : ControllerBase
    {
        private readonly IProfessorService _professorService;

        public ProfessorController(IProfessorService professorService)
        {
            _professorService = professorService;
        }

        [HttpGet("allProfessors")]
        public async Task<IActionResult> GetAllProfessors()
        {
            var result = await _professorService.GetAllProfessors();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProfessorById(int id)
        {
            try
            {
                var result = await _professorService.GetProfessorById(id);
                return Ok(result);
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Professor with ID {id} not found.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostProfessor([FromBody] PostProfessorDTO professor)
        {
            var result = await _professorService.AddProfessor(professor);
            if (result.Contains("required") || result.Contains("not found"))
                return BadRequest(result);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> EditProfessor([FromBody] PutProfessorDTO professor)
        {
            var result = await _professorService.UpdateProfessor(professor);
            if (result.Contains("required") || result.Contains("not found"))
                return BadRequest(result);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProfessor(int id)
        {
            try
            {
                var result = await _professorService.DeleteProfessor(id);
                return Ok(result);
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Professor with ID {id} not found.");
            }
        }
        [HttpPost("assign")]
        public async Task<IActionResult> AssignProfessorToCourse([FromBody] ProfessorCourseDTO assignmentDto)
        {
            try
            {
                var result = await _professorService.AssignProfessorToCourse(assignmentDto);
                return Ok(result);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
