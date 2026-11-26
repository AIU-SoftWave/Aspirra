using Aspirra.GoalPlannerService.Interfaces;
using Aspirra.GoalPlannerService.Models;
using Moq;

namespace Aspirra.GoalPlannerService.Tests;

public class GoalPlannerMoqTests
{
    private static PlanDraft DraftWithGoal(string? goalId)
    {
        return new PlanDraft
        {
            DraftId = "draft-1",
            StudentId = "student-1",
            GoalId = goalId,
            GoalText = "Prepare for final exams",
            Status = PlanStatus.Draft,
            CreatedAt = DateTime.UtcNow.AddDays(-1),
            UpdatedAt = DateTime.UtcNow.AddDays(-1)
        };
    }

    [Fact]
    public async Task TC_05_6_01_Approve_WithExistingGoal_ApprovesAndSavesOnce()
    {
        var draft = DraftWithGoal("goal-1");
        var repository = new Mock<IPlanDraftRepository>(MockBehavior.Strict);
        var aiProvider = new Mock<IAIProvider>(MockBehavior.Strict);
        var goalClient = new Mock<IGoalClient>(MockBehavior.Strict);
        repository
            .Setup(r => r.GetByIdAsync("draft-1"))
            .ReturnsAsync(draft);
        goalClient
            .Setup(g => g.GoalExistsAsync("goal-1"))
            .ReturnsAsync(true);
        repository
            .Setup(r => r.SaveChangesAsync())
            .Returns(Task.CompletedTask);

        var service = new Services.GoalPlannerService(repository.Object, aiProvider.Object, goalClient.Object);
        var result = await service.ApproveAsync("draft-1");

        Assert.True(result.IsSuccess);
        Assert.Equal(PlanStatus.Approved, result.Value!.Status);
        repository.Verify(r => r.SaveChangesAsync(), Times.Once);
        goalClient.Verify(g => g.GoalExistsAsync("goal-1"), Times.Once);
    }

    [Fact]
    public async Task TC_05_6_02_Approve_WhenGoalDoesNotExist_ReturnsGoalNotFoundAndDoesNotSave()
    {
        var draft = DraftWithGoal("missing-goal");
        var repository = new Mock<IPlanDraftRepository>(MockBehavior.Strict);
        var aiProvider = new Mock<IAIProvider>(MockBehavior.Strict);
        var goalClient = new Mock<IGoalClient>(MockBehavior.Strict);
        repository
            .Setup(r => r.GetByIdAsync("draft-1"))
            .ReturnsAsync(draft);
        goalClient
            .Setup(g => g.GoalExistsAsync("missing-goal"))
            .ReturnsAsync(false);

        var service = new Services.GoalPlannerService(repository.Object, aiProvider.Object, goalClient.Object);
        var result = await service.ApproveAsync("draft-1");

        Assert.False(result.IsSuccess);
        Assert.Equal(404, result.Error!.StatusCode);
        Assert.Equal("GOAL_NOT_FOUND", result.Error.Code);
        repository.Verify(r => r.SaveChangesAsync(), Times.Never);
    }

    [Fact]
    public async Task TC_05_6_03_Approve_WhenDraftMissing_ReturnsNotFound()
    {
        var repository = new Mock<IPlanDraftRepository>(MockBehavior.Strict);
        var aiProvider = new Mock<IAIProvider>(MockBehavior.Strict);
        var goalClient = new Mock<IGoalClient>(MockBehavior.Strict);
        repository
            .Setup(r => r.GetByIdAsync("missing-draft"))
            .ReturnsAsync((PlanDraft?)null);

        var service = new Services.GoalPlannerService(repository.Object, aiProvider.Object, goalClient.Object);
        var result = await service.ApproveAsync("missing-draft");

        Assert.False(result.IsSuccess);
        Assert.Equal(404, result.Error!.StatusCode);
        Assert.Equal("NOT_FOUND", result.Error.Code);
        goalClient.Verify(g => g.GoalExistsAsync(It.IsAny<string>()), Times.Never);
    }
}
