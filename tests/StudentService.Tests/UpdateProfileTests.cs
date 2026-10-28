using Aspirra.StudentService.Models;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace StudentService.Tests;

public class UpdateProfileTests
{
    [Fact]
    public async Task UpdateProfile_ChangesFields()
    {
        using var fixture = new StudentTestFixture();
        await fixture.Service.CreateProfileAsync(
            "student-1",
            StudentTestFixture.NewProfile());

        var update = new Profile
        {
            AcademicLevel = "Senior",
            Major = "Software Engineering",
            Interests = new List<string> { "Cloud Computing", "Databases" },
            CareerInterests = new List<string> { "Backend Engineer" }
        };

        var result = await fixture.Service.UpdateProfileAsync("student-1", update);

        Assert.True(result.IsSuccess);
        var stored = await fixture.Repository.GetByIdAsync("student-1");
        Assert.NotNull(stored);
        Assert.Equal("Senior", stored!.AcademicLevel);
        Assert.Equal("Software Engineering", stored.Major);
        Assert.Equal(new[] { "Cloud Computing", "Databases" }, stored.Interests);
        Assert.Equal(new[] { "Backend Engineer" }, stored.CareerInterests);
        Assert.True(stored.UpdatedAt >= stored.CreatedAt);
    }

    [Fact]
    public async Task UpdateProfile_ClearingRequiredField_KeepsOldValues()
    {
        using var fixture = new StudentTestFixture();
        var created = await fixture.Service.CreateProfileAsync(
            "student-1",
            StudentTestFixture.NewProfile());
        var originalUpdatedAt = created.Value!.UpdatedAt;

        var update = new Profile
        {
            AcademicLevel = "",
            Major = "Changed Major"
        };

        var result = await fixture.Service.UpdateProfileAsync("student-1", update);

        Assert.False(result.IsSuccess);
        Assert.Equal(ServiceResultStatus.RequiredField, result.Status);
        Assert.Equal("REQUIRED_FIELD", result.Code);
        Assert.Equal("AcademicLevel is required.", result.Message);

        var stored = await fixture.Repository.GetByIdAsync("student-1");
        Assert.NotNull(stored);
        Assert.Equal("Sophomore", stored!.AcademicLevel);
        Assert.Equal("Computer Science", stored.Major);
        Assert.Equal(originalUpdatedAt, stored.UpdatedAt);
    }

    [Fact]
    public async Task UpdateProfile_ChangesAvailability()
    {
        using var fixture = new StudentTestFixture();
        var created = await fixture.Service.CreateProfileAsync(
            "student-1",
            StudentTestFixture.NewProfile(
                availability: new List<TimeSlot>
                {
                    StudentTestFixture.NewSlot("Monday", "09:00", "10:00")
                }));
        var oldSlotId = created.Value!.Availability!.Single().TimeSlotId;

        var update = new Profile
        {
            AcademicLevel = "Junior",
            Major = "Computer Science",
            Availability = new List<TimeSlot>
            {
                StudentTestFixture.NewSlot("Tuesday", "13:00", "15:00"),
                StudentTestFixture.NewSlot("Thursday", "08:00", "09:30")
            }
        };

        var result = await fixture.Service.UpdateProfileAsync("student-1", update);

        Assert.True(result.IsSuccess);
        var stored = await fixture.Repository.GetByIdAsync("student-1");
        Assert.NotNull(stored);
        Assert.NotNull(stored!.Availability);
        Assert.Equal(2, stored.Availability!.Count);
        Assert.DoesNotContain(stored.Availability, slot => slot.TimeSlotId == oldSlotId);
        Assert.All(stored.Availability, slot => Assert.Equal("student-1", slot.StudentId));
        Assert.Contains(stored.Availability, slot => slot.DayOfWeek == "Tuesday");
        Assert.Contains(stored.Availability, slot => slot.DayOfWeek == "Thursday");
        Assert.Equal(2, await fixture.Db.TimeSlots.CountAsync());
    }

    [Fact]
    public async Task UpdateProfile_NotFound_ReturnsNotFound()
    {
        using var fixture = new StudentTestFixture();

        var result = await fixture.Service.UpdateProfileAsync(
            "missing-student",
            StudentTestFixture.NewProfile());

        Assert.False(result.IsSuccess);
        Assert.Equal(ServiceResultStatus.NotFound, result.Status);
        Assert.Equal("NOT_FOUND", result.Code);
        Assert.Equal("Profile not found", result.Message);
    }
}
