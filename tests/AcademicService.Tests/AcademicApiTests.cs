using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using Aspirra.AcademicService.Data;
using Aspirra.AcademicService.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Aspirra.AcademicService.Tests;

public class AcademicApiTests
{
    private sealed class AcademicApiFactory : WebApplicationFactory<Program>
    {
        private readonly string _databaseName = $"aspirra-academic-api-{Guid.NewGuid():N}";

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("Testing");
            builder.ConfigureServices(services =>
            {
                var registrations = services
                    .Where(descriptor =>
                        descriptor.ServiceType == typeof(DbContextOptions) ||
                        descriptor.ServiceType == typeof(DbContextOptions<AcademicDbContext>) ||
                        (descriptor.ServiceType.IsGenericType &&
                         descriptor.ServiceType.GetGenericArguments().Contains(typeof(AcademicDbContext))))
                    .ToList();

                foreach (var registration in registrations)
                {
                    services.Remove(registration);
                }

                services.AddDbContext<AcademicDbContext>(options =>
                    options.UseInMemoryDatabase(_databaseName));
            });
        }
    }

    private static string FutureDate => DateOnly.FromDateTime(DateTime.UtcNow).AddDays(30).ToString("yyyy-MM-dd");

    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        Converters = { new JsonStringEnumConverter() }
    };

    [Fact]
    public async Task TC_02_1_01_Health_ReturnsHealthy()
    {
        using var factory = new AcademicApiFactory();
        using var client = factory.CreateClient();

        var response = await client.GetAsync("/health");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        using var document = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
        Assert.Equal("healthy", document.RootElement.GetProperty("status").GetString());
    }

    [Fact]
    public async Task TC_02_1_02_CreateGoal_ReturnsCreatedGoalAndCanBeRetrieved()
    {
        using var factory = new AcademicApiFactory();
        using var client = factory.CreateClient();
        var payload = new
        {
            studentId = "student-1",
            title = "Prepare for finals",
            description = "Review every course",
            targetDate = FutureDate,
            priority = "High"
        };

        var createResponse = await client.PostAsJsonAsync("/api/goals", payload);

        Assert.Equal(HttpStatusCode.Created, createResponse.StatusCode);
        var created = await createResponse.Content.ReadFromJsonAsync<Goal>(JsonOptions);
        Assert.NotNull(created);
        Assert.Equal(GoalStatus.Active, created!.Status);
        Assert.NotNull(createResponse.Headers.Location);

        var getResponse = await client.GetAsync($"/api/goals/{created.GoalId}");
        Assert.Equal(HttpStatusCode.OK, getResponse.StatusCode);
        var fetched = await getResponse.Content.ReadFromJsonAsync<Goal>(JsonOptions);
        Assert.Equal(created.GoalId, fetched!.GoalId);
        Assert.Equal("Prepare for finals", fetched.Title);
    }

    [Fact]
    public async Task TC_02_1_03_CreateGoal_MissingTitle_ReturnsRequiredFieldPayload()
    {
        using var factory = new AcademicApiFactory();
        using var client = factory.CreateClient();
        var payload = new
        {
            studentId = "student-1",
            title = string.Empty,
            description = "Review every course",
            targetDate = FutureDate,
            priority = "High"
        };

        var response = await client.PostAsJsonAsync("/api/goals", payload);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        using var document = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
        Assert.Equal("REQUIRED_FIELD", document.RootElement.GetProperty("code").GetString());
        Assert.Equal("Title is required.", document.RootElement.GetProperty("message").GetString());
    }

    [Fact]
    public async Task TC_02_4_01_CompleteGoal_WhenArchived_ReturnsInvalidTransition()
    {
        using var factory = new AcademicApiFactory();
        using var client = factory.CreateClient();
        var payload = new
        {
            studentId = "student-1",
            title = "Prepare for finals",
            description = "Review every course",
            targetDate = FutureDate,
            priority = "High"
        };

        var createResponse = await client.PostAsJsonAsync("/api/goals", payload);
        var created = await createResponse.Content.ReadFromJsonAsync<Goal>(JsonOptions);

        var archiveResponse = await client.PostAsync($"/api/goals/{created!.GoalId}/archive", null);
        Assert.Equal(HttpStatusCode.OK, archiveResponse.StatusCode);

        var completeResponse = await client.PostAsync($"/api/goals/{created.GoalId}/complete", null);

        Assert.Equal(HttpStatusCode.BadRequest, completeResponse.StatusCode);
        using var document = JsonDocument.Parse(await completeResponse.Content.ReadAsStringAsync());
        Assert.Equal("INVALID_TRANSITION", document.RootElement.GetProperty("code").GetString());
    }

    [Fact]
    public async Task TC_02_4_02_CompleteGoal_WhenMissing_ReturnsNotFound()
    {
        using var factory = new AcademicApiFactory();
        using var client = factory.CreateClient();

        var response = await client.PostAsync("/api/goals/missing-goal/complete", null);

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        using var document = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
        Assert.Equal("NOT_FOUND", document.RootElement.GetProperty("code").GetString());
    }
}
