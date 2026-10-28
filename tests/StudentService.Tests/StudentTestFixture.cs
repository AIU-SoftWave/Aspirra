using System.Globalization;
using Aspirra.StudentService.Data;
using Aspirra.StudentService.Interfaces;
using Aspirra.StudentService.Models;
using Aspirra.StudentService.Repositories;
using Microsoft.EntityFrameworkCore;

namespace StudentService.Tests;

public sealed class StudentTestFixture : IDisposable
{
    public StudentTestFixture()
    {
        var options = new DbContextOptionsBuilder<StudentDbContext>()
            .UseInMemoryDatabase($"student-tests-{Guid.NewGuid()}")
            .Options;

        Db = new StudentDbContext(options);
        Repository = new ProfileRepository(Db);
        Service = new Aspirra.StudentService.Services.StudentProfileService(Repository);
    }

    public StudentDbContext Db { get; }
    public IProfileRepository Repository { get; }
    public Aspirra.StudentService.Services.StudentProfileService Service { get; }

    public static Profile NewProfile(
        string studentId = "student-1",
        string? academicLevel = "Sophomore",
        string? major = "Computer Science",
        List<string>? interests = null,
        List<string>? skills = null,
        List<string>? careerInterests = null,
        List<TimeSlot>? availability = null)
    {
        return new Profile
        {
            StudentId = studentId,
            AcademicLevel = academicLevel,
            Major = major,
            Interests = interests ?? new List<string> { "Artificial Intelligence" },
            Skills = skills ?? new List<string> { "C#", "SQL" },
            CareerInterests = careerInterests ?? new List<string> { "Software Engineering" },
            Availability = availability ?? new List<TimeSlot>()
        };
    }

    public static TimeSlot NewSlot(
        string dayOfWeek = "Monday",
        string startTime = "09:00",
        string endTime = "11:00")
    {
        return new TimeSlot
        {
            DayOfWeek = dayOfWeek,
            StartTime = TimeOnly.Parse(startTime, CultureInfo.InvariantCulture),
            EndTime = TimeOnly.Parse(endTime, CultureInfo.InvariantCulture)
        };
    }

    public void Dispose()
    {
        Db.Dispose();
    }
}
