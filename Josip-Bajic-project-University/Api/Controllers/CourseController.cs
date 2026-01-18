using Api.Common;
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
            return this.HandleResult(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCourseById(int id)
        {
          
                var result = await _courseService.GetCourseById(id);
                return this.HandleResult(result);
           
        }

        [HttpPost]
        public async Task<IActionResult> PostCourse([FromBody] PostCourseDTO course)
        {
            var result = await _courseService.AddCourse(course);
            return this.HandleResult(result);
        }

        [HttpPut]
        public async Task<IActionResult> EditCourse([FromBody] PutCourseDTO course)
        {
            var result = await _courseService.UpdateCourse(course);
            return this.HandleResult(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
                var result = await _courseService.DeleteCourse(id);
                return this.HandleResult(result);
        }
    }
}
