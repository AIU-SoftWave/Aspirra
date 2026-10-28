using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Aspirra.StudentService.Data;

public sealed class StudentDbContextFactory : IDesignTimeDbContextFactory<StudentDbContext>
{
    public StudentDbContext CreateDbContext(string[] args)
    {
        var options = new DbContextOptionsBuilder<StudentDbContext>()
            .UseNpgsql(
                "Host=localhost;Port=5432;Database=aspirra;Username=aspirra_user;Password=aspirra_password",
                npgsql => npgsql.MigrationsHistoryTable("__EFMigrationsHistory", "student"))
            .Options;

        return new StudentDbContext(options);
    }
}
