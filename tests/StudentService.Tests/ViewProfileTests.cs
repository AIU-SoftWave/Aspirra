using Aspirra.StudentService.Controllers;
using Aspirra.StudentService.Models;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace StudentService.Tests;

public class ViewProfileTests
{
    [Fact]
    public async Task ViewProfile_ReturnsAllFields()
    {
        using var fixture = new StudentTestFixture();
        var profile = StudentTestFixture.NewProfile(
            interests: new List<string> { "Artificial Intelligence", "Mathematics" },
            skills: new List<string> { "C#", "SQL", "Docker" },
            careerInterests: new List<string> { "Software Engineering" },
            availability: new List<TimeSlot>
            {
                StudentTestFixture.NewSlot("Monday", "09:00", "11:00")
            });

        await fixture.Service.CreateProfileAsync(profile.StudentId, profile);

        var result = await fixture.Service.GetProfileAsync("student-1");

        Assert.True(result.IsSuccess);
        var stored = result.Value!;
        Assert.Equal("student-1", stored.StudentId);
        Assert.Equal("Sophomore", stored.AcademicLevel);
        Assert.Equal("Computer Science", stored.Major);
        Assert.Equal(new[] { "Artificial Intelligence", "Mathematics" }, stored.Interests);
        Assert.Equal(new[] { "C#", "SQL", "Docker" }, stored.Skills);
        Assert.Equal(new[] { "Software Engineering" }, stored.CareerInterests);
        Assert.NotNull(stored.Availability);
        Assert.Single(stored.Availability!);
        Assert.Equal("Monday", stored.Availability![0].DayOfWeek);
        Assert.Equal(new TimeOnly(9, 0), stored.Availability[0].StartTime);
        Assert.Equal(new TimeOnly(11, 0), stored.Availability[0].EndTime);
        Assert.NotEqual(default, stored.CreatedAt);
        Assert.NotEqual(default, stored.UpdatedAt);
    }

    [Fact]
    public async Task ViewProfile_OptionalFieldMissing_ShowsNotSet()
    {
        using var fixture = new StudentTestFixture();
        var profile = new Profile
        {
            StudentId = "student-1",
            AcademicLevel = "",
            Major = "",
            Interests = new List<string>(),
            CareerInterests = new List<string>(),
            Availability = new List<TimeSlot>(),
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        await fixture.Repository.AddAsync(profile);
        await fixture.Repository.SaveChangesAsync();

        var controller = new ProfilesController(fixture.Service);
        var action = await controller.GetProfile("student-1", CancellationToken.None);

        var ok = Assert.IsType<OkObjectResult>(action);
        var response = Assert.IsType<ProfileResponse>(ok.Value);
        Assert.Equal("Not set", response.AcademicLevel);
        Assert.Equal("Not set", response.Major);
        Assert.Empty(response.Interests);
        Assert.Empty(response.CareerInterests);
        Assert.Empty(response.Availability);
    }

    [Fact]
    public async Task GetProfile_NotFound_ReturnsNotFound()
    {
        using var fixture = new StudentTestFixture();
        var controller = new ProfilesController(fixture.Service);

        var action = await controller.GetProfile("missing-student", CancellationToken.None);

        var notFound = Assert.IsType<NotFoundObjectResult>(action);
        Assert.Equal(404, notFound.StatusCode);

        var result = await fixture.Service.GetProfileAsync("missing-student");
        Assert.False(result.IsSuccess);
        Assert.Equal(ServiceResultStatus.NotFound, result.Status);
        Assert.Equal("NOT_FOUND", result.Code);
        Assert.Equal("Profile not found", result.Message);
    }

    [Fact]
    public async Task GetAvailability_ReturnsSlots()
    {
        using var fixture = new StudentTestFixture();
        await fixture.Service.CreateProfileAsync(
            "student-1",
            StudentTestFixture.NewProfile(
                availability: new List<TimeSlot>
                {
                    StudentTestFixture.NewSlot("Wednesday", "14:00", "16:00")
                }));

        var result = await fixture.Service.GetAvailabilityAsync("student-1");

        Assert.True(result.IsSuccess);
        Assert.Single(result.Value!);
        Assert.Equal("Wednesday", result.Value![0].DayOfWeek);
    }

    [Fact]
    public async Task GetAvailability_NotFound_ReturnsNotFound()
    {
        using var fixture = new StudentTestFixture();

        var result = await fixture.Service.GetAvailabilityAsync("missing-student");

        Assert.False(result.IsSuccess);
        Assert.Equal(ServiceResultStatus.NotFound, result.Status);
        Assert.Equal("NOT_FOUND", result.Code);
    }
}
