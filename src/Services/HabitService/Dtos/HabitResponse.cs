namespace Aspirra.HabitService.Dtos;

public sealed class HabitResponse
{
    public string HabitId { get; set; } = string.Empty;
    public string StudentId { get; set; } = string.Empty;
    public string? GoalId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Frequency { get; set; } = string.Empty;
    public string? GoalTarget { get; set; }
    public TimeOnly? ReminderTime { get; set; }
    public int CurrentStreak { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
