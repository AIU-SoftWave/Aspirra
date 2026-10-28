using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Aspirra.AssessmentService.Data;

public sealed class AssessmentDbContextFactory : IDesignTimeDbContextFactory<AssessmentDbContext>
{
    public AssessmentDbContext CreateDbContext(string[] args)
    {
        var options = new DbContextOptionsBuilder<AssessmentDbContext>()
            .UseNpgsql(
                "Host=localhost;Port=5432;Database=aspirra;Username=aspirra_user;Password=aspirra_password",
                npgsql => npgsql.MigrationsHistoryTable("__EFMigrationsHistory", "assessment"))
            .Options;

        return new AssessmentDbContext(options);
    }
}
