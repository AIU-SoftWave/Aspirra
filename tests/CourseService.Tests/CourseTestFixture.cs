using Aspirra.CourseService.Data;
using Aspirra.CourseService.Interfaces;
using Aspirra.CourseService.Models;
using Aspirra.CourseService.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CourseService.Tests;

public sealed class CourseTestFixture : IDisposable
{
    public CourseTestFixture(IEnumerable<AssessmentItem>? assignments = null)
    {
        var options = new DbContextOptionsBuilder<CourseDbContext>()
            .UseInMemoryDatabase($"course-tests-{Guid.NewGuid()}")
            .Options;

        Db = new CourseDbContext(options);
        Repository = new CourseRepository(Db);
        AssessmentClient = new FakeAssessmentClient(assignments?.ToArray() ?? Array.Empty<AssessmentItem>());
        Service = new Aspirra.CourseService.Services.CourseService(Repository, AssessmentClient);
    }

    public CourseDbContext Db { get; }
    public ICourseRepository Repository { get; }
    public FakeAssessmentClient AssessmentClient { get; }
    public Aspirra.CourseService.Services.CourseService Service { get; }

    public static Course NewCourse(
        string? courseId = null,
        string studentId = "student-1",
        string name = "Data Structures",
        string code = "CS201",
        string instructor = "Dr. Smith",
        string semester = "Fall 2026",
        int credits = 3,
        int weeklyWorkloadHours = 6) => new()
        {
            CourseId = courseId ?? Guid.NewGuid().ToString(),
            StudentId = studentId,
            Name = name,
            Code = code,
            Instructor = instructor,
            Semester = semester,
            Credits = credits,
            WeeklyWorkloadHours = weeklyWorkloadHours
        };

    public void Dispose()
    {
        Db.Dispose();
    }
}

public sealed class FakeAssessmentClient : IAssessmentClient
{
    private readonly IReadOnlyList<AssessmentItem> _assignments;

    public FakeAssessmentClient(IReadOnlyList<AssessmentItem> assignments)
    {
        _assignments = assignments;
    }

    public Task<IReadOnlyList<AssessmentItem>> GetAssignmentsAsync(string courseId) =>
        Task.FromResult(_assignments);
}
