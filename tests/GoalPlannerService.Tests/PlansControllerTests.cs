using System.Text.Json;
using Aspirra.GoalPlannerService.Contracts;
using Aspirra.GoalPlannerService.Controllers;
using Aspirra.GoalPlannerService.Data;
using Aspirra.GoalPlannerService.Interfaces;
using Aspirra.GoalPlannerService.Models;
using Aspirra.GoalPlannerService.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlanService = Aspirra.GoalPlannerService.Services.GoalPlannerService;

namespace GoalPlannerService.Tests;

public class PlansControllerTests
{
    private static readonly DateOnly Today = DateOnly.FromDateTime(DateTime.UtcNow);

    [Fact]
    public async Task GeneratePlan_WithDescription_CreatesDraftWithMilestonesAndTasks()
    {
        var (controller, db, _) = CreateController(new FakeAIProvider(_ => ValidAiPlan()));

        var result = await controller.Generate(new GeneratePlanRequest
        {
            StudentId = "student-1",
            GoalText = "Become a backend developer"
        });

        var created = Assert.IsType<CreatedResult>(result.Result);
        var draft = Assert.IsType<PlanDraft>(created.Value);
        Assert.Equal(PlanStatus.Draft, draft.Status);
        Assert.Equal("student-1", draft.StudentId);
        Assert.Equal("Become a backend developer", draft.GoalText);
        Assert.Equal(2, draft.Milestones.Count);
        Assert.All(draft.Milestones, milestone => Assert.Equal(2, milestone.Tasks.Count));
        Assert.Equal(1, await db.PlanDrafts.CountAsync());
        Assert.Equal(2, await db.Milestones.CountAsync());
        Assert.Equal(4, await db.PlanTasks.CountAsync());
    }

    [Fact]
    public async Task GeneratePlan_EmptyDescription_ReturnsRequiredFieldError()
    {
        var (controller, _, _) = CreateController(new FakeAIProvider(_ => ValidAiPlan()));

        var result = await controller.Generate(new GeneratePlanRequest
        {
            StudentId = "student-1",
            GoalText = "   "
        });

        var error = Assert.IsType<ObjectResult>(result.Result);
        Assert.Equal(400, error.StatusCode);
        Assert.Equal("REQUIRED_FIELD", ReadCode(error));
        Assert.Equal("Goal description is required.", ReadMessage(error));
    }

    [Fact]
    public async Task GeneratePlan_AiUnavailable_ReturnsAiUnavailableError()
    {
        var (controller, _, _) = CreateController(
            new FakeAIProvider(_ => throw new InvalidOperationException("provider down")));

        var result = await controller.Generate(new GeneratePlanRequest
        {
            StudentId = "student-1",
            GoalText = "Learn data structures"
        });

        var error = Assert.IsType<ObjectResult>(result.Result);
        Assert.Equal(503, error.StatusCode);
        Assert.Equal("AI_UNAVAILABLE", ReadCode(error));
        Assert.Equal("Planning is unavailable, try again later", ReadMessage(error));
    }

    [Fact]
    public async Task GeneratePlan_InvalidStructure_ReturnsPlanInvalidError()
    {
        var invalidPlan = new AiPlan
        {
            Milestones = new List<AiMilestone>
            {
                new()
                {
                    Title = "Broken milestone",
                    Deadline = Today.AddDays(7),
                    Tasks = new List<AiTask>
                    {
                        new() { Title = "Zero duration", DurationHours = 0, Deadline = Today.AddDays(3) }
                    }
                }
            }
        };
        var (controller, _, _) = CreateController(new FakeAIProvider(_ => invalidPlan));

        var result = await controller.Generate(new GeneratePlanRequest
        {
            StudentId = "student-1",
            GoalText = "Learn data structures"
        });

        var error = Assert.IsType<ObjectResult>(result.Result);
        Assert.Equal(400, error.StatusCode);
        Assert.Equal("PLAN_INVALID", ReadCode(error));
        Assert.Equal("Plan could not be generated", ReadMessage(error));
    }

    [Fact]
    public async Task UpdateDraft_EditsMilestonesAndTasks()
    {
        var (controller, db, _) = CreateController(new FakeAIProvider(_ => ValidAiPlan()));
        var generated = await controller.Generate(new GeneratePlanRequest
        {
            StudentId = "student-1",
            GoalText = "Learn ASP.NET Core"
        });
        var draft = Assert.IsType<PlanDraft>(Assert.IsType<CreatedResult>(generated.Result).Value);

        var input = new PlanDraftInput
        {
            GoalText = "Learn ASP.NET Core deeply",
            Milestones = new List<MilestoneInput>
            {
                new()
                {
                    Title = "Updated milestone",
                    Deadline = Today.AddDays(10),
                    OrderIndex = 0,
                    Tasks = new List<PlanTaskInput>
                    {
                        new()
                        {
                            Title = "Updated task",
                            DurationHours = 5,
                            Deadline = Today.AddDays(5),
                            OrderIndex = 0
                        }
                    }
                }
            }
        };

        var result = await controller.Update(draft.DraftId, input);

        var ok = Assert.IsType<OkObjectResult>(result.Result);
        Assert.Equal(200, ok.StatusCode);
        var updated = Assert.IsType<PlanDraft>(ok.Value);
        Assert.Equal("Learn ASP.NET Core deeply", updated.GoalText);
        Assert.Single(updated.Milestones);
        Assert.Equal("Updated milestone", updated.Milestones[0].Title);
        Assert.Single(updated.Milestones[0].Tasks);
        Assert.Equal("Updated task", updated.Milestones[0].Tasks[0].Title);
        Assert.Equal(5, updated.Milestones[0].Tasks[0].DurationHours);
        Assert.Equal(1, await db.Milestones.CountAsync());
        Assert.Equal(1, await db.PlanTasks.CountAsync());
    }

    [Fact]
    public async Task UpdateDraft_ApprovedPlan_ReturnsNotEditable()
    {
        var (controller, _, _) = CreateController(new FakeAIProvider(_ => ValidAiPlan()));
        var generated = await controller.Generate(new GeneratePlanRequest
        {
            StudentId = "student-1",
            GoalText = "Learn ASP.NET Core"
        });
        var draft = Assert.IsType<PlanDraft>(Assert.IsType<CreatedResult>(generated.Result).Value);
        await controller.Approve(draft.DraftId);

        var result = await controller.Update(draft.DraftId, new PlanDraftInput
        {
            Milestones = new List<MilestoneInput>
            {
                new()
                {
                    Title = "New milestone",
                    Deadline = Today.AddDays(5),
                    Tasks = new List<PlanTaskInput>
                    {
                        new() { Title = "New task", DurationHours = 2, Deadline = Today.AddDays(4) }
                    }
                }
            }
        });

        var error = Assert.IsType<ObjectResult>(result.Result);
        Assert.Equal(409, error.StatusCode);
        Assert.Equal("PLAN_NOT_EDITABLE", ReadCode(error));
        Assert.Equal("Only draft plans can be edited", ReadMessage(error));
    }

    [Fact]
    public async Task Approve_ValidDraft_SetsApproved()
    {
        var (controller, db, goalClient) = CreateController(new FakeAIProvider(_ => ValidAiPlan()));
        var generated = await controller.Generate(new GeneratePlanRequest
        {
            StudentId = "student-1",
            GoalId = "goal-1",
            GoalText = "Learn ASP.NET Core"
        });
        var draft = Assert.IsType<PlanDraft>(Assert.IsType<CreatedResult>(generated.Result).Value);

        var result = await controller.Approve(draft.DraftId);

        var ok = Assert.IsType<OkObjectResult>(result.Result);
        Assert.Equal(200, ok.StatusCode);
        var approved = Assert.IsType<PlanDraft>(ok.Value);
        Assert.Equal(PlanStatus.Approved, approved.Status);
        Assert.Contains("goal-1", goalClient.RequestedGoalIds);
        db.ChangeTracker.Clear();
        Assert.Equal(PlanStatus.Approved, (await db.PlanDrafts.SingleAsync()).Status);
    }

    [Fact]
    public async Task Approve_GoalMissing_ReturnsGoalNotFound()
    {
        var (controller, db, _) = CreateController(
            new FakeAIProvider(_ => ValidAiPlan()),
            goalExists: false);
        var generated = await controller.Generate(new GeneratePlanRequest
        {
            StudentId = "student-1",
            GoalId = "missing-goal",
            GoalText = "Learn ASP.NET Core"
        });
        var draft = Assert.IsType<PlanDraft>(Assert.IsType<CreatedResult>(generated.Result).Value);

        var result = await controller.Approve(draft.DraftId);

        var error = Assert.IsType<ObjectResult>(result.Result);
        Assert.Equal(404, error.StatusCode);
        Assert.Equal("GOAL_NOT_FOUND", ReadCode(error));
        Assert.Equal("Goal not found", ReadMessage(error));
        db.ChangeTracker.Clear();
        Assert.Equal(PlanStatus.Draft, (await db.PlanDrafts.SingleAsync()).Status);
    }

    [Fact]
    public async Task Discard_DeletesDraft()
    {
        var (controller, db, _) = CreateController(new FakeAIProvider(_ => ValidAiPlan()));
        var generated = await controller.Generate(new GeneratePlanRequest
        {
            StudentId = "student-1",
            GoalText = "Learn ASP.NET Core"
        });
        var draft = Assert.IsType<PlanDraft>(Assert.IsType<CreatedResult>(generated.Result).Value);

        var result = await controller.Discard(draft.DraftId);

        Assert.IsType<NoContentResult>(result);
        db.ChangeTracker.Clear();
        Assert.Equal(0, await db.PlanDrafts.CountAsync());
        Assert.Equal(0, await db.Milestones.CountAsync());
        Assert.Equal(0, await db.PlanTasks.CountAsync());
    }

    [Fact]
    public async Task GetTasks_ReturnsOnlyApprovedPlanTasks()
    {
        var (controller, _, _) = CreateController(new FakeAIProvider(_ => ValidAiPlan()));

        var approvedGenerated = await controller.Generate(new GeneratePlanRequest
        {
            StudentId = "student-1",
            GoalId = "goal-1",
            GoalText = "Learn ASP.NET Core"
        });
        var approvedDraft = Assert.IsType<PlanDraft>(
            Assert.IsType<CreatedResult>(approvedGenerated.Result).Value);
        await controller.Approve(approvedDraft.DraftId);

        await controller.Generate(new GeneratePlanRequest
        {
            StudentId = "student-1",
            GoalId = "goal-1",
            GoalText = "Another draft for the same goal"
        });

        var otherGenerated = await controller.Generate(new GeneratePlanRequest
        {
            StudentId = "student-2",
            GoalId = "goal-2",
            GoalText = "A different goal"
        });
        var otherDraft = Assert.IsType<PlanDraft>(
            Assert.IsType<CreatedResult>(otherGenerated.Result).Value);
        await controller.Approve(otherDraft.DraftId);

        var result = await controller.GetTasks("goal-1");

        var ok = Assert.IsType<OkObjectResult>(result.Result);
        var tasks = Assert.IsType<List<PlanTask>>(ok.Value);
        Assert.Equal(4, tasks.Count);
        var approvedMilestoneIds = approvedDraft.Milestones
            .Select(milestone => milestone.MilestoneId)
            .ToHashSet();
        Assert.All(tasks, task => Assert.Contains(task.MilestoneId, approvedMilestoneIds));
    }

    [Fact]
    public async Task Approve_AlreadyApproved_IsIdempotent()
    {
        var (controller, _, _) = CreateController(new FakeAIProvider(_ => ValidAiPlan()));
        var generated = await controller.Generate(new GeneratePlanRequest
        {
            StudentId = "student-1",
            GoalText = "Learn ASP.NET Core"
        });
        var draft = Assert.IsType<PlanDraft>(Assert.IsType<CreatedResult>(generated.Result).Value);

        await controller.Approve(draft.DraftId);
        var result = await controller.Approve(draft.DraftId);

        var ok = Assert.IsType<OkObjectResult>(result.Result);
        Assert.Equal(200, ok.StatusCode);
        Assert.Equal(PlanStatus.Approved, Assert.IsType<PlanDraft>(ok.Value).Status);
    }

    [Fact]
    public async Task GetDraft_Missing_ReturnsNotFound()
    {
        var (controller, _, _) = CreateController(new FakeAIProvider(_ => ValidAiPlan()));

        var result = await controller.Get("missing-draft");

        var error = Assert.IsType<NotFoundObjectResult>(result.Result);
        Assert.Equal(404, error.StatusCode);
        Assert.Equal("NOT_FOUND", ReadCode(error));
    }

    [Fact]
    public async Task Discard_Missing_ReturnsNotFound()
    {
        var (controller, _, _) = CreateController(new FakeAIProvider(_ => ValidAiPlan()));

        var result = await controller.Discard("missing-draft");

        var error = Assert.IsType<ObjectResult>(result);
        Assert.Equal(404, error.StatusCode);
        Assert.Equal("NOT_FOUND", ReadCode(error));
    }

    private static (PlansController Controller, GoalPlannerDbContext Db, FakeGoalClient GoalClient)
        CreateController(FakeAIProvider aiProvider, bool goalExists = true)
    {
        var options = new DbContextOptionsBuilder<GoalPlannerDbContext>()
            .UseInMemoryDatabase($"goal-planner-tests-{Guid.NewGuid()}")
            .Options;
        var db = new GoalPlannerDbContext(options);
        db.Database.EnsureCreated();
        var goalClient = new FakeGoalClient { Exists = goalExists };
        var repository = new PlanDraftRepository(db);
        var service = new PlanService(repository, aiProvider, goalClient);
        return (new PlansController(service), db, goalClient);
    }

    private static AiPlan ValidAiPlan() => new()
    {
        Milestones = new List<AiMilestone>
        {
            new()
            {
                Title = "Foundations",
                Deadline = Today.AddDays(7),
                Tasks = new List<AiTask>
                {
                    new() { Title = "Research", DurationHours = 2, Deadline = Today.AddDays(3) },
                    new() { Title = "Practice", DurationHours = 3, Deadline = Today.AddDays(6) }
                }
            },
            new()
            {
                Title = "Advanced work",
                Deadline = Today.AddDays(14),
                Tasks = new List<AiTask>
                {
                    new() { Title = "Build project", DurationHours = 4, Deadline = Today.AddDays(10) },
                    new() { Title = "Review", DurationHours = 2, Deadline = Today.AddDays(13) }
                }
            }
        }
    };

    private static string ReadCode(ObjectResult result) => ReadProperty(result, "code");

    private static string ReadMessage(ObjectResult result) => ReadProperty(result, "message");

    private static string ReadProperty(ObjectResult result, string propertyName)
    {
        var json = JsonSerializer.Serialize(result.Value);
        using var document = JsonDocument.Parse(json);
        return document.RootElement.GetProperty(propertyName).GetString() ?? string.Empty;
    }

    private sealed class FakeAIProvider : IAIProvider
    {
        private readonly Func<string, AiPlan?> _handler;

        public FakeAIProvider(Func<string, AiPlan?> handler)
        {
            _handler = handler;
        }

        public Task<AiPlan> GenerateAsync(string prompt) =>
            Task.FromResult(_handler(prompt)!);
    }

    private sealed class FakeGoalClient : IGoalClient
    {
        public bool Exists { get; set; } = true;
        public List<string> RequestedGoalIds { get; } = new();

        public Task<bool> GoalExistsAsync(string goalId)
        {
            RequestedGoalIds.Add(goalId);
            return Task.FromResult(Exists);
        }
    }
}
