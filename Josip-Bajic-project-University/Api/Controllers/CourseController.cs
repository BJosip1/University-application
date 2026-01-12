using Application.DTOs;
using Application.Interfaces.Services;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _courseService;

        public CourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [HttpGet("allCourses")]
        public async Task<IActionResult> GetAllCourses()
        {
            var result = await _courseService.GetAllCourses();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCourseById(int id)
        {
            try
            {
                var result = await _courseService.GetCourseById(id);
                return Ok(result);
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Course with ID {id} not found.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostCourse([FromBody] PostCourseDTO course)
        {
            var result = await _courseService.AddCourse(course);

            if (result.Contains("required") || result.Contains("not found"))
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> EditCourse([FromBody] PutCourseDTO course)
        {
            var result = await _courseService.UpdateCourse(course);

            if (result.Contains("required") || result.Contains("not found"))
                return BadRequest(result);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            try
            {
                var result = await _courseService.DeleteCourse(id);
                return Ok(result);
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Course with ID {id} not found.");
            }
        }
    }
}
