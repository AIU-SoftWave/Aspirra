using System.Text.Json.Serialization;
using Aspirra.AssessmentService.Data;
using Aspirra.AssessmentService.Interfaces;
using Aspirra.AssessmentService.Json;
using Aspirra.AssessmentService.Repositories;
using Aspirra.AssessmentService.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new TimeOnlyJsonConverter());
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

var connectionString = builder.Configuration.GetConnectionString("Default")
    ?? "Host=aspirra-db;Port=5432;Database=aspirra;Username=aspirra_user;Password=aspirra_password";

builder.Services.AddDbContext<AssessmentDbContext>(options =>
    options.UseNpgsql(
        connectionString,
        npgsql => npgsql.MigrationsHistoryTable("__EFMigrationsHistory", "assessment")));
builder.Services.AddScoped<IAssessmentRepository, EfAssessmentRepository>();
builder.Services.AddScoped<IAssessment, AssessmentService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
    var dbContext = scope.ServiceProvider.GetRequiredService<AssessmentDbContext>();

    for (var attempt = 1; attempt <= 10; attempt++)
    {
        try
        {
            await dbContext.Database.MigrateAsync();
            break;
        }
        catch (Exception ex)
        {
            logger.LogWarning(ex, "Database initialization attempt {Attempt} of 10 failed. Retrying in 3 seconds.", attempt);

            if (attempt == 10)
            {
                throw;
            }

            await Task.Delay(TimeSpan.FromSeconds(3));
        }
    }
}

app.MapControllers();

app.Run();

public partial class Program
{
}
