using Aspirra.AcademicService.Interfaces;
using Aspirra.AcademicService.Models;

namespace Aspirra.AcademicService.Services
{
    public class GoalService : IGoal
    {
        public Goal CreateGoal(string studentId, Goal data)
        {
            data.StudentId = studentId;
            data.Status = GoalStatus.Active;
            return data;
        }

        public Goal UpdateGoal(string goalId, Goal data)
        {
            data.GoalId = goalId;
            return data;
        }

        public void CompleteGoal(string goalId) { }
        public void ArchiveGoal(string goalId) { }
        public void RestoreGoal(string goalId) { }
        public void DeleteGoal(string goalId) { }
        public List<Goal> GetGoals(string studentId, string status) => new();
    }
}
