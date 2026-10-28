using System.Text.Json.Serialization;
using Aspirra.AcademicService.Data;
using Aspirra.AcademicService.Interfaces;
using Aspirra.AcademicService.Repositories;
using Aspirra.AcademicService.Services;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services
    .AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

var connectionString = builder.Configuration.GetConnectionString("Default")
    ?? "Host=aspirra-db;Port=5432;Database=aspirra;Username=aspirra_user;Password=aspirra_password";

builder.Services.AddDbContext<AcademicDbContext>(options =>
    options.UseNpgsql(
        connectionString,
        npgsql => npgsql.MigrationsHistoryTable("__EFMigrationsHistory", "academic")));
builder.Services.AddScoped<IGoalRepository, GoalRepository>();
builder.Services.AddScoped<IGoal, GoalService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(options =>
        options.WithTitle("Aspirra Academic Service API"));
}

app.MapControllers();

var logger = app.Services.GetRequiredService<ILoggerFactory>().CreateLogger("Startup");
for (var attempt = 1; ; attempt++)
{
    try
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<AcademicDbContext>();
        if (dbContext.Database.IsRelational())
        {
            await dbContext.Database.MigrateAsync();
        }

        break;
    }
    catch (Exception exception)
    {
        if (attempt >= 10)
        {
            logger.LogError(exception, "Failed to create the academic database after 10 attempts.");
            throw;
        }

        logger.LogWarning(exception, "Failed to create the academic database. Attempt {Attempt} of 10. Retrying in 3 seconds.", attempt);
        await Task.Delay(TimeSpan.FromSeconds(3));
    }
}

app.Run();

public partial class Program
{
}
