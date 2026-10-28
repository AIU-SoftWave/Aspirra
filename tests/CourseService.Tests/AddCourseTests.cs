using Aspirra.CourseService.Models;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace CourseService.Tests;

public class AddCourseTests
{
    [Fact]
    public async Task AddCourse_WithValidData_StoresCreditsAndWorkload()
    {
        using var fixture = new CourseTestFixture();
        var course = CourseTestFixture.NewCourse(credits: 4, weeklyWorkloadHours: 9);

        var result = await fixture.Service.AddCourseAsync(course);

        Assert.True(result.IsSuccess);
        var stored = await fixture.Repository.GetByIdAsync(course.CourseId);
        Assert.NotNull(stored);
        Assert.Equal(4, stored!.Credits);
        Assert.Equal(9, stored.WeeklyWorkloadHours);
        Assert.NotEqual(default, stored.CreatedAt);
        Assert.NotEqual(default, stored.UpdatedAt);
    }

    [Fact]
    public async Task AddCourse_MissingRequiredField_ReturnsRequiredFieldError()
    {
        using var fixture = new CourseTestFixture();
        var course = CourseTestFixture.NewCourse(name: "  ");

        var result = await fixture.Service.AddCourseAsync(course);

        Assert.False(result.IsSuccess);
        Assert.Equal(ServiceResultStatus.RequiredField, result.Status);
        Assert.Equal("REQUIRED_FIELD", result.Code);
        Assert.Equal("Name is required.", result.Message);
        Assert.Empty(await fixture.Db.Courses.ToListAsync());
    }

    [Fact]
    public async Task AddCourse_DuplicateCodeAndSemester_ReturnsDuplicateError()
    {
        using var fixture = new CourseTestFixture();
        await fixture.Service.AddCourseAsync(CourseTestFixture.NewCourse());

        var result = await fixture.Service.AddCourseAsync(
            CourseTestFixture.NewCourse(name: "Algorithms", code: "CS201", semester: "Fall 2026"));

        Assert.False(result.IsSuccess);
        Assert.Equal(ServiceResultStatus.DuplicateCourse, result.Status);
        Assert.Equal("DUPLICATE_COURSE", result.Code);
        Assert.Equal("Course already exists", result.Message);
        Assert.Single(await fixture.Db.Courses.ToListAsync());
    }

    [Fact]
    public async Task AddCourse_SameCodeDifferentSemester_Allowed()
    {
        using var fixture = new CourseTestFixture();
        await fixture.Service.AddCourseAsync(CourseTestFixture.NewCourse(semester: "Fall 2026"));

        var result = await fixture.Service.AddCourseAsync(
            CourseTestFixture.NewCourse(name: "Data Structures II", semester: "Spring 2027"));

        Assert.True(result.IsSuccess);
        Assert.Equal(2, await fixture.Db.Courses.CountAsync());
    }
}
