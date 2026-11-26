using Aspirra.NotificationService.Common;
using Aspirra.NotificationService.Models;

namespace Aspirra.NotificationService.Interfaces;

public interface INotification
{
    Task<ServiceResult<List<Notification>>> SendDeadlineRemindersAsync(string studentId, string email, int days, CancellationToken cancellationToken = default);
    Task<ServiceResult<Notification>> RetryAsync(string notificationId, CancellationToken cancellationToken = default);
    Task<ServiceResult<Notification>> GetNotificationAsync(string notificationId, CancellationToken cancellationToken = default);
}
