using Aspirra.HabitService.Common;
using Aspirra.HabitService.Data;
using Aspirra.HabitService.Dtos;
using Aspirra.HabitService.Models;
using Aspirra.HabitService.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;
using HabitServiceClass = Aspirra.HabitService.Services.HabitService;

namespace HabitService.Tests;

public class HabitServiceTests
{
    private static (HabitServiceClass Service, HabitDbContext Context) CreateService()
    {
        var options = new DbContextOptionsBuilder<HabitDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString("N"))
            .Options;
        var context = new HabitDbContext(options);
        var repository = new HabitRepository(context);
        return (new HabitServiceClass(repository), context);
    }

    private static Task<Habit> CreateDailyHabitAsync(HabitServiceClass service, string studentId = "student-1")
    {
        return CreateHabitAsync(service, studentId, "Morning Run", "Daily");
    }

    private static async Task<Habit> CreateHabitAsync(
        HabitServiceClass service,
        string studentId,
        string name,
        string frequency)
    {
        var result = await service.CreateHabitAsync(studentId, new CreateHabitRequest
        {
            StudentId = studentId,
            Name = name,
            Frequency = frequency
        });

        Assert.True(result.Success);
        Assert.NotNull(result.Value);
        return result.Value!;
    }

    private static DateOnly CreatedDate(Habit habit) =>
        DateOnly.FromDateTime(habit.CreatedAt.ToUniversalTime());

    [Fact]
    public async Task CreateHabit_WithValidData_SetsZeroStreak()
    {
        var (service, _) = CreateService();

        var result = await service.CreateHabitAsync("student-1", new CreateHabitRequest
        {
            StudentId = "student-1",
            Name = "Morning Run",
            Frequency = "Daily"
        });

        Assert.True(result.Success);
        Assert.NotNull(result.Value);
        Assert.Equal(0, result.Value!.CurrentStreak);
        Assert.True(result.Value.IsActive);
        Assert.False(string.IsNullOrWhiteSpace(result.Value.HabitId));
        Assert.Equal(HabitFrequency.Daily, result.Value.Frequency);
        Assert.Equal(DateTimeKind.Utc, result.Value.CreatedAt.Kind);
    }

    [Fact]
    public async Task CreateHabit_MissingName_ReturnsRequiredFieldError()
    {
        var (service, _) = CreateService();

        var result = await service.CreateHabitAsync("student-1", new CreateHabitRequest
        {
            StudentId = "student-1",
            Name = "   ",
            Frequency = "Daily"
        });

        Assert.False(result.Success);
        Assert.Equal(HabitErrorCodes.RequiredField, result.Code);
        Assert.Equal("Name is required.", result.Message);
    }

    [Fact]
    public async Task CreateHabit_InvalidFrequency_ReturnsInvalidFrequencyError()
    {
        var (service, _) = CreateService();

        var result = await service.CreateHabitAsync("student-1", new CreateHabitRequest
        {
            StudentId = "student-1",
            Name = "Morning Run",
            Frequency = "Monthly"
        });

        Assert.False(result.Success);
        Assert.Equal(HabitErrorCodes.InvalidFrequency, result.Code);
        Assert.Equal("Invalid habit frequency", result.Message);
    }

    [Fact]
    public async Task CompleteHabit_FirstTime_SetsStreakToOne()
    {
        var (service, context) = CreateService();
        var habit = await CreateDailyHabitAsync(service);
        var date = CreatedDate(habit);

        var result = await service.CompleteHabitAsync(habit.HabitId, date);

        Assert.True(result.Success);
        Assert.Equal(1, result.Value!.CurrentStreak);
        var log = Assert.Single(context.HabitLogs);
        Assert.Equal(date, log.LogDate);
        Assert.True(log.Completed);
        Assert.NotNull(log.CompletedAt);
    }

    [Fact]
    public async Task CompleteHabit_ConsecutiveDays_IncrementsStreak()
    {
        var (service, _) = CreateService();
        var habit = await CreateDailyHabitAsync(service);
        var date = CreatedDate(habit);

        await service.CompleteHabitAsync(habit.HabitId, date);
        await service.CompleteHabitAsync(habit.HabitId, date.AddDays(1));
        var result = await service.CompleteHabitAsync(habit.HabitId, date.AddDays(2));

        Assert.True(result.Success);
        Assert.Equal(3, result.Value!.CurrentStreak);
    }

    [Fact]
    public async Task CompleteHabit_MissedScheduledDay_ResetsStreakToOne()
    {
        var (service, _) = CreateService();
        var habit = await CreateDailyHabitAsync(service);
        var date = CreatedDate(habit);

        await service.CompleteHabitAsync(habit.HabitId, date);
        var result = await service.CompleteHabitAsync(habit.HabitId, date.AddDays(2));

        Assert.True(result.Success);
        Assert.Equal(1, result.Value!.CurrentStreak);
    }

    [Fact]
    public async Task CompleteHabit_SameDayTwice_DoesNotIncrementStreak()
    {
        var (service, context) = CreateService();
        var habit = await CreateDailyHabitAsync(service);
        var date = CreatedDate(habit);

        var first = await service.CompleteHabitAsync(habit.HabitId, date);
        var second = await service.CompleteHabitAsync(habit.HabitId, date);

        Assert.True(first.Success);
        Assert.True(second.Success);
        Assert.Equal(1, first.Value!.CurrentStreak);
        Assert.Equal(1, second.Value!.CurrentStreak);
        Assert.Single(context.HabitLogs);
    }

    [Fact]
    public async Task GetStreaks_ShowsDoneToday()
    {
        var (service, _) = CreateService();
        var completedHabit = await CreateHabitAsync(service, "student-1", "Read", "Daily");
        var pendingHabit = await CreateHabitAsync(service, "student-1", "Meditate", "Daily");
        var today = CreatedDate(completedHabit);

        await service.CompleteHabitAsync(completedHabit.HabitId, today);

        var streaks = await service.GetStreaksAsync("student-1", today);

        Assert.Equal(2, streaks.Count);
        var completed = Assert.Single(streaks, streak => streak.HabitId == completedHabit.HabitId);
        var pending = Assert.Single(streaks, streak => streak.HabitId == pendingHabit.HabitId);
        Assert.True(completed.DoneToday);
        Assert.Equal(1, completed.CurrentStreak);
        Assert.False(pending.DoneToday);
    }

    [Fact]
    public async Task LinkHabitToGoal_StoresGoalId()
    {
        var (service, context) = CreateService();
        var habit = await CreateDailyHabitAsync(service);

        var result = await service.LinkToGoalAsync(habit.HabitId, "goal-42");

        Assert.True(result.Success);
        Assert.Equal("goal-42", result.Value!.GoalId);
        var stored = await context.Habits.SingleAsync(item => item.HabitId == habit.HabitId);
        Assert.Equal("goal-42", stored.GoalId);
    }

    [Fact]
    public async Task LinkHabitToGoal_HabitNotFound_ReturnsNotFound()
    {
        var (service, _) = CreateService();

        var result = await service.LinkToGoalAsync("missing-habit", "goal-42");

        Assert.False(result.Success);
        Assert.Equal(HabitErrorCodes.NotFound, result.Code);
        Assert.Equal("Habit not found", result.Message);
    }
}
