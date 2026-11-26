using Aspirra.AcademicService.Exceptions;
using Aspirra.AcademicService.Interfaces;
using Aspirra.AcademicService.Models;
using Aspirra.AcademicService.Services;
using Moq;

namespace Aspirra.AcademicService.Tests;

public class GoalMoqTests
{
    private static DateOnly Today => DateOnly.FromDateTime(DateTime.UtcNow);
    private static DateOnly FutureDate => Today.AddDays(30);

    private static Goal GoalWithStatus(GoalStatus status)
    {
        return new Goal
        {
            GoalId = "goal-1",
            StudentId = "student-1",
            Title = "Prepare for final exams",
            TargetDate = FutureDate,
            Priority = "High",
            Status = status,
            CompletionDate = status == GoalStatus.Completed ? Today.AddDays(-2) : null
        };
    }

    [Fact]
    public async Task TC_02_4_03_CompleteGoal_FromActive_PersistsExactlyOnce()
    {
        var goal = GoalWithStatus(GoalStatus.Active);
        var repository = new Mock<IGoalRepository>(MockBehavior.Strict);
        repository
            .Setup(r => r.GetByIdAsync("goal-1", It.IsAny<CancellationToken>()))
            .ReturnsAsync(goal);
        repository
            .Setup(r => r.UpdateAsync(goal, It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        var service = new GoalService(repository.Object);
        var result = await service.CompleteGoalAsync(goal.GoalId);

        Assert.Equal(GoalStatus.Completed, result.Status);
        Assert.Equal(Today, result.CompletionDate);
        repository.Verify(r => r.UpdateAsync(goal, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task TC_02_4_04_CompleteGoal_FromArchived_DoesNotPersist()
    {
        var goal = GoalWithStatus(GoalStatus.Archived);
        var repository = new Mock<IGoalRepository>(MockBehavior.Strict);
        repository
            .Setup(r => r.GetByIdAsync("goal-1", It.IsAny<CancellationToken>()))
            .ReturnsAsync(goal);

        var service = new GoalService(repository.Object);

        var exception = await Assert.ThrowsAsync<GoalValidationException>(
            () => service.CompleteGoalAsync(goal.GoalId));

        Assert.Equal("INVALID_TRANSITION", exception.Code);
        repository.Verify(r => r.UpdateAsync(It.IsAny<Goal>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task TC_02_5_01_RestoreGoal_FromArchived_ClearsCompletionDateAndPersists()
    {
        var goal = GoalWithStatus(GoalStatus.Completed);
        goal.Status = GoalStatus.Archived;
        var repository = new Mock<IGoalRepository>(MockBehavior.Strict);
        repository
            .Setup(r => r.GetByIdAsync("goal-1", It.IsAny<CancellationToken>()))
            .ReturnsAsync(goal);
        repository
            .Setup(r => r.UpdateAsync(goal, It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        var service = new GoalService(repository.Object);
        var result = await service.RestoreGoalAsync(goal.GoalId);

        Assert.Equal(GoalStatus.Active, result.Status);
        Assert.Null(result.CompletionDate);
        repository.Verify(r => r.UpdateAsync(goal, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task TC_02_1_04_CreateGoal_WithoutStudentId_DoesNotPersist()
    {
        var repository = new Mock<IGoalRepository>(MockBehavior.Strict);
        var service = new GoalService(repository.Object);

        await Assert.ThrowsAsync<GoalValidationException>(
            () => service.CreateGoalAsync(string.Empty, new Goal()));

        repository.Verify(r => r.AddAsync(It.IsAny<Goal>(), It.IsAny<CancellationToken>()), Times.Never);
    }
}
