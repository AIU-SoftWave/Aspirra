using Aspirra.GoalPlannerService.Models;

namespace Aspirra.GoalPlannerService.Interfaces
{
    public interface IAIProvider
    {
        Task<AiPlan> GenerateAsync(string prompt);
    }
}
