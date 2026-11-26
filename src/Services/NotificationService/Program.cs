using System.Text.Json.Serialization;
using Aspirra.NotificationService.Clients;
using Aspirra.NotificationService.Data;
using Aspirra.NotificationService.Gateways;
using Aspirra.NotificationService.Interfaces;
using Aspirra.NotificationService.Repositories;
using Aspirra.NotificationService.Services;
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

var connectionString = builder.Configuration.GetConnectionString("Default");
if (string.IsNullOrWhiteSpace(connectionString))
{
    connectionString = "Host=aspirra-db;Port=5432;Database=aspirra;Username=aspirra_user;Password=aspirra_password";
}

builder.Services.AddDbContext<NotificationDbContext>(options =>
    options.UseNpgsql(
        connectionString,
        npgsql => npgsql.MigrationsHistoryTable("__EFMigrationsHistory", "notification")));
builder.Services.AddScoped<INotificationRepository, NotificationRepository>();
builder.Services.AddScoped<IEmailGateway, LoggingEmailGateway>();
builder.Services.AddScoped<INotification, NotificationService>();

var assessmentServiceUrl = builder.Configuration["Services:AssessmentService"]
    ?? "http://assessment-service:8080";

builder.Services.AddHttpClient<IAssessment, HttpAssessmentClient>(client =>
{
    client.BaseAddress = new Uri(assessmentServiceUrl);
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(options =>
        options.WithTitle("Aspirra Notification Service API"));
}

app.MapControllers();

for (var attempt = 1; attempt <= 10; attempt++)
{
    try
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<NotificationDbContext>();
        if (dbContext.Database.IsRelational())
        {
            await dbContext.Database.MigrateAsync();
        }

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
