using Aspirra.AcademicService.Models;
using Microsoft.EntityFrameworkCore;

namespace Aspirra.AcademicService.Data
{
    public class AcademicDbContext : DbContext
    {
        public AcademicDbContext(DbContextOptions<AcademicDbContext> options) : base(options)
        {
        }

        public DbSet<Goal> Goals => Set<Goal>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("academic");

            var goal = modelBuilder.Entity<Goal>();
            goal.ToTable("Goals");
            goal.HasKey(g => g.GoalId);
            goal.Property(g => g.GoalId).HasMaxLength(50);
            goal.Property(g => g.StudentId).IsRequired().HasMaxLength(50);
            goal.Property(g => g.Title).IsRequired().HasMaxLength(200);
            goal.Property(g => g.Description).HasColumnType("text");
            goal.Property(g => g.TargetDate).IsRequired();
            goal.Property(g => g.Priority).IsRequired().HasMaxLength(20);
            goal.Property(g => g.Status).HasConversion<string>().HasMaxLength(20);
            goal.Property(g => g.CompletionDate);
            goal.Property(g => g.CreatedAt).IsRequired();
            goal.Property(g => g.UpdatedAt).IsRequired();
        }
    }
}
