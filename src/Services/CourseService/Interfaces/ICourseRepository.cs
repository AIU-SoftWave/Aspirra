using Aspirra.CourseService.Models;

namespace Aspirra.CourseService.Interfaces;

public interface ICourseRepository
{
    Task<Course?> GetByIdAsync(string courseId);
    Task<List<Course>> GetByStudentAsync(string? studentId);
    Task<bool> ExistsAsync(string code, string semester, string? excludeCourseId = null);
    Task AddAsync(Course course);
    Task UpdateAsync(Course course);
    Task RemoveAsync(Course course);
}
