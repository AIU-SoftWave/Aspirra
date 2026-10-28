using Aspirra.GoalPlannerService.Contracts;
using Aspirra.GoalPlannerService.Interfaces;
using Aspirra.GoalPlannerService.Models;

namespace Aspirra.GoalPlannerService.Services
{
    public class GoalPlannerService : IGoalPlanner
    {
        private readonly IPlanDraftRepository _repository;
        private readonly IAIProvider _aiProvider;
        private readonly IGoalClient _goalClient;

        public GoalPlannerService(
            IPlanDraftRepository repository,
            IAIProvider aiProvider,
            IGoalClient goalClient)
        {
            _repository = repository;
            _aiProvider = aiProvider;
            _goalClient = goalClient;
        }

        public async Task<ServiceResult<PlanDraft>> GeneratePlanAsync(GeneratePlanRequest request)
        {
            if (request is null || string.IsNullOrWhiteSpace(request.GoalText))
            {
                return ServiceResult<PlanDraft>.Failure(
                    400, "REQUIRED_FIELD", "Goal description is required.");
            }

            AiPlan? aiPlan;
            try
            {
                aiPlan = await _aiProvider.GenerateAsync(request.GoalText);
            }
            catch
            {
                return AiUnavailable();
            }

            if (aiPlan?.Milestones is null || aiPlan.Milestones.Count == 0)
            {
                return AiUnavailable();
            }

            if (!IsValidAiPlan(aiPlan))
            {
                return PlanInvalid();
            }

            var now = DateTime.UtcNow;
            var draft = new PlanDraft
            {
                DraftId = Guid.NewGuid().ToString(),
                StudentId = request.StudentId ?? string.Empty,
                GoalId = string.IsNullOrWhiteSpace(request.GoalId) ? null : request.GoalId,
                GoalText = request.GoalText.Trim(),
                Status = PlanStatus.Draft,
                CreatedAt = now,
                UpdatedAt = now
            };

            for (var milestoneIndex = 0; milestoneIndex < aiPlan.Milestones.Count; milestoneIndex++)
            {
                var aiMilestone = aiPlan.Milestones[milestoneIndex];
                var milestone = new Milestone
                {
                    MilestoneId = Guid.NewGuid().ToString(),
                    DraftId = draft.DraftId,
                    Title = aiMilestone.Title.Trim(),
                    Deadline = aiMilestone.Deadline,
                    OrderIndex = milestoneIndex
                };

                var aiTasks = aiMilestone.Tasks ?? new List<AiTask>();
                for (var taskIndex = 0; taskIndex < aiTasks.Count; taskIndex++)
                {
                    var aiTask = aiTasks[taskIndex];
                    milestone.Tasks.Add(new PlanTask
                    {
                        TaskId = Guid.NewGuid().ToString(),
                        MilestoneId = milestone.MilestoneId,
                        Title = aiTask.Title?.Trim() ?? string.Empty,
                        DurationHours = aiTask.DurationHours,
                        Deadline = aiTask.Deadline,
                        DependsOnTaskId = null,
                        OrderIndex = taskIndex
                    });
                }

                draft.Milestones.Add(milestone);
            }

            await _repository.AddAsync(draft);
            await _repository.SaveChangesAsync();

            return ServiceResult<PlanDraft>.Success(draft);
        }

        public async Task<ServiceResult<PlanDraft>> UpdateDraftAsync(string draftId, PlanDraftInput input)
        {
            var draft = await _repository.GetByIdAsync(draftId);
            if (draft is null)
            {
                return DraftNotFound();
            }

            if (draft.Status != PlanStatus.Draft)
            {
                return NotEditable();
            }

            if (input?.Milestones is null || input.Milestones.Count == 0 || !IsValidInput(input))
            {
                return PlanInvalid();
            }

            if (!string.IsNullOrWhiteSpace(input.GoalText))
            {
                draft.GoalText = input.GoalText.Trim();
            }

            if (!string.IsNullOrWhiteSpace(input.GoalId))
            {
                draft.GoalId = input.GoalId;
            }

            draft.UpdatedAt = DateTime.UtcNow;
            draft.Milestones.Clear();

            for (var milestoneIndex = 0; milestoneIndex < input.Milestones.Count; milestoneIndex++)
            {
                var milestoneInput = input.Milestones[milestoneIndex];
                var milestone = new Milestone
                {
                    MilestoneId = Guid.NewGuid().ToString(),
                    DraftId = draft.DraftId,
                    Title = milestoneInput.Title.Trim(),
                    Deadline = milestoneInput.Deadline,
                    OrderIndex = milestoneInput.OrderIndex
                };

                var tasks = milestoneInput.Tasks ?? new List<PlanTaskInput>();
                for (var taskIndex = 0; taskIndex < tasks.Count; taskIndex++)
                {
                    var taskInput = tasks[taskIndex];
                    milestone.Tasks.Add(new PlanTask
                    {
                        TaskId = Guid.NewGuid().ToString(),
                        MilestoneId = milestone.MilestoneId,
                        Title = taskInput.Title?.Trim() ?? string.Empty,
                        DurationHours = taskInput.DurationHours,
                        Deadline = taskInput.Deadline,
                        DependsOnTaskId = taskInput.DependsOnTaskId,
                        OrderIndex = taskInput.OrderIndex
                    });
                }

                draft.Milestones.Add(milestone);
            }

            await _repository.SaveChangesAsync();

            return ServiceResult<PlanDraft>.Success(draft);
        }

        public async Task<ServiceResult<PlanDraft>> ApproveAsync(string draftId)
        {
            var draft = await _repository.GetByIdAsync(draftId);
            if (draft is null)
            {
                return DraftNotFound();
            }

            if (draft.Status == PlanStatus.Approved)
            {
                return ServiceResult<PlanDraft>.Success(draft);
            }

            if (!string.IsNullOrWhiteSpace(draft.GoalId))
            {
                var goalExists = await _goalClient.GoalExistsAsync(draft.GoalId);
                if (!goalExists)
                {
                    return ServiceResult<PlanDraft>.Failure(
                        404, "GOAL_NOT_FOUND", "Goal not found");
                }
            }

            draft.Approve();
            draft.UpdatedAt = DateTime.UtcNow;
            await _repository.SaveChangesAsync();

            return ServiceResult<PlanDraft>.Success(draft);
        }

        public async Task<ServiceResult<bool>> DiscardAsync(string draftId)
        {
            var draft = await _repository.GetByIdAsync(draftId);
            if (draft is null)
            {
                return ServiceResult<bool>.Failure(
                    404, "NOT_FOUND", "Plan draft not found");
            }

            _repository.Remove(draft);
            await _repository.SaveChangesAsync();

            return ServiceResult<bool>.Success(true);
        }

        public Task<PlanDraft?> GetDraftAsync(string draftId) => _repository.GetByIdAsync(draftId);

        public Task<List<PlanTask>> GetTasksAsync(string goalId) =>
            _repository.GetApprovedTasksByGoalAsync(goalId);

        private static bool IsValidAiPlan(AiPlan plan) =>
            plan.Milestones.All(milestone =>
                milestone is not null
                && !string.IsNullOrWhiteSpace(milestone.Title)
                && milestone.Deadline != default
                && (milestone.Tasks ?? new List<AiTask>()).All(task =>
                    task is not null && task.DurationHours > 0));

        private static bool IsValidInput(PlanDraftInput input) =>
            input.Milestones.All(milestone =>
                milestone is not null
                && !string.IsNullOrWhiteSpace(milestone.Title)
                && milestone.Deadline != default
                && (milestone.Tasks ?? new List<PlanTaskInput>()).All(task =>
                    task is not null && task.DurationHours > 0));

        private static ServiceResult<PlanDraft> AiUnavailable() =>
            ServiceResult<PlanDraft>.Failure(
                503, "AI_UNAVAILABLE", "Planning is unavailable, try again later");

        private static ServiceResult<PlanDraft> PlanInvalid() =>
            ServiceResult<PlanDraft>.Failure(
                400, "PLAN_INVALID", "Plan could not be generated");

        private static ServiceResult<PlanDraft> DraftNotFound() =>
            ServiceResult<PlanDraft>.Failure(
                404, "NOT_FOUND", "Plan draft not found");

        private static ServiceResult<PlanDraft> NotEditable() =>
            ServiceResult<PlanDraft>.Failure(
                409, "PLAN_NOT_EDITABLE", "Only draft plans can be edited");
    }
}
