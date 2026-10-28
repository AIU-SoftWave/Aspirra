using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Aspirra.HabitService.Data;

public sealed class HabitDbContextFactory : IDesignTimeDbContextFactory<HabitDbContext>
{
    public HabitDbContext CreateDbContext(string[] args)
    {
        var options = new DbContextOptionsBuilder<HabitDbContext>()
            .UseNpgsql(
                "Host=localhost;Port=5432;Database=aspirra;Username=aspirra_user;Password=aspirra_password",
                npgsql => npgsql.MigrationsHistoryTable("__EFMigrationsHistory", "habit"))
            .Options;

        return new HabitDbContext(options);
    }
}
