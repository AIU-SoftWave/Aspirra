using Aspirra.HabitService.Interfaces;
using Aspirra.HabitService.Models;

namespace Aspirra.HabitService.Services
{
    public class HabitService : IHabit
    {
        public Habit CreateHabit(string studentId, Habit habit)
        {
            habit.StudentId = studentId;
            return habit;
        }

        public void CompleteHabit(string habitId, DateOnly date) { }
        public object GetStreaks(string studentId) => new();
        public void LinkToGoal(string habitId, string goalId) { }
    }
}
