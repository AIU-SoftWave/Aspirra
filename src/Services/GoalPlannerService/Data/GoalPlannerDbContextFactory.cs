using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Aspirra.GoalPlannerService.Data;

public sealed class GoalPlannerDbContextFactory : IDesignTimeDbContextFactory<GoalPlannerDbContext>
{
    public GoalPlannerDbContext CreateDbContext(string[] args)
    {
        var options = new DbContextOptionsBuilder<GoalPlannerDbContext>()
            .UseNpgsql(
                "Host=localhost;Port=5432;Database=aspirra;Username=aspirra_user;Password=aspirra_password",
                npgsql => npgsql.MigrationsHistoryTable("__EFMigrationsHistory", "goal_planner"))
            .Options;

        return new GoalPlannerDbContext(options);
    }
}
