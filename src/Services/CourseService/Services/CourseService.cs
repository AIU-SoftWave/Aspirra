using Aspirra.CourseService.Interfaces;
using Aspirra.CourseService.Models;

namespace Aspirra.CourseService.Services;

public class CourseService : ICourse
{
    private readonly ICourseRepository _repository;
    private readonly IAssessmentClient _assessmentClient;

    public CourseService(ICourseRepository repository, IAssessmentClient assessmentClient)
    {
        _repository = repository;
        _assessmentClient = assessmentClient;
    }

    public async Task<ServiceResult<Course>> AddCourseAsync(Course course)
    {
        var validation = Validate(course);
        if (validation is not null)
        {
            return validation;
        }

        if (await _repository.ExistsAsync(course.Code, course.Semester))
        {
            return ServiceResult<Course>.DuplicateCourse("Course already exists");
        }

        if (string.IsNullOrWhiteSpace(course.CourseId))
        {
            course.CourseId = Guid.NewGuid().ToString();
        }

        course.CreatedAt = DateTime.UtcNow;
        course.UpdatedAt = DateTime.UtcNow;

        await _repository.AddAsync(course);
        return ServiceResult<Course>.Success(course);
    }

    public async Task<ServiceResult<Course>> GetCourseAsync(string courseId)
    {
        var course = await _repository.GetByIdAsync(courseId);
        return course is null
            ? ServiceResult<Course>.NotFound("Course not found")
            : ServiceResult<Course>.Success(course);
    }

    public async Task<ServiceResult<Course>> UpdateCourseAsync(string courseId, Course course)
    {
        var existing = await _repository.GetByIdAsync(courseId);
        if (existing is null)
        {
            return ServiceResult<Course>.NotFound("Course not found");
        }

        var validation = Validate(course);
        if (validation is not null)
        {
            return validation;
        }

        if (await _repository.ExistsAsync(course.Code, course.Semester, courseId))
        {
            return ServiceResult<Course>.DuplicateCourse("Course already exists");
        }

        existing.StudentId = course.StudentId;
        existing.Name = course.Name;
        existing.Code = course.Code;
        existing.Instructor = course.Instructor;
        existing.Semester = course.Semester;
        existing.Credits = course.Credits;
        existing.WeeklyWorkloadHours = course.WeeklyWorkloadHours;
        existing.UpdatedAt = DateTime.UtcNow;

        await _repository.UpdateAsync(existing);
        return ServiceResult<Course>.Success(existing);
    }

    public async Task<ServiceResult<bool>> RemoveCourseAsync(string courseId)
    {
        var existing = await _repository.GetByIdAsync(courseId);
        if (existing is null)
        {
            return ServiceResult<bool>.NotFound("Course not found");
        }

        await _repository.RemoveAsync(existing);
        return ServiceResult<bool>.Success(true);
    }

    public Task<List<Course>> GetCoursesAsync(string? studentId) =>
        _repository.GetByStudentAsync(studentId);

    public async Task<ServiceResult<CourseProgress>> GetCourseProgressAsync(string courseId)
    {
        var course = await _repository.GetByIdAsync(courseId);
        if (course is null)
        {
            return ServiceResult<CourseProgress>.NotFound("Course not found");
        }

        var assignments = await _assessmentClient.GetAssignmentsAsync(courseId);

        if (assignments.Count == 0)
        {
            return ServiceResult<CourseProgress>.Success(new CourseProgress
            {
                Message = "No assignments yet",
                Completed = Array.Empty<AssessmentItem>(),
                Outstanding = Array.Empty<AssessmentItem>()
            });
        }

        return ServiceResult<CourseProgress>.Success(new CourseProgress
        {
            Completed = assignments
                .Where(a => string.Equals(a.Status, "Completed", StringComparison.OrdinalIgnoreCase))
                .ToList(),
            Outstanding = assignments
                .Where(a => !string.Equals(a.Status, "Completed", StringComparison.OrdinalIgnoreCase))
                .ToList()
        });
    }

    private static ServiceResult<Course>? Validate(Course course)
    {
        if (string.IsNullOrWhiteSpace(course.Name))
        {
            return ServiceResult<Course>.RequiredField("Name");
        }

        if (string.IsNullOrWhiteSpace(course.Code))
        {
            return ServiceResult<Course>.RequiredField("Code");
        }

        if (string.IsNullOrWhiteSpace(course.Semester))
        {
            return ServiceResult<Course>.RequiredField("Semester");
        }

        return null;
    }
}
