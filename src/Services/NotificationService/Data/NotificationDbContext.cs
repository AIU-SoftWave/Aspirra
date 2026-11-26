using Aspirra.NotificationService.Models;
using Microsoft.EntityFrameworkCore;

namespace Aspirra.NotificationService.Data;

public class NotificationDbContext : DbContext
{
    public NotificationDbContext(DbContextOptions<NotificationDbContext> options) : base(options)
    {
    }

    public DbSet<Notification> Notifications => Set<Notification>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("notification");

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.ToTable("Notifications");
            entity.HasKey(notification => notification.NotificationId);
            entity.Property(notification => notification.NotificationId).HasMaxLength(64);
            entity.Property(notification => notification.StudentId).IsRequired().HasMaxLength(64);
            entity.Property(notification => notification.AssessmentId).IsRequired().HasMaxLength(64);
            entity.Property(notification => notification.Recipient).IsRequired().HasMaxLength(254);
            entity.Property(notification => notification.Type).IsRequired().HasMaxLength(50);
            entity.Property(notification => notification.Subject).IsRequired().HasMaxLength(300);
            entity.Property(notification => notification.Message).IsRequired().HasMaxLength(1000);
            entity.Property(notification => notification.Status).HasConversion<string>().IsRequired().HasMaxLength(20);
            entity.HasIndex(notification => notification.StudentId);
        });
    }
}
