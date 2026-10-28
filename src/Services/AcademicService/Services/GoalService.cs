using Aspirra.AcademicService.Exceptions;
using Aspirra.AcademicService.Interfaces;
using Aspirra.AcademicService.Models;
using Microsoft.EntityFrameworkCore;

namespace Aspirra.AcademicService.Services
{
    public class GoalService : IGoal
    {
        private readonly IGoalRepository _repository;

        public GoalService(IGoalRepository repository)
        {
            _repository = repository;
        }

        public Goal CreateGoal(string studentId, Goal data)
        {
            return CreateGoalAsync(studentId, data).GetAwaiter().GetResult();
        }

        public Goal UpdateGoal(string goalId, Goal data)
        {
            return UpdateGoalAsync(goalId, data).GetAwaiter().GetResult();
        }

        public void CompleteGoal(string goalId)
        {
            CompleteGoalAsync(goalId).GetAwaiter().GetResult();
        }

        public void ArchiveGoal(string goalId)
        {
            ArchiveGoalAsync(goalId).GetAwaiter().GetResult();
        }

        public void RestoreGoal(string goalId)
        {
            RestoreGoalAsync(goalId).GetAwaiter().GetResult();
        }

        public void DeleteGoal(string goalId)
        {
            DeleteGoalAsync(goalId).GetAwaiter().GetResult();
        }

        public List<Goal> GetGoals(string studentId, string? status = null)
        {
            return GetGoalsAsync(studentId, status).GetAwaiter().GetResult();
        }

        public Goal? GetGoal(string goalId)
        {
            return GetGoalAsync(goalId).GetAwaiter().GetResult();
        }

        public async Task<Goal> CreateGoalAsync(string studentId, Goal data, CancellationToken cancellationToken = default)
        {
            var ownerId = string.IsNullOrWhiteSpace(studentId) ? data.StudentId : studentId;
            if (string.IsNullOrWhiteSpace(ownerId))
            {
                throw new GoalValidationException("REQUIRED_FIELD", "StudentId is required.");
            }

            Validate(data);
            var now = DateTime.UtcNow;
            var goal = new Goal
            {
                GoalId = Guid.NewGuid().ToString("N"),
                StudentId = ownerId.Trim(),
                Title = data.Title.Trim(),
                Description = data.Description ?? string.Empty,
                TargetDate = data.TargetDate,
                Priority = data.Priority.Trim(),
                Status = GoalStatus.Active,
                CompletionDate = null,
                CreatedAt = now,
                UpdatedAt = now
            };

            return await _repository.AddAsync(goal, cancellationToken);
        }

        public async Task<Goal> UpdateGoalAsync(string goalId, Goal data, CancellationToken cancellationToken = default)
        {
            var goal = await GetRequiredAsync(goalId, cancellationToken);
            Validate(data);

            goal.Title = data.Title.Trim();
            goal.Description = data.Description ?? string.Empty;
            goal.Priority = data.Priority.Trim();
            goal.TargetDate = data.TargetDate;
            goal.UpdatedAt = DateTime.UtcNow;

            await _repository.UpdateAsync(goal, cancellationToken);
            return goal;
        }

        public async Task<Goal> CompleteGoalAsync(string goalId, CancellationToken cancellationToken = default)
        {
            var goal = await GetRequiredAsync(goalId, cancellationToken);
            goal.Complete();
            goal.UpdatedAt = DateTime.UtcNow;
            await _repository.UpdateAsync(goal, cancellationToken);
            return goal;
        }

        public async Task<Goal> ArchiveGoalAsync(string goalId, CancellationToken cancellationToken = default)
        {
            var goal = await GetRequiredAsync(goalId, cancellationToken);
            goal.Archive();
            goal.UpdatedAt = DateTime.UtcNow;
            await _repository.UpdateAsync(goal, cancellationToken);
            return goal;
        }

        public async Task<Goal> RestoreGoalAsync(string goalId, CancellationToken cancellationToken = default)
        {
            var goal = await GetRequiredAsync(goalId, cancellationToken);
            goal.Restore();
            goal.UpdatedAt = DateTime.UtcNow;
            await _repository.UpdateAsync(goal, cancellationToken);
            return goal;
        }

        public async Task DeleteGoalAsync(string goalId, CancellationToken cancellationToken = default)
        {
            var goal = await GetRequiredAsync(goalId, cancellationToken);
            await _repository.DeleteAsync(goal, cancellationToken);
        }

        public async Task<List<Goal>> GetGoalsAsync(string studentId, string? status = null, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(studentId))
            {
                throw new GoalValidationException("REQUIRED_FIELD", "StudentId is required.");
            }

            GoalStatus? goalStatus = null;
            if (!string.IsNullOrWhiteSpace(status))
            {
                if (!Enum.TryParse<GoalStatus>(status, true, out var parsedStatus))
                {
                    throw new GoalValidationException("INVALID_STATUS", "Status is invalid.");
                }

                goalStatus = parsedStatus;
            }

            var query = _repository.Query().Where(g => g.StudentId == studentId);
            if (goalStatus.HasValue)
            {
                query = query.Where(g => g.Status == goalStatus.Value);
            }

            return await query
                .OrderByDescending(g => g.CreatedAt)
                .ThenBy(g => g.GoalId)
                .ToListAsync(cancellationToken);
        }

        public async Task<Goal?> GetGoalAsync(string goalId, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(goalId))
            {
                return null;
            }

            return await _repository.GetByIdAsync(goalId, cancellationToken);
        }

        private async Task<Goal> GetRequiredAsync(string goalId, CancellationToken cancellationToken)
        {
            var goal = await _repository.GetByIdAsync(goalId, cancellationToken);
            if (goal is null)
            {
                throw new GoalNotFoundException();
            }

            return goal;
        }

        private static void Validate(Goal data)
        {
            if (string.IsNullOrWhiteSpace(data.Title))
            {
                throw new GoalValidationException("REQUIRED_FIELD", "Title is required.");
            }

            if (data.TargetDate == default)
            {
                throw new GoalValidationException("REQUIRED_FIELD", "TargetDate is required.");
            }

            if (string.IsNullOrWhiteSpace(data.Priority))
            {
                throw new GoalValidationException("REQUIRED_FIELD", "Priority is required.");
            }

            if (data.TargetDate <= DateOnly.FromDateTime(DateTime.UtcNow))
            {
                throw new GoalValidationException("INVALID_DATE", "Target date must be in the future");
            }
        }
    }
}
