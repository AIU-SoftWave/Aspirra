using Aspirra.SkillService.Models;
using Microsoft.EntityFrameworkCore;

namespace Aspirra.SkillService.Data;

public class SkillDbContext : DbContext
{
    public SkillDbContext(DbContextOptions<SkillDbContext> options) : base(options)
    {
    }

    public DbSet<Skill> Skills => Set<Skill>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("skill");

        modelBuilder.Entity<Skill>(entity =>
        {
            entity.ToTable("Skills");
            entity.HasKey(skill => skill.SkillId);
            entity.Property(skill => skill.SkillId).HasMaxLength(64);
            entity.Property(skill => skill.StudentId).IsRequired().HasMaxLength(64);
            entity.Property(skill => skill.Name).IsRequired().HasMaxLength(100);
            entity.Property(skill => skill.Type).HasConversion<string>().IsRequired().HasMaxLength(20);
            entity.Property(skill => skill.Status).HasConversion<string>().IsRequired().HasMaxLength(20);
            entity.HasIndex(skill => new { skill.StudentId, skill.Name }).IsUnique();
        });
    }
}
