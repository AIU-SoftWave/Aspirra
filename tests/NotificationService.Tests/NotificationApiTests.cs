using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Aspirra.NotificationService.Data;
using Aspirra.NotificationService.Dtos;
using Aspirra.NotificationService.Interfaces;
using Aspirra.NotificationService.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moq;

namespace Aspirra.NotificationService.Tests;

public class NotificationApiTests
{
    private sealed class NotificationApiFactory : WebApplicationFactory<Program>
    {
        private readonly string _databaseName = $"aspirra-notification-api-{Guid.NewGuid():N}";

        public Mock<IAssessment> Assessment { get; } = new();
        public Mock<IEmailGateway> EmailGateway { get; } = new();

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("Testing");
            builder.ConfigureServices(services =>
            {
                var registrations = services
                    .Where(descriptor =>
                        descriptor.ServiceType == typeof(DbContextOptions) ||
                        descriptor.ServiceType == typeof(DbContextOptions<NotificationDbContext>) ||
                        (descriptor.ServiceType.IsGenericType &&
                         descriptor.ServiceType.GetGenericArguments().Contains(typeof(NotificationDbContext))))
                    .ToList();

                foreach (var registration in registrations)
                {
                    services.Remove(registration);
                }

                services.AddDbContext<NotificationDbContext>(options =>
                    options.UseInMemoryDatabase(_databaseName));

                services.RemoveAll<IAssessment>();
                services.RemoveAll<IEmailGateway>();
                services.AddSingleton(Assessment.Object);
                services.AddSingleton(EmailGateway.Object);
            });
        }
    }

    private static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web);

    private static object ReminderPayload => new
    {
        studentId = NotificationFixtures.StudentId,
        email = NotificationFixtures.Email,
        days = NotificationFixtures.Days
    };

    private static NotificationApiFactory CreateFactory(bool emailFailsFirst = false)
    {
        var factory = new NotificationApiFactory();
        factory.Assessment
            .Setup(a => a.GetDeadlinesAsync(NotificationFixtures.StudentId, NotificationFixtures.Days, It.IsAny<CancellationToken>()))
            .ReturnsAsync(() => new List<Deadline> { NotificationFixtures.DatabaseProjectDeadline() });

        if (emailFailsFirst)
        {
            factory.EmailGateway
                .SetupSequence(g => g.SendAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new HttpRequestException("SMTP server unavailable"))
                .Returns(Task.CompletedTask);
        }
        else
        {
            factory.EmailGateway
                .Setup(g => g.SendAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);
        }

        return factory;
    }

    private static async Task<string?> ReadCodeAsync(HttpResponseMessage response)
    {
        using var document = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
        return document.RootElement.GetProperty("code").GetString();
    }

    [Fact]
    public async Task Health_ReturnsHealthy()
    {
        using var factory = CreateFactory();
        using var client = factory.CreateClient();

        var response = await client.GetAsync("/health");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task SendDeadlineReminders_ReturnsSentNotificationThatCanBeRetrieved()
    {
        using var factory = CreateFactory();
        using var client = factory.CreateClient();

        var response = await client.PostAsJsonAsync("/api/notifications/deadline-reminders", ReminderPayload);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var notifications = await response.Content.ReadFromJsonAsync<List<NotificationResponse>>(JsonOptions);
        var notification = Assert.Single(notifications!);
        Assert.Equal("Sent", notification.Status);

        var getResponse = await client.GetAsync($"/api/notifications/{notification.NotificationId}");
        Assert.Equal(HttpStatusCode.OK, getResponse.StatusCode);
        factory.EmailGateway.Verify(
            g => g.SendAsync(NotificationFixtures.Email, It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task FailedReminder_CanBeRetriedOnce_ThenRetryIsRejected()
    {
        using var factory = CreateFactory(emailFailsFirst: true);
        using var client = factory.CreateClient();

        var sendResponse = await client.PostAsJsonAsync("/api/notifications/deadline-reminders", ReminderPayload);
        var failed = Assert.Single((await sendResponse.Content.ReadFromJsonAsync<List<NotificationResponse>>(JsonOptions))!);
        Assert.Equal("Failed", failed.Status);

        var retryResponse = await client.PostAsync($"/api/notifications/{failed.NotificationId}/retry", null);
        Assert.Equal(HttpStatusCode.OK, retryResponse.StatusCode);
        var retried = await retryResponse.Content.ReadFromJsonAsync<NotificationResponse>(JsonOptions);
        Assert.Equal("Sent", retried!.Status);
        Assert.Equal(2, retried.Attempts);

        var secondRetry = await client.PostAsync($"/api/notifications/{failed.NotificationId}/retry", null);
        Assert.Equal(HttpStatusCode.BadRequest, secondRetry.StatusCode);
        Assert.Equal("INVALID_TRANSITION", await ReadCodeAsync(secondRetry));
    }

    [Fact]
    public async Task Retry_WhenEmailFailsAgain_ReturnsBadGateway()
    {
        using var factory = CreateFactory();
        factory.EmailGateway
            .Setup(g => g.SendAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new HttpRequestException("SMTP server unavailable"));
        using var client = factory.CreateClient();

        var sendResponse = await client.PostAsJsonAsync("/api/notifications/deadline-reminders", ReminderPayload);
        var failed = Assert.Single((await sendResponse.Content.ReadFromJsonAsync<List<NotificationResponse>>(JsonOptions))!);

        var retryResponse = await client.PostAsync($"/api/notifications/{failed.NotificationId}/retry", null);

        Assert.Equal(HttpStatusCode.BadGateway, retryResponse.StatusCode);
        Assert.Equal("SEND_FAILED", await ReadCodeAsync(retryResponse));
    }

    [Fact]
    public async Task Retry_WhenMissing_ReturnsNotFound()
    {
        using var factory = CreateFactory();
        using var client = factory.CreateClient();

        var response = await client.PostAsync("/api/notifications/missing/retry", null);

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        Assert.Equal("NOT_FOUND", await ReadCodeAsync(response));
    }

    [Fact]
    public async Task GetNotification_WhenMissing_ReturnsNotFound()
    {
        using var factory = CreateFactory();
        using var client = factory.CreateClient();

        var response = await client.GetAsync("/api/notifications/missing");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task SendDeadlineReminders_WithoutEmail_ReturnsBadRequest()
    {
        using var factory = CreateFactory();
        using var client = factory.CreateClient();

        var response = await client.PostAsJsonAsync(
            "/api/notifications/deadline-reminders",
            new { studentId = NotificationFixtures.StudentId });

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.Equal("REQUIRED_FIELD", await ReadCodeAsync(response));
    }
}
