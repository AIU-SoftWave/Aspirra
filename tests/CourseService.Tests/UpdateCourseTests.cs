using Aspirra.CourseService.Models;
using Xunit;

namespace CourseService.Tests;

public class UpdateCourseTests
{
    [Fact]
    public async Task UpdateCourse_ChangesFields()
    {
        using var fixture = new CourseTestFixture();
        var course = CourseTestFixture.NewCourse();
        await fixture.Service.AddCourseAsync(course);

        var update = CourseTestFixture.NewCourse(
            courseId: course.CourseId,
            studentId: course.StudentId,
            name: "Advanced Data Structures",
            code: "CS301",
            instructor: "Dr. Jones",
            semester: "Spring 2027",
            credits: 4,
            weeklyWorkloadHours: 10);

        var result = await fixture.Service.UpdateCourseAsync(course.CourseId, update);

        Assert.True(result.IsSuccess);
        var stored = await fixture.Repository.GetByIdAsync(course.CourseId);
        Assert.NotNull(stored);
        Assert.Equal("Advanced Data Structures", stored!.Name);
        Assert.Equal("CS301", stored.Code);
        Assert.Equal("Dr. Jones", stored.Instructor);
        Assert.Equal("Spring 2027", stored.Semester);
        Assert.Equal(4, stored.Credits);
        Assert.Equal(10, stored.WeeklyWorkloadHours);
        Assert.True(stored.UpdatedAt >= stored.CreatedAt);
    }
}
