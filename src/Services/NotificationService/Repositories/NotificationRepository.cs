using Aspirra.NotificationService.Data;
using Aspirra.NotificationService.Interfaces;
using Aspirra.NotificationService.Models;
using Microsoft.EntityFrameworkCore;

namespace Aspirra.NotificationService.Repositories;

public class NotificationRepository : INotificationRepository
{
    private readonly NotificationDbContext _context;

    public NotificationRepository(NotificationDbContext context)
    {
        _context = context;
    }

    public async Task<Notification> AddAsync(Notification notification, CancellationToken cancellationToken = default)
    {
        await _context.Notifications.AddAsync(notification, cancellationToken);
        return notification;
    }

    public Task<Notification?> GetByIdAsync(string notificationId, CancellationToken cancellationToken = default) =>
        _context.Notifications.FirstOrDefaultAsync(
            notification => notification.NotificationId == notificationId,
            cancellationToken);

    public Task SaveChangesAsync(CancellationToken cancellationToken = default) =>
        _context.SaveChangesAsync(cancellationToken);
}
