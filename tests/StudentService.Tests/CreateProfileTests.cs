using Aspirra.StudentService.Models;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace StudentService.Tests;

public class CreateProfileTests
{
    [Fact]
    public async Task CreateProfile_WithRequiredFields_CreatesProfile()
    {
        using var fixture = new StudentTestFixture();
        var profile = StudentTestFixture.NewProfile();

        var result = await fixture.Service.CreateProfileAsync(profile.StudentId, profile);

        Assert.True(result.IsSuccess);
        Assert.Equal(ServiceResultStatus.Success, result.Status);

        var stored = await fixture.Repository.GetByIdAsync(profile.StudentId);
        Assert.NotNull(stored);
        Assert.Equal("student-1", stored!.StudentId);
        Assert.Equal("Sophomore", stored.AcademicLevel);
        Assert.Equal("Computer Science", stored.Major);
        Assert.NotEqual(default, stored.CreatedAt);
        Assert.Equal(DateTimeKind.Utc, stored.CreatedAt.Kind);
        Assert.Equal(stored.CreatedAt, stored.UpdatedAt);
    }

    [Fact]
    public async Task CreateProfile_MissingAcademicLevel_ReturnsRequiredFieldError()
    {
        using var fixture = new StudentTestFixture();
        var profile = StudentTestFixture.NewProfile(academicLevel: null);

        var result = await fixture.Service.CreateProfileAsync(profile.StudentId, profile);

        Assert.False(result.IsSuccess);
        Assert.Equal(ServiceResultStatus.RequiredField, result.Status);
        Assert.Equal("REQUIRED_FIELD", result.Code);
        Assert.Equal("AcademicLevel is required.", result.Message);
        Assert.Empty(await fixture.Db.Profiles.ToListAsync());
    }

    [Fact]
    public async Task CreateProfile_EndBeforeStart_ReturnsInvalidTimeSlot()
    {
        using var fixture = new StudentTestFixture();
        var profile = StudentTestFixture.NewProfile(
            availability: new List<TimeSlot>
            {
                StudentTestFixture.NewSlot(startTime: "10:00", endTime: "09:00")
            });

        var result = await fixture.Service.CreateProfileAsync(profile.StudentId, profile);

        Assert.False(result.IsSuccess);
        Assert.Equal(ServiceResultStatus.InvalidTimeSlot, result.Status);
        Assert.Equal("INVALID_TIME_SLOT", result.Code);
        Assert.Equal("Invalid time slot", result.Message);
        Assert.Empty(await fixture.Db.Profiles.ToListAsync());
    }

    [Fact]
    public async Task CreateProfile_Duplicate_ReturnsDuplicateError()
    {
        using var fixture = new StudentTestFixture();
        await fixture.Service.CreateProfileAsync(
            "student-1",
            StudentTestFixture.NewProfile());

        var result = await fixture.Service.CreateProfileAsync(
            "student-1",
            StudentTestFixture.NewProfile(major: "Software Engineering"));

        Assert.False(result.IsSuccess);
        Assert.Equal(ServiceResultStatus.DuplicateProfile, result.Status);
        Assert.Equal("DUPLICATE_PROFILE", result.Code);
        Assert.Equal("Profile already exists", result.Message);
        Assert.Single(await fixture.Db.Profiles.ToListAsync());
    }

    [Fact]
    public async Task CreateProfile_MissingMajor_ReturnsRequiredFieldError()
    {
        using var fixture = new StudentTestFixture();
        var profile = StudentTestFixture.NewProfile(major: "");

        var result = await fixture.Service.CreateProfileAsync(profile.StudentId, profile);

        Assert.False(result.IsSuccess);
        Assert.Equal(ServiceResultStatus.RequiredField, result.Status);
        Assert.Equal("Major is required.", result.Message);
        Assert.Empty(await fixture.Db.Profiles.ToListAsync());
    }
}
