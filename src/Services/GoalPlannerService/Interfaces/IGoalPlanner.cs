using Aspirra.GoalPlannerService.Models;

namespace Aspirra.GoalPlannerService.Interfaces
{
    public interface IGoalPlanner
    {
        PlanDraft GeneratePlan(string studentId, string goalText);
        PlanDraft UpdateDraft(string draftId, PlanDraft edits);
        object Approve(string draftId);
        void Discard(string draftId);
        List<PlanTask> GetTasks(string goalId);
    }
}
