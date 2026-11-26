using Aspirra.NotificationService.Models;

namespace Aspirra.NotificationService.Tests;

// The objects of the Send Deadline Reminders object diagram (deadline-reminders-object.puml) used as test fixtures.
internal static class NotificationFixtures
{
    public const string StudentId = "student-1001";
    public const string Email = "student1001@aiu.edu.eg";
    public const int Days = 1;
    public const string AssignmentId = "asg-3001";
    public const string NotificationId = "ntf-5001";

    public static Deadline DatabaseProjectDeadline() => new()
    {
        AssessmentId = AssignmentId,
        StudentId = StudentId,
        Title = "Database Project",
        Kind = "Assignment",
        DueDate = new DateOnly(2026, 11, 27),
        IsCompleted = false
    };

    public static Deadline CompletedDeadline() => new()
    {
        AssessmentId = "asg-3002",
        StudentId = StudentId,
        Title = "ERD Sheet",
        Kind = "Assignment",
        DueDate = new DateOnly(2026, 11, 27),
        IsCompleted = true
    };

    public static Notification NotificationWithStatus(NotificationStatus status) => new()
    {
        NotificationId = NotificationId,
        StudentId = StudentId,
        AssessmentId = AssignmentId,
        Recipient = Email,
        Type = Notification.DeadlineReminderType,
        Subject = "Reminder: Database Project is due on 2026-11-27",
        Message = "Your assignment \"Database Project\" is due on 2026-11-27.",
        Status = status,
        Attempts = status == NotificationStatus.Pending ? 0 : 1,
        SentAt = status == NotificationStatus.Sent ? new DateTime(2026, 11, 26, 8, 0, 0, DateTimeKind.Utc) : null,
        CreatedAt = new DateTime(2026, 11, 26, 8, 0, 0, DateTimeKind.Utc),
        UpdatedAt = new DateTime(2026, 11, 26, 8, 0, 0, DateTimeKind.Utc)
    };
}
