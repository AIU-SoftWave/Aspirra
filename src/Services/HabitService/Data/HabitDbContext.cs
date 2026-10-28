using Aspirra.HabitService.Models;
using Microsoft.EntityFrameworkCore;

namespace Aspirra.HabitService.Data;

public class HabitDbContext : DbContext
{
    public HabitDbContext(DbContextOptions<HabitDbContext> options) : base(options)
    {
    }

    public DbSet<Habit> Habits => Set<Habit>();
    public DbSet<HabitLog> HabitLogs => Set<HabitLog>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("habit");

        modelBuilder.Entity<Habit>(entity =>
        {
            entity.ToTable("Habits");
            entity.HasKey(habit => habit.HabitId);
            entity.Property(habit => habit.HabitId).HasMaxLength(64);
            entity.Property(habit => habit.StudentId).IsRequired().HasMaxLength(64);
            entity.Property(habit => habit.GoalId).HasMaxLength(64);
            entity.Property(habit => habit.Name).IsRequired().HasMaxLength(200);
            entity.Property(habit => habit.Frequency).HasConversion<string>().IsRequired().HasMaxLength(20);
            entity.Property(habit => habit.GoalTarget).HasMaxLength(500);
            entity.HasMany(habit => habit.HabitLogs)
                .WithOne(log => log.Habit)
                .HasForeignKey(log => log.HabitId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<HabitLog>(entity =>
        {
            entity.ToTable("HabitLogs");
            entity.HasKey(log => log.LogId);
            entity.Property(log => log.LogId).HasMaxLength(64);
            entity.Property(log => log.HabitId).IsRequired().HasMaxLength(64);
            entity.HasIndex(log => new { log.HabitId, log.LogDate }).IsUnique();
        });
    }
}
