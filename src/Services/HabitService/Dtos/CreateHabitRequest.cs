namespace Aspirra.HabitService.Dtos;

public sealed class CreateHabitRequest
{
    public string? StudentId { get; set; }
    public string? GoalId { get; set; }
    public string? Name { get; set; }
    public string? Frequency { get; set; }
    public string? GoalTarget { get; set; }
    public TimeOnly? ReminderTime { get; set; }
}
