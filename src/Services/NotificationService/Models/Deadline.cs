namespace Aspirra.NotificationService.Models;

public class Deadline
{
    public string AssessmentId { get; set; } = string.Empty;
    public string StudentId { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Kind { get; set; } = string.Empty;
    public DateOnly DueDate { get; set; }
    public bool IsCompleted { get; set; }
}
