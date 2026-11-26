using System.Text.Json;
using Aspirra.AcademicService.Controllers;
using Aspirra.AcademicService.Data;
using Aspirra.AcademicService.Exceptions;
using Aspirra.AcademicService.Interfaces;
using Aspirra.AcademicService.Models;
using Aspirra.AcademicService.Repositories;
using Aspirra.AcademicService.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Aspirra.AcademicService.Tests;

public class GoalStateTransitionTests
{
    private static DateOnly Today => DateOnly.FromDateTime(DateTime.UtcNow);
    private static DateOnly FutureDate => Today.AddDays(30);

    private static AcademicDbContext CreateContext()
    {
        var options = new DbContextOptionsBuilder<AcademicDbContext>()
            .UseInMemoryDatabase($"aspirra-goal-state-{Guid.NewGuid():N}")
            .Options;

        return new AcademicDbContext(options);
    }

    private static Goal GoalWithStatus(GoalStatus status, string goalId = "goal-1")
    {
        return new Goal
        {
            GoalId = goalId,
            StudentId = "student-1",
            Title = "Prepare for final exams",
            Description = "Review every course",
            TargetDate = FutureDate,
            Priority = "High",
            Status = status,
            CompletionDate = status == GoalStatus.Completed ? Today.AddDays(-2) : null,
            CreatedAt = DateTime.UtcNow.AddDays(-10),
            UpdatedAt = DateTime.UtcNow.AddDays(-10)
        };
    }

    private static async Task<Goal> SeedGoalAsync(AcademicDbContext context, GoalStatus status)
    {
        var goal = GoalWithStatus(status);
        context.Goals.Add(goal);
        await context.SaveChangesAsync();
        return goal;
    }

    [Fact]
    public async Task TC_02_4_07_CompleteGoal_FromActive_ReturnsCompleted()
    {
        using var context = CreateContext();
        var service = new GoalService(new GoalRepository(context));
        var goal = await SeedGoalAsync(context, GoalStatus.Active);

        var completed = await service.CompleteGoalAsync(goal.GoalId);

        Assert.Equal(GoalStatus.Completed, completed.Status);
        Assert.Equal(Today, completed.CompletionDate);
    }

    [Fact]
    public async Task TC_02_4_08_CompleteGoal_FromCompleted_IsIdempotent()
    {
        using var context = CreateContext();
        var service = new GoalService(new GoalRepository(context));
        var goal = await SeedGoalAsync(context, GoalStatus.Completed);
        var originalCompletionDate = goal.CompletionDate;

        var completed = await service.CompleteGoalAsync(goal.GoalId);

        Assert.Equal(GoalStatus.Completed, completed.Status);
        Assert.Equal(originalCompletionDate, completed.CompletionDate);
    }

    [Fact]
    public async Task TC_02_5_03_ArchiveGoal_FromActive_ReturnsArchived()
    {
        using var context = CreateContext();
        var service = new GoalService(new GoalRepository(context));
        var goal = await SeedGoalAsync(context, GoalStatus.Active);

        var archived = await service.ArchiveGoalAsync(goal.GoalId);

        Assert.Equal(GoalStatus.Archived, archived.Status);
    }

    [Fact]
    public async Task TC_02_5_04_ArchiveGoal_FromCompleted_ReturnsArchived()
    {
        using var context = CreateContext();
        var service = new GoalService(new GoalRepository(context));
        var goal = await SeedGoalAsync(context, GoalStatus.Completed);

        var archived = await service.ArchiveGoalAsync(goal.GoalId);

        Assert.Equal(GoalStatus.Archived, archived.Status);
        Assert.NotNull(archived.CompletionDate);
    }

    [Fact]
    public async Task TC_02_5_05_RestoreGoal_FromArchived_ReturnsActive()
    {
        using var context = CreateContext();
        var service = new GoalService(new GoalRepository(context));
        var goal = await SeedGoalAsync(context, GoalStatus.Archived);

        var restored = await service.RestoreGoalAsync(goal.GoalId);

        Assert.Equal(GoalStatus.Active, restored.Status);
        Assert.Null(restored.CompletionDate);
    }

    [Fact]
    public async Task TC_02_5_06_RestoreGoal_AfterArchivedCompletedGoal_ClearsCompletionDate()
    {
        using var context = CreateContext();
        var service = new GoalService(new GoalRepository(context));
        var goal = await SeedGoalAsync(context, GoalStatus.Completed);

        await service.ArchiveGoalAsync(goal.GoalId);
        var restored = await service.RestoreGoalAsync(goal.GoalId);

        Assert.Equal(GoalStatus.Active, restored.Status);
        Assert.Null(restored.CompletionDate);
    }

    [Fact]
    public async Task TC_02_4_09_CompleteGoal_FromArchived_ThrowsInvalidTransitionAndDoesNotUpdate()
    {
        var repository = new CountingGoalRepository { Goal = GoalWithStatus(GoalStatus.Archived) };
        var service = new GoalService(repository);

        var exception = await Assert.ThrowsAsync<GoalValidationException>(
            () => service.CompleteGoalAsync("goal-1"));

        Assert.Equal("INVALID_TRANSITION", exception.Code);
        Assert.Equal(0, repository.UpdateCalls);
    }

    [Fact]
    public async Task TC_02_5_07_RestoreGoal_FromActive_ThrowsInvalidTransitionAndDoesNotUpdate()
    {
        var repository = new CountingGoalRepository { Goal = GoalWithStatus(GoalStatus.Active) };
        var service = new GoalService(repository);

        var exception = await Assert.ThrowsAsync<GoalValidationException>(
            () => service.RestoreGoalAsync("goal-1"));

        Assert.Equal("INVALID_TRANSITION", exception.Code);
        Assert.Equal(0, repository.UpdateCalls);
    }

    [Fact]
    public async Task TC_02_5_08_RestoreGoal_FromCompleted_ThrowsInvalidTransitionAndDoesNotUpdate()
    {
        var repository = new CountingGoalRepository { Goal = GoalWithStatus(GoalStatus.Completed) };
        var service = new GoalService(repository);

        var exception = await Assert.ThrowsAsync<GoalValidationException>(
            () => service.RestoreGoalAsync("goal-1"));

        Assert.Equal("INVALID_TRANSITION", exception.Code);
        Assert.Equal(0, repository.UpdateCalls);
    }

    [Fact]
    public async Task TC_02_4_10_CompleteGoal_MissingGoal_ThrowsNotFound()
    {
        var repository = new CountingGoalRepository { Goal = null };
        var service = new GoalService(repository);

        await Assert.ThrowsAsync<GoalNotFoundException>(() => service.CompleteGoalAsync("missing"));
    }

    [Fact]
    public async Task TC_02_5_09_ArchiveGoal_MissingGoal_ThrowsNotFound()
    {
        var repository = new CountingGoalRepository { Goal = null };
        var service = new GoalService(repository);

        await Assert.ThrowsAsync<GoalNotFoundException>(() => service.ArchiveGoalAsync("missing"));
    }

    [Fact]
    public async Task TC_02_5_10_RestoreGoal_MissingGoal_ThrowsNotFound()
    {
        var repository = new CountingGoalRepository { Goal = null };
        var service = new GoalService(repository);

        await Assert.ThrowsAsync<GoalNotFoundException>(() => service.RestoreGoalAsync("missing"));
    }

    [Fact]
    public async Task TC_02_4_11_CompleteGoal_InvalidTransition_ReturnsBadRequestWithCode()
    {
        var repository = new CountingGoalRepository { Goal = GoalWithStatus(GoalStatus.Archived) };
        var controller = new GoalsController(new GoalService(repository));

        var result = await controller.CompleteGoal("goal-1", CancellationToken.None);

        var badRequest = Assert.IsType<BadRequestObjectResult>(result);
        using var document = JsonDocument.Parse(JsonSerializer.Serialize(badRequest.Value));
        Assert.Equal("INVALID_TRANSITION", document.RootElement.GetProperty("code").GetString());
        Assert.Equal(0, repository.UpdateCalls);
    }

    [Fact]
    public async Task TC_02_5_11_RestoreGoal_InvalidTransition_ReturnsBadRequestWithCode()
    {
        var repository = new CountingGoalRepository { Goal = GoalWithStatus(GoalStatus.Active) };
        var controller = new GoalsController(new GoalService(repository));

        var result = await controller.RestoreGoal("goal-1", CancellationToken.None);

        var badRequest = Assert.IsType<BadRequestObjectResult>(result);
        using var document = JsonDocument.Parse(JsonSerializer.Serialize(badRequest.Value));
        Assert.Equal("INVALID_TRANSITION", document.RootElement.GetProperty("code").GetString());
        Assert.Equal(0, repository.UpdateCalls);
    }

    [Fact]
    public async Task TC_02_4_12_CompleteGoal_MissingGoal_ReturnsNotFound()
    {
        var repository = new CountingGoalRepository { Goal = null };
        var controller = new GoalsController(new GoalService(repository));

        var result = await controller.CompleteGoal("missing", CancellationToken.None);

        var notFound = Assert.IsType<NotFoundObjectResult>(result);
        using var document = JsonDocument.Parse(JsonSerializer.Serialize(notFound.Value));
        Assert.Equal("NOT_FOUND", document.RootElement.GetProperty("code").GetString());
    }

    private sealed class CountingGoalRepository : IGoalRepository
    {
        public Goal? Goal { get; init; }
        public int UpdateCalls { get; private set; }

        public Task<Goal> AddAsync(Goal goal, CancellationToken cancellationToken = default)
            => throw new NotSupportedException();

        public Task<Goal?> GetByIdAsync(string goalId, CancellationToken cancellationToken = default)
            => Task.FromResult(Goal);

        public Task Update(Goal goal, CancellationToken cancellationToken = default)
            => throw new NotSupportedException();

        public Task Delete(Goal goal, CancellationToken cancellationToken = default)
            => throw new NotSupportedException();

        public Task UpdateAsync(Goal goal, CancellationToken cancellationToken = default)
        {
            UpdateCalls++;
            return Task.CompletedTask;
        }

        public Task DeleteAsync(Goal goal, CancellationToken cancellationToken = default)
            => throw new NotSupportedException();

        public IQueryable<Goal> Query() => Array.Empty<Goal>().AsQueryable();
    }
}
