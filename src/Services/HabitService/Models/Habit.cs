namespace Aspirra.HabitService.Models;

public class Habit
{
    public string HabitId { get; set; } = string.Empty;
    public string StudentId { get; set; } = string.Empty;
    public string? GoalId { get; set; }
    public string Name { get; set; } = string.Empty;
    public HabitFrequency Frequency { get; set; }
    public string? GoalTarget { get; set; }
    public TimeOnly? ReminderTime { get; set; }
    public int CurrentStreak { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public ICollection<HabitLog> HabitLogs { get; set; } = new List<HabitLog>();
}
