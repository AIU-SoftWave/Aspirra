using Aspirra.HabitService.Common;
using Aspirra.HabitService.Interfaces;
using Aspirra.HabitService.Models;
using Aspirra.HabitService.Services;
using Moq;
using Xunit;

namespace Aspirra.HabitService.Tests;

public class HabitMoqTests
{
    private static Habit DailyHabit()
    {
        return new Habit
        {
            HabitId = "habit-1",
            StudentId = "student-1",
            Name = "Study",
            Frequency = HabitFrequency.Daily,
            CurrentStreak = 0,
            IsActive = true,
            CreatedAt = DateTime.UtcNow.AddDays(-5),
            UpdatedAt = DateTime.UtcNow.AddDays(-5)
        };
    }

    [Fact]
    public async Task TC_13_2_01_CompleteHabit_WhenNoLogExists_AddsLogAndSaves()
    {
        var habit = DailyHabit();
        var repository = new Mock<IHabitRepository>(MockBehavior.Strict);
        repository
            .Setup(r => r.GetHabitAsync("habit-1", It.IsAny<CancellationToken>()))
            .ReturnsAsync(habit);
        repository
            .Setup(r => r.GetLogAsync("habit-1", It.IsAny<DateOnly>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((HabitLog?)null);
        repository
            .Setup(r => r.AddLogAsync(It.IsAny<HabitLog>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);
        repository
            .Setup(r => r.GetLogsAsync("habit-1", It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<HabitLog>());
        repository
            .Setup(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        var service = new Services.HabitService(repository.Object);
        var result = await service.CompleteHabitAsync("habit-1");

        Assert.True(result.Success);
        repository.Verify(r => r.AddLogAsync(It.IsAny<HabitLog>(), It.IsAny<CancellationToken>()), Times.Once);
        repository.Verify(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Exactly(2));
    }

    [Fact]
    public async Task TC_13_2_02_CompleteHabit_WhenLogAlreadyCompleted_DoesNotAddLog()
    {
        var habit = DailyHabit();
        var existingLog = new HabitLog { LogId = "log-1", HabitId = "habit-1", Completed = true };
        var repository = new Mock<IHabitRepository>(MockBehavior.Strict);
        repository
            .Setup(r => r.GetHabitAsync("habit-1", It.IsAny<CancellationToken>()))
            .ReturnsAsync(habit);
        repository
            .Setup(r => r.GetLogAsync("habit-1", It.IsAny<DateOnly>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(existingLog);
        repository
            .Setup(r => r.GetLogsAsync("habit-1", It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<HabitLog> { existingLog });
        repository
            .Setup(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        var service = new Services.HabitService(repository.Object);
        var result = await service.CompleteHabitAsync("habit-1");

        Assert.True(result.Success);
        repository.Verify(r => r.AddLogAsync(It.IsAny<HabitLog>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task TC_13_2_03_CompleteHabit_WhenHabitMissing_ReturnsNotFoundAndDoesNotPersist()
    {
        var repository = new Mock<IHabitRepository>(MockBehavior.Strict);
        repository
            .Setup(r => r.GetHabitAsync("missing", It.IsAny<CancellationToken>()))
            .ReturnsAsync((Habit?)null);

        var service = new Services.HabitService(repository.Object);
        var result = await service.CompleteHabitAsync("missing");

        Assert.False(result.Success);
        Assert.Equal(HabitErrorCodes.NotFound, result.Code);
        repository.Verify(r => r.AddLogAsync(It.IsAny<HabitLog>(), It.IsAny<CancellationToken>()), Times.Never);
        repository.Verify(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }
}
