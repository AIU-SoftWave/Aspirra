namespace Aspirra.HabitService.Models
{
    public class Habit
    {
        public string HabitId { get; set; } = string.Empty;
        public string StudentId { get; set; } = string.Empty;
        public string GoalId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Frequency { get; set; } = string.Empty;
        public int CurrentStreak { get; set; }
    }
}
