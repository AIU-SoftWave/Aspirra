using Aspirra.NotificationService.Exceptions;

namespace Aspirra.NotificationService.Models;

public class Notification
{
    public const string DeadlineReminderType = "DeadlineReminder";

    public string NotificationId { get; set; } = string.Empty;
    public string StudentId { get; set; } = string.Empty;
    public string AssessmentId { get; set; } = string.Empty;
    public string Recipient { get; set; } = string.Empty;
    public string Type { get; set; } = DeadlineReminderType;
    public string Subject { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public NotificationStatus Status { get; set; } = NotificationStatus.Pending;
    public int Attempts { get; set; }
    public DateTime? SentAt { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    // Pending -> Sent (first send) and Failed -> Sent (retry). Sent -> Sent is rejected.
    public void MarkSent(DateTime now)
    {
        if (Status == NotificationStatus.Sent)
        {
            throw new NotificationTransitionException("A sent notification cannot be sent again.");
        }

        Status = NotificationStatus.Sent;
        Attempts++;
        SentAt = now;
        UpdatedAt = now;
    }

    // Pending -> Failed (first send) and Failed -> Failed (retry failed again). Sent -> Failed is rejected.
    public void MarkFailed(DateTime now)
    {
        if (Status == NotificationStatus.Sent)
        {
            throw new NotificationTransitionException("A sent notification cannot fail.");
        }

        Status = NotificationStatus.Failed;
        Attempts++;
        UpdatedAt = now;
    }

    // Only a failed notification can be retried.
    public bool CanRetry => Status == NotificationStatus.Failed;
}
