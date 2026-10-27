using Aspirra.HabitService.Models;

namespace Aspirra.HabitService.Interfaces
{
    public interface IHabit
    {
        Habit CreateHabit(string studentId, Habit habit);
        void CompleteHabit(string habitId, DateOnly date);
        object GetStreaks(string studentId);
        void LinkToGoal(string habitId, string goalId);
    }
}
