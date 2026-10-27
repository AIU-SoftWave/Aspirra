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
        List<Goal> GetGoals(string studentId, string status);
    }
}
