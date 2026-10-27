using Aspirra.GoalPlannerService.Interfaces;
using Aspirra.GoalPlannerService.Models;

namespace Aspirra.GoalPlannerService.Services
{
    public class GoalPlannerService : IGoalPlanner
    {
        public PlanDraft GeneratePlan(string studentId, string goalText) => new();
        public PlanDraft UpdateDraft(string draftId, PlanDraft edits) => edits;
        public object Approve(string draftId) => new();
        public void Discard(string draftId) { }
        public List<PlanTask> GetTasks(string goalId) => new();
    }
}
