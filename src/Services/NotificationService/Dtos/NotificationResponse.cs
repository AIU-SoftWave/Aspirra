namespace Aspirra.NotificationService.Dtos;

public sealed class NotificationResponse
{
    public string NotificationId { get; set; } = string.Empty;
    public string StudentId { get; set; } = string.Empty;
    public string AssessmentId { get; set; } = string.Empty;
    public string Recipient { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public string Subject { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public int Attempts { get; set; }
    public DateTime? SentAt { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
