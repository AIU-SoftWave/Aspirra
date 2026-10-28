using Aspirra.CourseService.Models;

namespace Aspirra.CourseService.Interfaces;

public interface ICourse
{
    Task<ServiceResult<Course>> AddCourseAsync(Course course);
    Task<ServiceResult<Course>> GetCourseAsync(string courseId);
    Task<ServiceResult<Course>> UpdateCourseAsync(string courseId, Course course);
    Task<ServiceResult<bool>> RemoveCourseAsync(string courseId);
    Task<List<Course>> GetCoursesAsync(string? studentId);
    Task<ServiceResult<CourseProgress>> GetCourseProgressAsync(string courseId);
}
