using Aspirra.HabitService.Data;
using Aspirra.HabitService.Interfaces;
using Aspirra.HabitService.Repositories;
using Aspirra.HabitService.Services;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

var connectionString = builder.Configuration.GetConnectionString("Default");
if (string.IsNullOrWhiteSpace(connectionString))
{
    connectionString = "Host=aspirra-db;Port=5432;Database=aspirra;Username=aspirra_user;Password=aspirra_password";
}

builder.Services.AddDbContext<HabitDbContext>(options =>
    options.UseNpgsql(
        connectionString,
        npgsql => npgsql.MigrationsHistoryTable("__EFMigrationsHistory", "habit")));
builder.Services.AddScoped<IHabitRepository, HabitRepository>();
builder.Services.AddScoped<IHabit, HabitService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(options =>
        options.WithTitle("Aspirra Habit Service API"));
}

app.MapControllers();

for (var attempt = 1; attempt <= 10; attempt++)
{
    try
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<HabitDbContext>();
        await dbContext.Database.MigrateAsync();
        break;
    }
    catch (Exception exception)
    {
        app.Logger.LogWarning(
            exception,
            "Database initialization attempt {Attempt} of 10 failed.",
            attempt);

        if (attempt < 10)
        {
            await Task.Delay(TimeSpan.FromSeconds(3));
        }
    }
}

app.Run();

public partial class Program
{
}
