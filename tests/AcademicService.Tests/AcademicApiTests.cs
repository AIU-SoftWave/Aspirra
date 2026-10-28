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
    public async Task Health_ReturnsHealthy()
    {
        using var factory = new AcademicApiFactory();
        using var client = factory.CreateClient();

        var response = await client.GetAsync("/health");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        using var document = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
        Assert.Equal("healthy", document.RootElement.GetProperty("status").GetString());
    }

    [Fact]
    public async Task CreateGoal_ReturnsCreatedGoalAndCanBeRetrieved()
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
    public async Task CreateGoal_MissingTitle_ReturnsRequiredFieldPayload()
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
}
