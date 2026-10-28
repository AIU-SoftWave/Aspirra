namespace Aspirra.HabitService.Dtos;

public sealed class HabitStreakResponse
{
    public string HabitId { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public int CurrentStreak { get; set; }
    public bool DoneToday { get; set; }
}
