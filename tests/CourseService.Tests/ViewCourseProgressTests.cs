using Aspirra.CourseService.Models;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace CourseService.Tests;

public class ViewCourseProgressTests
{
    [Fact]
    public async Task ViewCourseProgress_SeparatesCompletedAndOutstanding()
    {
        var assignments = new[]
        {
            new AssessmentItem("a1", "Homework 1", "Completed"),
            new AssessmentItem("a2", "Quiz 1", "completed"),
            new AssessmentItem("a3", "Project", "Pending")
        };

        using var fixture = new CourseTestFixture(assignments);
        var course = CourseTestFixture.NewCourse();
        await fixture.Service.AddCourseAsync(course);

        var result = await fixture.Service.GetCourseProgressAsync(course.CourseId);

        Assert.True(result.IsSuccess);
        var progress = result.Value!;
        Assert.Equal(2, progress.Completed.Count);
        Assert.Single(progress.Outstanding);
        Assert.Contains(progress.Completed, a => a.AssignmentId == "a1");
        Assert.Contains(progress.Completed, a => a.AssignmentId == "a2");
        Assert.Contains(progress.Outstanding, a => a.AssignmentId == "a3");
    }

    [Fact]
    public async Task ViewCourseProgress_NoAssignments_ReturnsNoAssignmentsMessage()
    {
        using var fixture = new CourseTestFixture();
        var course = CourseTestFixture.NewCourse();
        await fixture.Service.AddCourseAsync(course);

        var result = await fixture.Service.GetCourseProgressAsync(course.CourseId);

        Assert.True(result.IsSuccess);
        var progress = result.Value!;
        Assert.Equal("No assignments yet", progress.Message);
        Assert.Empty(progress.Completed);
        Assert.Empty(progress.Outstanding);
    }

    [Fact]
    public async Task ViewCourseProgress_CourseNotFound_ReturnsNotFound()
    {
        using var fixture = new CourseTestFixture();

        var result = await fixture.Service.GetCourseProgressAsync("missing-course");

        Assert.False(result.IsSuccess);
        Assert.Equal(ServiceResultStatus.NotFound, result.Status);
        Assert.Equal("NOT_FOUND", result.Code);
        Assert.Equal("Course not found", result.Message);
    }
}
