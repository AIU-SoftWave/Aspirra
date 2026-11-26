using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Aspirra.SkillService.Data;

public sealed class SkillDbContextFactory : IDesignTimeDbContextFactory<SkillDbContext>
{
    public SkillDbContext CreateDbContext(string[] args)
    {
        var options = new DbContextOptionsBuilder<SkillDbContext>()
            .UseNpgsql(
                "Host=localhost;Port=5432;Database=aspirra;Username=aspirra_user;Password=aspirra_password",
                npgsql => npgsql.MigrationsHistoryTable("__EFMigrationsHistory", "skill"))
            .Options;

        return new SkillDbContext(options);
    }
}
