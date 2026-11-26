using Aspirra.NotificationService.Common;
using Aspirra.NotificationService.Interfaces;
using Aspirra.NotificationService.Models;

namespace Aspirra.NotificationService.Services;

public class NotificationService : INotification
{
    public const int MinDays = 1;
    public const int MaxDays = 30;

    private readonly IAssessment _assessment;
    private readonly IEmailGateway _emailGateway;
    private readonly INotificationRepository _repository;

    public NotificationService(
        IAssessment assessment,
        IEmailGateway emailGateway,
        INotificationRepository repository)
    {
        _assessment = assessment;
        _emailGateway = emailGateway;
        _repository = repository;
    }

    public async Task<ServiceResult<List<Notification>>> SendDeadlineRemindersAsync(
        string studentId,
        string email,
        int days,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(studentId))
        {
            return ServiceResult<List<Notification>>.Fail(NotificationErrorCodes.RequiredField, "StudentId is required.");
        }

        if (string.IsNullOrWhiteSpace(email) || !email.Contains('@'))
        {
            return ServiceResult<List<Notification>>.Fail(NotificationErrorCodes.RequiredField, "A valid email is required.");
        }

        if (days < MinDays || days > MaxDays)
        {
            return ServiceResult<List<Notification>>.Fail(NotificationErrorCodes.InvalidDays, "Days must be between 1 and 30.");
        }

        var deadlines = await _assessment.GetDeadlinesAsync(studentId.Trim(), days, cancellationToken);
        var notifications = new List<Notification>();

        foreach (var deadline in deadlines.Where(deadline => !deadline.IsCompleted))
        {
            var now = DateTime.UtcNow;
            var notification = new Notification
            {
                NotificationId = Guid.NewGuid().ToString(),
                StudentId = studentId.Trim(),
                AssessmentId = deadline.AssessmentId,
                Recipient = email.Trim(),
                Type = Notification.DeadlineReminderType,
                Subject = $"Reminder: {deadline.Title} is due on {deadline.DueDate:yyyy-MM-dd}",
                Message = $"Your {deadline.Kind.ToLowerInvariant()} \"{deadline.Title}\" is due on {deadline.DueDate:yyyy-MM-dd}.",
                Status = NotificationStatus.Pending,
                CreatedAt = now,
                UpdatedAt = now
            };

            await _repository.AddAsync(notification, cancellationToken);
            await TrySendAsync(notification, cancellationToken);
            notifications.Add(notification);
        }

        if (notifications.Count > 0)
        {
            await _repository.SaveChangesAsync(cancellationToken);
        }

        return ServiceResult<List<Notification>>.Ok(notifications);
    }

    public async Task<ServiceResult<Notification>> RetryAsync(
        string notificationId,
        CancellationToken cancellationToken = default)
    {
        var notification = string.IsNullOrWhiteSpace(notificationId)
            ? null
            : await _repository.GetByIdAsync(notificationId, cancellationToken);

        if (notification is null)
        {
            return ServiceResult<Notification>.Fail(NotificationErrorCodes.NotFound, "Notification not found");
        }

        if (!notification.CanRetry)
        {
            return ServiceResult<Notification>.Fail(
                NotificationErrorCodes.InvalidTransition,
                "Only a failed notification can be retried.");
        }

        var sent = await TrySendAsync(notification, cancellationToken);
        await _repository.SaveChangesAsync(cancellationToken);

        return sent
            ? ServiceResult<Notification>.Ok(notification)
            : ServiceResult<Notification>.Fail(NotificationErrorCodes.SendFailed, "The email could not be sent.");
    }

    public async Task<ServiceResult<Notification>> GetNotificationAsync(
        string notificationId,
        CancellationToken cancellationToken = default)
    {
        var notification = string.IsNullOrWhiteSpace(notificationId)
            ? null
            : await _repository.GetByIdAsync(notificationId, cancellationToken);

        return notification is null
            ? ServiceResult<Notification>.Fail(NotificationErrorCodes.NotFound, "Notification not found")
            : ServiceResult<Notification>.Ok(notification);
    }

    private async Task<bool> TrySendAsync(Notification notification, CancellationToken cancellationToken)
    {
        try
        {
            await _emailGateway.SendAsync(
                notification.Recipient,
                notification.Subject,
                notification.Message,
                cancellationToken);
            notification.MarkSent(DateTime.UtcNow);
            return true;
        }
        catch (Exception exception) when (exception is not OperationCanceledException)
        {
            notification.MarkFailed(DateTime.UtcNow);
            return false;
        }
    }
}
