using Application.Common;
using Application.DTOs;
using Application.Interfaces;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Repositories;
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

        public async Task<Result<IEnumerable<GetCourseDTO>>> GetAllCourses()
        {
            var courses = await _courseRepository.GetAllCourses();
            var courseDTOs = courses.Select(c => MapToDTO(c));
            return Result<IEnumerable<GetCourseDTO>>.Success(courseDTOs);
        }

        public async Task<Result<GetCourseDTO>> GetCourseById(int id)
        {
            var course = await _courseRepository.GetCourseById(id);
            var courseDTO = MapToDTO(course);
            return Result<GetCourseDTO>.Success(courseDTO);
        }

        public async Task<Result<object>> AddCourse(PostCourseDTO course)
        {
            var courseEntity = course.ToModel();
            var validationResult = await CreateOrUpdateValidation(courseEntity);
            if (!validationResult.IsSuccess)
                return Result<object>.Failure(validationResult.ValidationItems);

            _courseRepository.CreateCourse(courseEntity);
            await _unitOfWork.SaveChangesAsync();
            return Result<object>.Success();
        }

        public async Task<Result<object>> UpdateCourse(PutCourseDTO course)
        {
            var courseEntity = course.ToModel();
            var validationResult = await CreateOrUpdateValidation(courseEntity);
            if (!validationResult.IsSuccess)
                return Result<object>.Failure(validationResult.ValidationItems);

            await _courseRepository.UpdateCourse(courseEntity);
            await _unitOfWork.SaveChangesAsync();
            return Result<object>.Success();
        }

        public async Task<Result<object>> DeleteCourse(int id)
        {
            await _courseRepository.DeleteCourse(id);
            await _unitOfWork.SaveChangesAsync();
            return Result<object>.Success();
        }

        private GetCourseDTO MapToDTO(Course course)
        {
            if (course == null)
                return null;

            return new GetCourseDTO
            {
                Id = course.Id,
                Name = course.Name,
                CourseCode = course.CourseCode,
                Description = course.Description
            };
        }

        private async Task<ValidationResult> CreateOrUpdateValidation(Course course)
        {
            var result = new ValidationResult();
            if (string.IsNullOrWhiteSpace(course.Name))
                result.ValidationItems.Add("Course name is required.");
            if (course.Name.Length > 100)
                result.ValidationItems.Add("Course name cannot exceed 100 characters.");

            if (string.IsNullOrWhiteSpace(course.CourseCode))
                result.ValidationItems.Add("Course code is required.");
            if (course.CourseCode.Length > 10)
                result.ValidationItems.Add("Course code cannot exceed 10 characters.");
            if (!await IsCourseCodeUnique(course.CourseCode, course.Id))
                result.ValidationItems.Add("Course code must be unique.");

            if (string.IsNullOrWhiteSpace(course.Description))
                result.ValidationItems.Add("Description is required.");
            if (!string.IsNullOrWhiteSpace(course.Description) && course.Description.Length > 1000)
                result.ValidationItems.Add("Course description cannot exceed 1000 characters.");

            return result;
        }


        private async Task<bool> IsCourseCodeUnique(string coursecode, int? courseId = null)
        {
            var courses = await _courseRepository.GetAllCourses();
            var existingCourse = courses.FirstOrDefault(p => p.CourseCode == coursecode && (!courseId.HasValue || p.Id != courseId));
            return existingCourse == null;
        }
    }
}
