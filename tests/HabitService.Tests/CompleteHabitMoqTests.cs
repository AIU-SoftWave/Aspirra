using Aspirra.HabitService.Common;
using Aspirra.HabitService.Interfaces;
using Aspirra.HabitService.Models;
using Moq;
using Xunit;
using HabitServiceClass = Aspirra.HabitService.Services.HabitService;

namespace Aspirra.HabitService.Tests;

public class CompleteHabitMoqTests
{
    private const string HabitId = "habit-1";

    private static Habit CreateHabit(DateOnly createdDate, HabitFrequency frequency = HabitFrequency.Daily) =>
        new()
        {
            HabitId = HabitId,
            StudentId = "student-1",
            Name = "Study",
            Frequency = frequency,
            CurrentStreak = 0,
            IsActive = true,
            CreatedAt = createdDate.ToDateTime(TimeOnly.MinValue, DateTimeKind.Utc),
            UpdatedAt = createdDate.ToDateTime(TimeOnly.MinValue, DateTimeKind.Utc)
        };

    private static HabitLog CompletedLog(DateOnly date) =>
        new()
        {
            LogId = $"log-{date:yyyyMMdd}",
            HabitId = HabitId,
            LogDate = date,
            Completed = true,
            CompletedAt = date.ToDateTime(TimeOnly.MinValue, DateTimeKind.Utc)
        };

    private static Mock<IHabitRepository> CreateRepository(Habit habit, List<HabitLog> logs)
    {
        var repository = new Mock<IHabitRepository>(MockBehavior.Strict);
        repository
            .Setup(r => r.GetHabitAsync(HabitId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(habit);
        repository
            .Setup(r => r.GetLogAsync(HabitId, It.IsAny<DateOnly>(), It.IsAny<CancellationToken>()))
            .Returns((string _, DateOnly date, CancellationToken _) =>
                Task.FromResult(logs.SingleOrDefault(log => log.LogDate == date)));
        repository
            .Setup(r => r.AddLogAsync(It.IsAny<HabitLog>(), It.IsAny<CancellationToken>()))
            .Callback<HabitLog, CancellationToken>((log, _) => logs.Add(log))
            .Returns(Task.CompletedTask);
        repository
            .Setup(r => r.GetLogsAsync(HabitId, It.IsAny<CancellationToken>()))
            .Returns((string _, CancellationToken _) => Task.FromResult(logs.ToList()));
        repository
            .Setup(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        return repository;
    }

    [Fact]
    public async Task CompleteHabit_FirstCompletion_AddsLogAndRecalculatesStreak()
    {
        var date = new DateOnly(2026, 11, 23);
        var habit = CreateHabit(date);
        var repository = CreateRepository(habit, []);

        var result = await new HabitServiceClass(repository.Object).CompleteHabitAsync(HabitId, date);

        Assert.True(result.Success);
        Assert.Equal(1, result.Value!.CurrentStreak);
        repository.Verify(r => r.GetHabitAsync(HabitId, It.IsAny<CancellationToken>()), Times.Once());
        repository.Verify(r => r.GetLogAsync(HabitId, date, It.IsAny<CancellationToken>()), Times.Once());
        repository.Verify(
            r => r.AddLogAsync(
                It.Is<HabitLog>(log => log.HabitId == HabitId && log.LogDate == date && log.Completed),
                It.IsAny<CancellationToken>()),
            Times.Once());
        repository.Verify(r => r.GetLogsAsync(HabitId, It.IsAny<CancellationToken>()), Times.Once());
        repository.Verify(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Exactly(2));
        repository.Verify(
            r => r.GetHabitsByStudentAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()),
            Times.Never());
    }

    [Fact]
    public async Task CompleteHabit_SameDayDuplicate_DoesNotAddAnotherLogOrIncrementStreak()
    {
        var date = new DateOnly(2026, 11, 23);
        var habit = CreateHabit(date);
        var log = CompletedLog(date);
        var repository = CreateRepository(habit, [log]);

        var result = await new HabitServiceClass(repository.Object).CompleteHabitAsync(HabitId, date);

        Assert.True(result.Success);
        Assert.Equal(1, result.Value!.CurrentStreak);
        repository.Verify(r => r.GetHabitAsync(HabitId, It.IsAny<CancellationToken>()), Times.Once());
        repository.Verify(r => r.GetLogAsync(HabitId, date, It.IsAny<CancellationToken>()), Times.Once());
        repository.Verify(r => r.AddLogAsync(It.IsAny<HabitLog>(), It.IsAny<CancellationToken>()), Times.Never());
        repository.Verify(r => r.GetLogsAsync(HabitId, It.IsAny<CancellationToken>()), Times.Once());
        repository.Verify(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Exactly(2));
    }

    [Fact]
    public async Task CompleteHabit_IncompleteLog_UpdatesExistingLogWithoutAddingAnother()
    {
        var date = new DateOnly(2026, 11, 23);
        var habit = CreateHabit(date);
        var log = new HabitLog { LogId = "log-1", HabitId = HabitId, LogDate = date };
        var repository = CreateRepository(habit, [log]);

        var result = await new HabitServiceClass(repository.Object).CompleteHabitAsync(HabitId, date);

        Assert.True(result.Success);
        Assert.True(log.Completed);
        Assert.NotNull(log.CompletedAt);
        Assert.Equal(1, result.Value!.CurrentStreak);
        repository.Verify(r => r.GetHabitAsync(HabitId, It.IsAny<CancellationToken>()), Times.Once());
        repository.Verify(r => r.GetLogAsync(HabitId, date, It.IsAny<CancellationToken>()), Times.Once());
        repository.Verify(r => r.AddLogAsync(It.IsAny<HabitLog>(), It.IsAny<CancellationToken>()), Times.Never());
        repository.Verify(r => r.GetLogsAsync(HabitId, It.IsAny<CancellationToken>()), Times.Once());
        repository.Verify(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Exactly(2));
    }

    [Fact]
    public async Task CompleteHabit_ConsecutiveDailyCompletions_CountsEachScheduledDay()
    {
        var firstDate = new DateOnly(2026, 11, 23);
        var habit = CreateHabit(firstDate);
        var logs = new List<HabitLog>();
        var repository = CreateRepository(habit, logs);
        var service = new HabitServiceClass(repository.Object);

        await service.CompleteHabitAsync(HabitId, firstDate);
        await service.CompleteHabitAsync(HabitId, firstDate.AddDays(1));
        var result = await service.CompleteHabitAsync(HabitId, firstDate.AddDays(2));

        Assert.True(result.Success);
        Assert.Equal(3, result.Value!.CurrentStreak);
        repository.Verify(r => r.GetHabitAsync(HabitId, It.IsAny<CancellationToken>()), Times.Exactly(3));
        repository.Verify(r => r.GetLogAsync(HabitId, It.IsAny<DateOnly>(), It.IsAny<CancellationToken>()), Times.Exactly(3));
        repository.Verify(r => r.AddLogAsync(It.IsAny<HabitLog>(), It.IsAny<CancellationToken>()), Times.Exactly(3));
        repository.Verify(r => r.GetLogsAsync(HabitId, It.IsAny<CancellationToken>()), Times.Exactly(3));
        repository.Verify(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Exactly(6));
    }

    [Fact]
    public async Task GetStreaks_MissedDailySchedule_ReturnsZero()
    {
        var today = new DateOnly(2026, 11, 25);
        var habit = CreateHabit(today.AddDays(-3));
        habit.CurrentStreak = 2;
        var repository = new Mock<IHabitRepository>(MockBehavior.Strict);
        repository
            .Setup(r => r.GetHabitsByStudentAsync("student-1", It.IsAny<CancellationToken>()))
            .ReturnsAsync([habit]);
        repository
            .Setup(r => r.GetLogsAsync(HabitId, It.IsAny<CancellationToken>()))
            .ReturnsAsync([CompletedLog(today.AddDays(-2))]);

        var streak = Assert.Single(await new HabitServiceClass(repository.Object).GetStreaksAsync("student-1", today));

        Assert.Equal(0, streak.CurrentStreak);
        Assert.False(streak.DoneToday);
        repository.Verify(
            r => r.GetHabitsByStudentAsync("student-1", It.IsAny<CancellationToken>()),
            Times.Once());
        repository.Verify(r => r.GetLogsAsync(HabitId, It.IsAny<CancellationToken>()), Times.Once());
        repository.Verify(r => r.GetHabitAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Never());
        repository.Verify(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never());
    }

    [Fact]
    public async Task GetStreaks_DailyScheduleStillInProgress_PreservesPreviousStreak()
    {
        var today = new DateOnly(2026, 11, 25);
        var habit = CreateHabit(today.AddDays(-3));
        habit.CurrentStreak = 0;
        var repository = new Mock<IHabitRepository>(MockBehavior.Strict);
        repository
            .Setup(r => r.GetHabitsByStudentAsync("student-1", It.IsAny<CancellationToken>()))
            .ReturnsAsync([habit]);
        repository
            .Setup(r => r.GetLogsAsync(HabitId, It.IsAny<CancellationToken>()))
            .ReturnsAsync([CompletedLog(today.AddDays(-1))]);

        var streak = Assert.Single(await new HabitServiceClass(repository.Object).GetStreaksAsync("student-1", today));

        Assert.Equal(1, streak.CurrentStreak);
        Assert.False(streak.DoneToday);
        repository.Verify(
            r => r.GetHabitsByStudentAsync("student-1", It.IsAny<CancellationToken>()),
            Times.Once());
        repository.Verify(r => r.GetLogsAsync(HabitId, It.IsAny<CancellationToken>()), Times.Once());
    }

    [Fact]
    public async Task GetStreaks_ConsecutiveWeeklySchedules_CountsScheduledDays()
    {
        var firstMonday = new DateOnly(2026, 11, 2);
        var today = firstMonday.AddDays(8);
        var habit = CreateHabit(firstMonday, HabitFrequency.Weekly);
        var repository = new Mock<IHabitRepository>(MockBehavior.Strict);
        repository
            .Setup(r => r.GetHabitsByStudentAsync("student-1", It.IsAny<CancellationToken>()))
            .ReturnsAsync([habit]);
        repository
            .Setup(r => r.GetLogsAsync(HabitId, It.IsAny<CancellationToken>()))
            .ReturnsAsync([CompletedLog(firstMonday), CompletedLog(firstMonday.AddDays(7))]);

        var streak = Assert.Single(await new HabitServiceClass(repository.Object).GetStreaksAsync("student-1", today));

        Assert.Equal(2, streak.CurrentStreak);
        repository.Verify(
            r => r.GetHabitsByStudentAsync("student-1", It.IsAny<CancellationToken>()),
            Times.Once());
        repository.Verify(r => r.GetLogsAsync(HabitId, It.IsAny<CancellationToken>()), Times.Once());
    }

    [Fact]
    public async Task GetStreaks_WeeklyScheduleStillInProgress_DoesNotResetEarly()
    {
        var firstMonday = new DateOnly(2026, 11, 2);
        var today = firstMonday.AddDays(7);
        var habit = CreateHabit(firstMonday, HabitFrequency.Weekly);
        var repository = new Mock<IHabitRepository>(MockBehavior.Strict);
        repository
            .Setup(r => r.GetHabitsByStudentAsync("student-1", It.IsAny<CancellationToken>()))
            .ReturnsAsync([habit]);
        repository
            .Setup(r => r.GetLogsAsync(HabitId, It.IsAny<CancellationToken>()))
            .ReturnsAsync([CompletedLog(firstMonday)]);

        var streak = Assert.Single(await new HabitServiceClass(repository.Object).GetStreaksAsync("student-1", today));

        Assert.Equal(1, streak.CurrentStreak);
        Assert.False(streak.DoneToday);
        repository.Verify(
            r => r.GetHabitsByStudentAsync("student-1", It.IsAny<CancellationToken>()),
            Times.Once());
        repository.Verify(r => r.GetLogsAsync(HabitId, It.IsAny<CancellationToken>()), Times.Once());
    }

    [Fact]
    public async Task GetStreaks_MissedWeeklySchedule_ReturnsZeroAfterPeriodEnds()
    {
        var firstMonday = new DateOnly(2026, 11, 2);
        var today = firstMonday.AddDays(8);
        var habit = CreateHabit(firstMonday, HabitFrequency.Weekly);
        var repository = new Mock<IHabitRepository>(MockBehavior.Strict);
        repository
            .Setup(r => r.GetHabitsByStudentAsync("student-1", It.IsAny<CancellationToken>()))
            .ReturnsAsync([habit]);
        repository
            .Setup(r => r.GetLogsAsync(HabitId, It.IsAny<CancellationToken>()))
            .ReturnsAsync([CompletedLog(firstMonday)]);

        var streak = Assert.Single(await new HabitServiceClass(repository.Object).GetStreaksAsync("student-1", today));

        Assert.Equal(0, streak.CurrentStreak);
        repository.Verify(
            r => r.GetHabitsByStudentAsync("student-1", It.IsAny<CancellationToken>()),
            Times.Once());
        repository.Verify(r => r.GetLogsAsync(HabitId, It.IsAny<CancellationToken>()), Times.Once());
    }

    [Fact]
    public async Task CompleteHabit_MissingHabit_ReturnsNotFoundWithoutRepositoryWrites()
    {
        var repository = new Mock<IHabitRepository>(MockBehavior.Strict);
        repository
            .Setup(r => r.GetHabitAsync("missing", It.IsAny<CancellationToken>()))
            .ReturnsAsync((Habit?)null);

        var result = await new HabitServiceClass(repository.Object).CompleteHabitAsync("missing");

        Assert.False(result.Success);
        Assert.Equal(HabitErrorCodes.NotFound, result.Code);
        repository.Verify(r => r.GetHabitAsync("missing", It.IsAny<CancellationToken>()), Times.Once());
        repository.Verify(r => r.GetLogAsync(It.IsAny<string>(), It.IsAny<DateOnly>(), It.IsAny<CancellationToken>()), Times.Never());
        repository.Verify(r => r.AddLogAsync(It.IsAny<HabitLog>(), It.IsAny<CancellationToken>()), Times.Never());
        repository.Verify(r => r.GetLogsAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Never());
        repository.Verify(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never());
    }

    [Fact]
    public async Task GetStreaks_UsesStudentScopedHabitQuery()
    {
        var today = new DateOnly(2026, 11, 25);
        var habit = CreateHabit(today);
        var repository = new Mock<IHabitRepository>(MockBehavior.Strict);
        repository
            .Setup(r => r.GetHabitsByStudentAsync("student-1", It.IsAny<CancellationToken>()))
            .ReturnsAsync([habit]);
        repository
            .Setup(r => r.GetLogsAsync(HabitId, It.IsAny<CancellationToken>()))
            .ReturnsAsync([]);

        var streak = Assert.Single(await new HabitServiceClass(repository.Object).GetStreaksAsync("student-1", today));

        Assert.Equal(HabitId, streak.HabitId);
        repository.Verify(
            r => r.GetHabitsByStudentAsync("student-1", It.IsAny<CancellationToken>()),
            Times.Once());
        repository.Verify(r => r.GetLogsAsync(HabitId, It.IsAny<CancellationToken>()), Times.Once());
    }
}
