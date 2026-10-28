using Aspirra.GoalPlannerService.Models;
using Microsoft.EntityFrameworkCore;

namespace Aspirra.GoalPlannerService.Data
{
    public class GoalPlannerDbContext : DbContext
    {
        public GoalPlannerDbContext(DbContextOptions<GoalPlannerDbContext> options) : base(options)
        {
        }

        public DbSet<PlanDraft> PlanDrafts => Set<PlanDraft>();
        public DbSet<Milestone> Milestones => Set<Milestone>();
        public DbSet<PlanTask> PlanTasks => Set<PlanTask>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("goal_planner");

            modelBuilder.Entity<PlanDraft>(entity =>
            {
                entity.ToTable("PlanDrafts");
                entity.HasKey(draft => draft.DraftId);
                entity.Property(draft => draft.DraftId).HasMaxLength(64);
                entity.Property(draft => draft.StudentId).IsRequired().HasMaxLength(64);
                entity.Property(draft => draft.GoalId).HasMaxLength(64);
                entity.Property(draft => draft.GoalText).IsRequired();
                entity.Property(draft => draft.Status).HasConversion<string>().IsRequired().HasMaxLength(20);
                entity.HasMany(draft => draft.Milestones)
                    .WithOne()
                    .HasForeignKey(milestone => milestone.DraftId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Milestone>(entity =>
            {
                entity.ToTable("Milestones");
                entity.HasKey(milestone => milestone.MilestoneId);
                entity.Property(milestone => milestone.MilestoneId).HasMaxLength(64);
                entity.Property(milestone => milestone.DraftId).IsRequired().HasMaxLength(64);
                entity.Property(milestone => milestone.Title).IsRequired();
                entity.HasMany(milestone => milestone.Tasks)
                    .WithOne()
                    .HasForeignKey(task => task.MilestoneId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<PlanTask>(entity =>
            {
                entity.ToTable("PlanTasks");
                entity.HasKey(task => task.TaskId);
                entity.Property(task => task.TaskId).HasMaxLength(64);
                entity.Property(task => task.MilestoneId).IsRequired().HasMaxLength(64);
                entity.Property(task => task.Title).IsRequired();
                entity.Property(task => task.DependsOnTaskId).HasMaxLength(64);
            });
        }
    }
}
