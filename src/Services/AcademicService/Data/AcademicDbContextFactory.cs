using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Aspirra.AcademicService.Data;

public sealed class AcademicDbContextFactory : IDesignTimeDbContextFactory<AcademicDbContext>
{
    public AcademicDbContext CreateDbContext(string[] args)
    {
        var options = new DbContextOptionsBuilder<AcademicDbContext>()
            .UseNpgsql(
                "Host=localhost;Port=5432;Database=aspirra;Username=aspirra_user;Password=aspirra_password",
                npgsql => npgsql.MigrationsHistoryTable("__EFMigrationsHistory", "academic"))
            .Options;

        return new AcademicDbContext(options);
    }
}
