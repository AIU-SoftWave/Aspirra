using Aspirra.GoalPlannerService.Data;
using Aspirra.GoalPlannerService.Interfaces;
using Aspirra.GoalPlannerService.Models;
using Microsoft.EntityFrameworkCore;

namespace Aspirra.GoalPlannerService.Repositories
{
    public class PlanDraftRepository : IPlanDraftRepository
    {
        private readonly GoalPlannerDbContext _dbContext;

        public PlanDraftRepository(GoalPlannerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<PlanDraft?> GetByIdAsync(string draftId) =>
            _dbContext.PlanDrafts
                .Include(draft => draft.Milestones)
                .ThenInclude(milestone => milestone.Tasks)
                .FirstOrDefaultAsync(draft => draft.DraftId == draftId);

        public async Task AddAsync(PlanDraft draft)
        {
            await _dbContext.PlanDrafts.AddAsync(draft);
        }

        public void Remove(PlanDraft draft)
        {
            _dbContext.PlanDrafts.Remove(draft);
        }

        public Task SaveChangesAsync() => _dbContext.SaveChangesAsync();

        public Task<List<PlanTask>> GetApprovedTasksByGoalAsync(string goalId)
        {
            if (string.IsNullOrWhiteSpace(goalId))
            {
                return Task.FromResult(new List<PlanTask>());
            }

            return (from task in _dbContext.PlanTasks.AsNoTracking()
                    join milestone in _dbContext.Milestones.AsNoTracking()
                        on task.MilestoneId equals milestone.MilestoneId
                    join draft in _dbContext.PlanDrafts.AsNoTracking()
                        on milestone.DraftId equals draft.DraftId
                    where draft.GoalId == goalId && draft.Status == PlanStatus.Approved
                    orderby milestone.OrderIndex, task.OrderIndex
                    select task).ToListAsync();
        }
    }
}
