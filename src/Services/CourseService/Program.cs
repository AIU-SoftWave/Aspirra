using Aspirra.CourseService.Data;
using Aspirra.CourseService.Interfaces;
using Aspirra.CourseService.Repositories;
using Aspirra.CourseService.Services;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

var connectionString = builder.Configuration.GetConnectionString("Default")
    ?? "Host=aspirra-db;Port=5432;Database=aspirra;Username=aspirra_user;Password=aspirra_password";

builder.Services.AddDbContext<CourseDbContext>(options =>
    options.UseNpgsql(
        connectionString,
        npgsql => npgsql.MigrationsHistoryTable("__EFMigrationsHistory", "course")));

builder.Services.AddScoped<ICourseRepository, CourseRepository>();
builder.Services.AddScoped<ICourse, CourseService>();

var assessmentServiceUrl = builder.Configuration["Services:AssessmentService"]
    ?? "http://assessment-service:8080";

builder.Services.AddHttpClient<IAssessmentClient, HttpAssessmentClient>(client =>
{
    client.BaseAddress = new Uri(assessmentServiceUrl);
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(options =>
        options.WithTitle("Aspirra Course Service API"));
}

app.MapControllers();
app.MapGet("/health", () => Results.Ok(new { status = "healthy" }));

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
            var db = scope.ServiceProvider.GetRequiredService<CourseDbContext>();
            await db.Database.MigrateAsync();
            return;
        }
        catch (Exception ex)
        {
            if (attempt == maxAttempts)
            {
                app.Logger.LogError(ex, "Database initialization failed after {Attempts} attempts", maxAttempts);
                throw;
            }

            app.Logger.LogWarning(
                ex,
                "Database initialization attempt {Attempt} of {MaxAttempts} failed; retrying in {DelaySeconds}s",
                attempt,
                maxAttempts,
                delay.TotalSeconds);

            await Task.Delay(delay);
        }
    }
}

public partial class Program
{
}
