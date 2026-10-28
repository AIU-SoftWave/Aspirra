using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Aspirra.CourseService.Data;

public sealed class CourseDbContextFactory : IDesignTimeDbContextFactory<CourseDbContext>
{
    public CourseDbContext CreateDbContext(string[] args)
    {
        var options = new DbContextOptionsBuilder<CourseDbContext>()
            .UseNpgsql(
                "Host=localhost;Port=5432;Database=aspirra;Username=aspirra_user;Password=aspirra_password",
                npgsql => npgsql.MigrationsHistoryTable("__EFMigrationsHistory", "course"))
            .Options;

        return new CourseDbContext(options);
    }
}
