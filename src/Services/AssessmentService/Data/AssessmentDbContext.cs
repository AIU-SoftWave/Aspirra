using Aspirra.AssessmentService.Models;
using Microsoft.EntityFrameworkCore;

namespace Aspirra.AssessmentService.Data
{
    public class AssessmentDbContext : DbContext
    {
        public AssessmentDbContext(DbContextOptions<AssessmentDbContext> options)
            : base(options)
        {
        }

        public DbSet<Assignment> Assignments => Set<Assignment>();
        public DbSet<Exam> Exams => Set<Exam>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("assessment");

            modelBuilder.Entity<Assignment>(entity =>
            {
                entity.ToTable("Assignments");
                entity.HasKey(x => x.AssignmentId);
                entity.Property(x => x.AssignmentId).HasMaxLength(64);
                entity.Property(x => x.CourseId).IsRequired().HasMaxLength(64);
                entity.Property(x => x.StudentId).HasMaxLength(64);
                entity.Property(x => x.Title).IsRequired().HasMaxLength(200);
                entity.Property(x => x.Description).HasMaxLength(2000);
                entity.Property(x => x.Priority).HasMaxLength(50);
                entity.Property(x => x.Status).HasConversion<string>().HasMaxLength(20);
                entity.Property(x => x.DueDate).HasColumnType("date");
                entity.Property(x => x.CreatedAt).HasColumnType("timestamp with time zone");
                entity.Property(x => x.UpdatedAt).HasColumnType("timestamp with time zone");
            });

            modelBuilder.Entity<Exam>(entity =>
            {
                entity.ToTable("Exams");
                entity.HasKey(x => x.ExamId);
                entity.Property(x => x.ExamId).HasMaxLength(64);
                entity.Property(x => x.CourseId).IsRequired().HasMaxLength(64);
                entity.Property(x => x.StudentId).HasMaxLength(64);
                entity.Property(x => x.Title).IsRequired().HasMaxLength(200);
                entity.Property(x => x.Location).HasMaxLength(200);
                entity.Property(x => x.ExamDate).HasColumnType("date");
                entity.Property(x => x.ExamTime).HasColumnType("time without time zone");
                entity.Property(x => x.CreatedAt).HasColumnType("timestamp with time zone");
                entity.Property(x => x.UpdatedAt).HasColumnType("timestamp with time zone");
            });
        }
    }
}
