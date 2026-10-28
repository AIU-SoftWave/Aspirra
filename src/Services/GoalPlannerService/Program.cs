using System.Text.Json.Serialization;
using Aspirra.GoalPlannerService.Clients;
using Aspirra.GoalPlannerService.Data;
using Aspirra.GoalPlannerService.Interfaces;
using Aspirra.GoalPlannerService.Repositories;
using Aspirra.GoalPlannerService.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddControllers()
    .AddJsonOptions(options =>
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

var connectionString = builder.Configuration.GetConnectionString("Default")
    ?? "Host=aspirra-db;Port=5432;Database=aspirra;Username=aspirra_user;Password=aspirra_password";

builder.Services.AddDbContext<GoalPlannerDbContext>(options =>
    options.UseNpgsql(
        connectionString,
        npgsql => npgsql.MigrationsHistoryTable("__EFMigrationsHistory", "goal_planner")));

builder.Services.AddScoped<IPlanDraftRepository, PlanDraftRepository>();
builder.Services.AddScoped<IGoalPlanner, GoalPlannerService>();
builder.Services.AddSingleton<IAIProvider, MockAIProvider>();

var academicService = builder.Configuration["Services:AcademicService"]
    ?? "http://academic-service:8080";

builder.Services.AddHttpClient<IGoalClient, HttpGoalClient>(client =>
{
    client.BaseAddress = new Uri(academicService.TrimEnd('/') + "/");
});

var app = builder.Build();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<GoalPlannerDbContext>();
    const int maxAttempts = 10;

    for (var attempt = 1; ; attempt++)
    {
        try
        {
            await dbContext.Database.MigrateAsync();
            break;
        }
        catch (Exception exception) when (attempt < maxAttempts)
        {
            app.Logger.LogWarning(
                exception,
                "Database initialization attempt {Attempt} of {MaxAttempts} failed. Retrying in 3 seconds.",
                attempt,
                maxAttempts);
            await Task.Delay(TimeSpan.FromSeconds(3));
        }
    }
}

app.Run();

public partial class Program
{
}
