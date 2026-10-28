using Aspirra.GoalPlannerService.Contracts;
using Aspirra.GoalPlannerService.Models;

namespace Aspirra.GoalPlannerService.Interfaces
{
    public interface IGoalPlanner
    {
        Task<ServiceResult<PlanDraft>> GeneratePlanAsync(GeneratePlanRequest request);
        Task<ServiceResult<PlanDraft>> UpdateDraftAsync(string draftId, PlanDraftInput input);
        Task<ServiceResult<PlanDraft>> ApproveAsync(string draftId);
        Task<ServiceResult<bool>> DiscardAsync(string draftId);
        Task<PlanDraft?> GetDraftAsync(string draftId);
        Task<List<PlanTask>> GetTasksAsync(string goalId);
    }
}
