using Application.DTOs;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfessorController : ControllerBase
    {
        //private ProfessorRepository _professorRepository;

        //public ProfessorController(ProfessorRepository professorRepository) 
        //{
        //    _professorRepository=professorRepository;
        //}

        [HttpGet]
        public IEnumerable<Professor> GetAllProfessors()
        {
            return Enumerable.Empty<Professor>();
        }

        [HttpGet("{id}")]
        public Professor GetProfessorById(int id)
        {
            return new Professor();
        }

        [HttpPost("new")]
        public int AddProfessor([FromBody] PostProfessorDTO newProfessor)
        {
            return 0;
        }

        [HttpPut("edit/{id}")]
        public int EditProfessor(int id, [FromBody] PostProfessorDTO professor)
        {
            return 0;
        }

        [HttpDelete("delete/{id}")]
        public int DeleteProfessor([FromRoute] int id)
        {
            return 0;
        }
    }
}
