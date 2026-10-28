using Aspirra.CourseService.Models;
using Microsoft.EntityFrameworkCore;

namespace Aspirra.CourseService.Data;

public class CourseDbContext : DbContext
{
    public CourseDbContext(DbContextOptions<CourseDbContext> options) : base(options)
    {
    }

    public DbSet<Course> Courses => Set<Course>();
    public DbSet<CourseEnrollment> CourseEnrollments => Set<CourseEnrollment>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("course");

        modelBuilder.Entity<Course>(entity =>
        {
            entity.ToTable("Courses");
            entity.HasKey(c => c.CourseId);
            entity.Property(c => c.CourseId).HasMaxLength(64);
            entity.Property(c => c.StudentId).IsRequired().HasMaxLength(64);
            entity.Property(c => c.Name).IsRequired().HasMaxLength(200);
            entity.Property(c => c.Code).IsRequired().HasMaxLength(64);
            entity.Property(c => c.Instructor).HasMaxLength(200);
            entity.Property(c => c.Semester).IsRequired().HasMaxLength(64);
            entity.HasIndex(c => new { c.Code, c.Semester });
        });

        modelBuilder.Entity<CourseEnrollment>(entity =>
        {
            entity.ToTable("CourseEnrollments");
            entity.HasKey(e => e.EnrollmentId);
            entity.Property(e => e.EnrollmentId).HasMaxLength(64);
            entity.Property(e => e.CourseId).IsRequired().HasMaxLength(64);
            entity.Property(e => e.StudentId).IsRequired().HasMaxLength(64);
            entity.Property(e => e.Status).HasMaxLength(64);
            entity.HasOne<Course>()
                .WithMany()
                .HasForeignKey(e => e.CourseId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}
