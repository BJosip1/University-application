using Api.Common;
using Application.DTOs;
using Application.Interfaces.Services;
using Domain.Models;
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
            return this.HandleResult(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProfessorById(int id)
        {
            var result = await _professorService.GetProfessorById(id);
            return this.HandleResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> PostProfessor([FromBody] PostProfessorDTO professor)
        {
            var result = await _professorService.AddProfessor(professor);
            return this.HandleResult(result);
        }

        [HttpPut]
        public async Task<IActionResult> EditProfessor([FromBody] PutProfessorDTO professor)
        {
            var result = await _professorService.UpdateProfessor(professor);
            return this.HandleResult(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProfessor(int id)
        {
            var result = await _professorService.DeleteProfessor(id);
            return this.HandleResult(result);
        }
        [HttpPost("assign")]
        public async Task<IActionResult> AssignProfessorToCourse([FromBody] ProfessorCourseDTO assignmentDto)
        {
            var result = await _professorService.AssignProfessorToCourse(assignmentDto);
            return this.HandleResult(result);
        }
    }
}
