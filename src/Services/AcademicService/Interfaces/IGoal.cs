using Aspirra.AcademicService.Models;

namespace Aspirra.AcademicService.Interfaces
{
    public interface IGoal
    {
        Goal CreateGoal(string studentId, Goal data);
        Goal UpdateGoal(string goalId, Goal data);
        void CompleteGoal(string goalId);
        void ArchiveGoal(string goalId);
        void RestoreGoal(string goalId);
        void DeleteGoal(string goalId);
        List<Goal> GetGoals(string studentId, string? status = null);
        Goal? GetGoal(string goalId);

        Task<Goal> CreateGoalAsync(string studentId, Goal data, CancellationToken cancellationToken = default);
        Task<Goal> UpdateGoalAsync(string goalId, Goal data, CancellationToken cancellationToken = default);
        Task<Goal> CompleteGoalAsync(string goalId, CancellationToken cancellationToken = default);
        Task<Goal> ArchiveGoalAsync(string goalId, CancellationToken cancellationToken = default);
        Task<Goal> RestoreGoalAsync(string goalId, CancellationToken cancellationToken = default);
        Task DeleteGoalAsync(string goalId, CancellationToken cancellationToken = default);
        Task<List<Goal>> GetGoalsAsync(string studentId, string? status = null, CancellationToken cancellationToken = default);
        Task<Goal?> GetGoalAsync(string goalId, CancellationToken cancellationToken = default);
    }
}
