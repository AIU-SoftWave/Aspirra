namespace Aspirra.GoalPlannerService.Interfaces
{
    public interface IGoalClient
    {
        Task<bool> GoalExistsAsync(string goalId);
    }
}
