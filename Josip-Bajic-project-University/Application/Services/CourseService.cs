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
            var validationResult =await CreateOrUpdateValidation(courseEntity);

            if (validationResult != null)
                return validationResult;

            _courseRepository.CreateCourse(courseEntity);
            await _unitOfWork.SaveChangesAsync();

            return $"Course successfully created with Id: {courseEntity.Id}";
        }

        public async Task<string> UpdateCourse(PutCourseDTO course)
        {
            var courseEntity = course.ToModel();
            var validationResult = await CreateOrUpdateValidation(courseEntity);

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

        private async Task<string> CreateOrUpdateValidation(Course course)
        {
            if (string.IsNullOrWhiteSpace(course.Name))
                return "Course name is required.";
            if (course.Name.Length > 100)
                return "Course name cannot exceed 100 characters.";

            if (string.IsNullOrWhiteSpace(course.CourseCode))
                return "Course code is required.";
            if (course.CourseCode.Length > 10)
                return "Course code cannot exceed 10 characters.";
            if (string.IsNullOrWhiteSpace(course.Description))
                return "Description is required.";
            if (!string.IsNullOrWhiteSpace(course.Description) && course.Description.Length > 1000) 
                return "Course description cannot exceed 1000 characters.";

            return null;
        }

    }
}
