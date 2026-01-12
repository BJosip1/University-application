using Application.DTOs;
using Application.Interfaces;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Domain.Models;

namespace Application.Services
{
    public class CourseService :ICourseService
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IUnitOfWork _unitOfWork;
        public CourseService(ICourseRepository courseRepository, IUnitOfWork unitOfWork)
        {
            _courseRepository = courseRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Course>> GetAllCourses()
        {
            return await _courseRepository.GetAllCourses();
        }

        public async Task<Course?> GetCourseById(int id)
        {
            return await _courseRepository.GetCourseById(id);
        }

        public async Task<string> AddCourse(PostCourseDTO course)
        {
            var courseEntity = course.ToModel();
            var validationResult = ValidateCourse(courseEntity);

            if (validationResult != null)
                return validationResult;

            _courseRepository.CreateCourse(courseEntity);
            await _unitOfWork.SaveChangesAsync();

            return $"Course successfully created with Id: {courseEntity.Id}";
        }

        public async Task<string> UpdateCourse(PutCourseDTO course)
        {
            var courseEntity = course.ToModel();
            var validationResult = await UpdateValidation(courseEntity);

            if (validationResult != null)
                return validationResult;

            await _courseRepository.UpdateCourse(courseEntity);
            await _unitOfWork.SaveChangesAsync();

            return $"Successfully updated course with Id: {courseEntity.Id}";
        }

        public async Task<string> DeleteCourse(int id)
        {
            await _courseRepository.DeleteCourse(id);
            await _unitOfWork.SaveChangesAsync();

            return $"Successfully deleted course with Id: {id}";
        }

        private string ValidateCourse(Course course)
        {
            if (string.IsNullOrWhiteSpace(course.Name))
                return "Course name is required.";

            if (string.IsNullOrWhiteSpace(course.CourseCode))
                return "Course code is required.";

            return null;
        }

        private async Task<string> UpdateValidation(Course course)
        {
            try
            {
                var existingCourse = await _courseRepository.GetCourseById(course.Id);
                if (existingCourse == null)
                    return "Course not found.";
            }
            catch (KeyNotFoundException)
            {
                return "Course not found.";
            }

            return ValidateCourse(course);
        }
    }
}
