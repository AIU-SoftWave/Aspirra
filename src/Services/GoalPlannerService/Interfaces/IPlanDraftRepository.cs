using Aspirra.GoalPlannerService.Models;

namespace Aspirra.GoalPlannerService.Interfaces
{
    public interface IPlanDraftRepository
    {
        Task<PlanDraft?> GetByIdAsync(string draftId);
        Task AddAsync(PlanDraft draft);
        void Remove(PlanDraft draft);
        Task SaveChangesAsync();
        Task<List<PlanTask>> GetApprovedTasksByGoalAsync(string goalId);
    }
}
