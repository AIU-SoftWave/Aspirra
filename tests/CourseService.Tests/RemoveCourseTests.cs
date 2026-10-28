using Aspirra.CourseService.Models;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace CourseService.Tests;

public class RemoveCourseTests
{
    [Fact]
    public async Task RemoveCourse_DeletesCourse()
    {
        using var fixture = new CourseTestFixture();
        var course = CourseTestFixture.NewCourse();
        await fixture.Service.AddCourseAsync(course);

        var result = await fixture.Service.RemoveCourseAsync(course.CourseId);

        Assert.True(result.IsSuccess);
        Assert.Null(await fixture.Repository.GetByIdAsync(course.CourseId));
        Assert.Equal(0, await fixture.Db.Courses.CountAsync());
    }
}
