using Aspirra.StudentService.Data;
using Aspirra.StudentService.Interfaces;
using Aspirra.StudentService.Repositories;
using Aspirra.StudentService.Services;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

var connectionString = builder.Configuration.GetConnectionString("Default")
    ?? "Host=aspirra-db;Port=5432;Database=aspirra;Username=aspirra_user;Password=aspirra_password";

builder.Services.AddDbContext<StudentDbContext>(options =>
    options.UseNpgsql(
        connectionString,
        npgsql => npgsql.MigrationsHistoryTable("__EFMigrationsHistory", "student")));

builder.Services.AddScoped<IProfileRepository, ProfileRepository>();
builder.Services.AddScoped<IStudentProfile, StudentProfileService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(options =>
        options.WithTitle("Aspirra Student Service API"));
}

app.MapControllers();

await EnsureDatabaseAsync(app);

app.Run();

static async Task EnsureDatabaseAsync(WebApplication app)
{
    const int maxAttempts = 10;
    var delay = TimeSpan.FromSeconds(3);

    for (var attempt = 1; attempt <= maxAttempts; attempt++)
    {
        try
        {
            using var scope = app.Services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<StudentDbContext>();
            await db.Database.MigrateAsync();
            return;
        }
        catch (Exception ex)
        {
            app.Logger.LogWarning(
                ex,
                "Database initialization attempt {Attempt} of {MaxAttempts} failed; retrying in {DelaySeconds}s",
                attempt,
                maxAttempts,
                delay.TotalSeconds);

            if (attempt < maxAttempts)
            {
                await Task.Delay(delay);
            }
        }
    }

    throw new InvalidOperationException(
        $"Database initialization failed after {maxAttempts} attempts");
}

public partial class Program
{
}
