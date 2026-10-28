using Aspirra.CourseService.Models;
using Xunit;

namespace CourseService.Tests;

public class GetCoursesTests
{
    [Fact]
    public async Task GetCourses_FiltersByStudent()
    {
        using var fixture = new CourseTestFixture();
        await fixture.Service.AddCourseAsync(CourseTestFixture.NewCourse(studentId: "student-1", code: "CS101"));
        await fixture.Service.AddCourseAsync(CourseTestFixture.NewCourse(studentId: "student-1", code: "CS102"));
        await fixture.Service.AddCourseAsync(CourseTestFixture.NewCourse(studentId: "student-2", code: "CS103"));

        var courses = await fixture.Service.GetCoursesAsync("student-1");

        Assert.Equal(2, courses.Count);
        Assert.All(courses, course => Assert.Equal("student-1", course.StudentId));
    }
}
