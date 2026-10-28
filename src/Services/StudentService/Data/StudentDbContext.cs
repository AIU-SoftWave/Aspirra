using Aspirra.StudentService.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Aspirra.StudentService.Data
{
    public class StudentDbContext : DbContext
    {
        public StudentDbContext(DbContextOptions<StudentDbContext> options) : base(options)
        {
        }

        public DbSet<Profile> Profiles => Set<Profile>();
        public DbSet<TimeSlot> TimeSlots => Set<TimeSlot>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("student");

            var listConverter = new ValueConverter<List<string>?, string>(
                value => value == null ? string.Empty : string.Join(';', value),
                value => string.IsNullOrEmpty(value)
                    ? new List<string>()
                    : value.Split(';', StringSplitOptions.None).ToList());

            var listComparer = new ValueComparer<List<string>?>(
                (left, right) => (left == null && right == null) ||
                                 (left != null && right != null && left.SequenceEqual(right)),
                value => value == null
                    ? 0
                    : value.Aggregate(0, (hash, item) => HashCode.Combine(hash, item.GetHashCode())),
                value => value == null ? null : value.ToList());

            modelBuilder.Entity<Profile>(entity =>
            {
                entity.ToTable("Profiles");
                entity.HasKey(p => p.StudentId);
                entity.Property(p => p.StudentId).HasMaxLength(50);
                entity.Property(p => p.AcademicLevel).IsRequired().HasMaxLength(50);
                entity.Property(p => p.Major).IsRequired().HasMaxLength(100);
                entity.Property(p => p.Interests)
                    .HasConversion(listConverter)
                    .Metadata.SetValueComparer(listComparer);
                entity.Property(p => p.Skills)
                    .HasConversion(listConverter)
                    .Metadata.SetValueComparer(listComparer);
                entity.Property(p => p.CareerInterests)
                    .HasConversion(listConverter)
                    .Metadata.SetValueComparer(listComparer);
                entity.Property(p => p.CreatedAt).IsRequired();
                entity.Property(p => p.UpdatedAt).IsRequired();
                entity.HasMany(p => p.Availability)
                    .WithOne()
                    .HasForeignKey(t => t.StudentId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<TimeSlot>(entity =>
            {
                entity.ToTable("TimeSlots");
                entity.HasKey(t => t.TimeSlotId);
                entity.Property(t => t.TimeSlotId).HasMaxLength(50);
                entity.Property(t => t.StudentId).IsRequired().HasMaxLength(50);
                entity.Property(t => t.DayOfWeek).HasMaxLength(20);
                entity.Property(t => t.StartTime).HasColumnType("time");
                entity.Property(t => t.EndTime).HasColumnType("time");
            });
        }
    }
}
