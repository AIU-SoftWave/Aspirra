using System.Text.Json;
using Aspirra.AcademicService.Controllers;
using Aspirra.AcademicService.Data;
using Aspirra.AcademicService.Exceptions;
using Aspirra.AcademicService.Models;
using Aspirra.AcademicService.Repositories;
using Aspirra.AcademicService.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Aspirra.AcademicService.Tests;

public class GoalServiceTests
{
    private static DateOnly Today => DateOnly.FromDateTime(DateTime.UtcNow);
    private static DateOnly FutureDate => Today.AddDays(30);
    private static DateOnly PastDate => Today.AddDays(-1);

    private static AcademicDbContext CreateContext()
    {
        var options = new DbContextOptionsBuilder<AcademicDbContext>()
            .UseInMemoryDatabase($"aspirra-academic-{Guid.NewGuid():N}")
            .Options;

        return new AcademicDbContext(options);
    }

    private static GoalService CreateService(AcademicDbContext context)
    {
        return new GoalService(new GoalRepository(context));
    }

    private static Goal ValidGoal(string studentId = "student-1")
    {
        return new Goal
        {
            StudentId = studentId,
            Title = "Prepare for final exams",
            Description = "Review every course",
            TargetDate = FutureDate,
            Priority = "High"
        };
    }

    [Fact]
    public async Task CreateGoal_WithValidData_SetsStatusActive()
    {
        using var context = CreateContext();
        var service = CreateService(context);

        var goal = await service.CreateGoalAsync("student-1", ValidGoal());

        Assert.Equal(GoalStatus.Active, goal.Status);
        Assert.False(string.IsNullOrWhiteSpace(goal.GoalId));
        Assert.Equal("student-1", goal.StudentId);
        Assert.Null(goal.CompletionDate);
        Assert.NotEqual(default, goal.CreatedAt);
        Assert.Equal(goal.CreatedAt, goal.UpdatedAt);
        Assert.Equal(GoalStatus.Active, (await context.Goals.FindAsync(goal.GoalId))!.Status);
    }

    [Fact]
    public async Task CreateGoal_MissingTitle_ReturnsRequiredFieldError()
    {
        using var context = CreateContext();
        var service = CreateService(context);
        var data = ValidGoal();
        data.Title = "   ";

        var exception = await Assert.ThrowsAsync<GoalValidationException>(() => service.CreateGoalAsync("student-1", data));

        Assert.Equal("REQUIRED_FIELD", exception.Code);
        Assert.Equal("Title is required.", exception.Message);
    }

    [Fact]
    public async Task CreateGoal_MissingPriority_ReturnsRequiredFieldError()
    {
        using var context = CreateContext();
        var service = CreateService(context);
        var data = ValidGoal();
        data.Priority = string.Empty;

        var exception = await Assert.ThrowsAsync<GoalValidationException>(() => service.CreateGoalAsync("student-1", data));

        Assert.Equal("REQUIRED_FIELD", exception.Code);
        Assert.Equal("Priority is required.", exception.Message);
    }

    [Fact]
    public async Task CreateGoal_PastTargetDate_ReturnsInvalidDateError()
    {
        using var context = CreateContext();
        var service = CreateService(context);
        var data = ValidGoal();
        data.TargetDate = PastDate;

        var exception = await Assert.ThrowsAsync<GoalValidationException>(() => service.CreateGoalAsync("student-1", data));

        Assert.Equal("INVALID_DATE", exception.Code);
        Assert.Equal("Target date must be in the future", exception.Message);
    }

    [Fact]
    public async Task UpdateGoal_ChangesFields()
    {
        using var context = CreateContext();
        var service = CreateService(context);
        var created = await service.CreateGoalAsync("student-1", ValidGoal());
        var createdAt = created.CreatedAt;
        var newTargetDate = FutureDate.AddDays(14);

        var updated = await service.UpdateGoalAsync(created.GoalId, new Goal
        {
            StudentId = "someone-else",
            Title = "Finish the capstone project",
            Description = "Deliver the final version",
            Priority = "Low",
            TargetDate = newTargetDate
        });

        Assert.Equal(created.GoalId, updated.GoalId);
        Assert.Equal("student-1", updated.StudentId);
        Assert.Equal("Finish the capstone project", updated.Title);
        Assert.Equal("Deliver the final version", updated.Description);
        Assert.Equal("Low", updated.Priority);
        Assert.Equal(newTargetDate, updated.TargetDate);
        Assert.Equal(createdAt, updated.CreatedAt);
        Assert.True(updated.UpdatedAt >= createdAt);
    }

    [Fact]
    public async Task UpdateGoal_PastTargetDate_KeepsOldValues()
    {
        using var context = CreateContext();
        var service = CreateService(context);
        var created = await service.CreateGoalAsync("student-1", ValidGoal());
        var oldTargetDate = created.TargetDate;
        var oldUpdatedAt = created.UpdatedAt;

        var exception = await Assert.ThrowsAsync<GoalValidationException>(() => service.UpdateGoalAsync(created.GoalId, new Goal
        {
            Title = "Changed title",
            Description = "Changed description",
            Priority = "Low",
            TargetDate = PastDate
        }));

        Assert.Equal("INVALID_DATE", exception.Code);
        context.ChangeTracker.Clear();
        var stored = await new GoalRepository(context).GetByIdAsync(created.GoalId);
        Assert.NotNull(stored);
        Assert.Equal("Prepare for final exams", stored!.Title);
        Assert.Equal("Review every course", stored.Description);
        Assert.Equal("High", stored.Priority);
        Assert.Equal(oldTargetDate, stored.TargetDate);
        Assert.Equal(oldUpdatedAt, stored.UpdatedAt);
    }

    [Fact]
    public async Task UpdateGoal_MissingTitle_KeepsOldValues()
    {
        using var context = CreateContext();
        var service = CreateService(context);
        var created = await service.CreateGoalAsync("student-1", ValidGoal());

        var exception = await Assert.ThrowsAsync<GoalValidationException>(() => service.UpdateGoalAsync(created.GoalId, new Goal
        {
            Title = string.Empty,
            Description = "Changed description",
            Priority = "Low",
            TargetDate = FutureDate.AddDays(7)
        }));

        Assert.Equal("REQUIRED_FIELD", exception.Code);
        context.ChangeTracker.Clear();
        var stored = await new GoalRepository(context).GetByIdAsync(created.GoalId);
        Assert.NotNull(stored);
        Assert.Equal("Prepare for final exams", stored!.Title);
        Assert.Equal("High", stored.Priority);
        Assert.Equal(FutureDate, stored.TargetDate);
    }

    [Fact]
    public async Task CompleteGoal_SetsCompletedAndCompletionDate()
    {
        using var context = CreateContext();
        var service = CreateService(context);
        var created = await service.CreateGoalAsync("student-1", ValidGoal());

        var completed = await service.CompleteGoalAsync(created.GoalId);

        Assert.Equal(GoalStatus.Completed, completed.Status);
        Assert.Equal(Today, completed.CompletionDate);
        Assert.True(completed.UpdatedAt >= completed.CreatedAt);
    }

    [Fact]
    public async Task CompleteGoal_Twice_KeepsFirstCompletionDate()
    {
        using var context = CreateContext();
        var service = CreateService(context);
        var created = await service.CreateGoalAsync("student-1", ValidGoal());
        var first = await service.CompleteGoalAsync(created.GoalId);
        var originalCompletionDate = Today.AddDays(-3);
        first.CompletionDate = originalCompletionDate;
        await context.SaveChangesAsync();

        var second = await service.CompleteGoalAsync(created.GoalId);

        Assert.Equal(GoalStatus.Completed, second.Status);
        Assert.Equal(originalCompletionDate, second.CompletionDate);
    }

    [Fact]
    public async Task ArchiveAndRestoreGoal_ChangesStatus()
    {
        using var context = CreateContext();
        var service = CreateService(context);
        var created = await service.CreateGoalAsync("student-1", ValidGoal());

        var archived = await service.ArchiveGoalAsync(created.GoalId);
        Assert.Equal(GoalStatus.Archived, archived.Status);

        var restored = await service.RestoreGoalAsync(created.GoalId);
        Assert.Equal(GoalStatus.Active, restored.Status);
    }

    [Fact]
    public async Task DeleteGoal_RemovesGoal()
    {
        using var context = CreateContext();
        var service = CreateService(context);
        var created = await service.CreateGoalAsync("student-1", ValidGoal());

        await service.DeleteGoalAsync(created.GoalId);

        Assert.Null(await context.Goals.FindAsync(created.GoalId));
        await Assert.ThrowsAsync<GoalNotFoundException>(() => service.DeleteGoalAsync(created.GoalId));
    }

    [Fact]
    public async Task GetGoals_FiltersByStudentAndStatus()
    {
        using var context = CreateContext();
        var service = CreateService(context);
        var active = await service.CreateGoalAsync("student-1", ValidGoal());
        var completed = await service.CreateGoalAsync("student-1", ValidGoal());
        await service.CompleteGoalAsync(completed.GoalId);
        var archived = await service.CreateGoalAsync("student-1", ValidGoal());
        await service.ArchiveGoalAsync(archived.GoalId);
        var otherStudent = await service.CreateGoalAsync("student-2", ValidGoal("student-2"));

        var activeGoals = await service.GetGoalsAsync("student-1", "Active");
        Assert.Single(activeGoals);
        Assert.Equal(active.GoalId, activeGoals[0].GoalId);

        var allGoals = await service.GetGoalsAsync("student-1");
        Assert.Equal(3, allGoals.Count);
        Assert.DoesNotContain(allGoals, g => g.GoalId == otherStudent.GoalId);

        var completedGoals = await service.GetGoalsAsync("student-1", "completed");
        Assert.Single(completedGoals);
        Assert.Equal(completed.GoalId, completedGoals[0].GoalId);

        var archivedGoals = await service.GetGoalsAsync("student-1", "Archived");
        Assert.Single(archivedGoals);
        Assert.Equal(archived.GoalId, archivedGoals[0].GoalId);
    }

    [Fact]
    public async Task GetGoal_NotFound_ReturnsNotFound()
    {
        using var context = CreateContext();
        var controller = new GoalsController(CreateService(context));

        var result = await controller.GetGoal("missing-goal", CancellationToken.None);

        var notFound = Assert.IsType<NotFoundObjectResult>(result);
        using var document = JsonDocument.Parse(JsonSerializer.Serialize(notFound.Value));
        Assert.Equal("NOT_FOUND", document.RootElement.GetProperty("code").GetString());
        Assert.Equal("Goal not found", document.RootElement.GetProperty("message").GetString());
    }
}
