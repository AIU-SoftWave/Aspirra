using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Aspirra.SkillService.Data;
using Aspirra.SkillService.Dtos;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Aspirra.SkillService.Tests;

public class SkillApiTests
{
    private sealed class SkillApiFactory : WebApplicationFactory<Program>
    {
        private readonly string _databaseName = $"aspirra-skill-api-{Guid.NewGuid():N}";

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("Testing");
            builder.ConfigureServices(services =>
            {
                var registrations = services
                    .Where(descriptor =>
                        descriptor.ServiceType == typeof(DbContextOptions) ||
                        descriptor.ServiceType == typeof(DbContextOptions<SkillDbContext>) ||
                        (descriptor.ServiceType.IsGenericType &&
                         descriptor.ServiceType.GetGenericArguments().Contains(typeof(SkillDbContext))))
                    .ToList();

                foreach (var registration in registrations)
                {
                    services.Remove(registration);
                }

                services.AddDbContext<SkillDbContext>(options =>
                    options.UseInMemoryDatabase(_databaseName));
            });
        }
    }

    private static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web);

    private static object SqlPayload => new
    {
        studentId = SkillFixtures.StudentId,
        name = SkillFixtures.SkillName,
        type = "Technical",
        level = 3
    };

    private static async Task<string?> ReadCodeAsync(HttpResponseMessage response)
    {
        using var document = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
        return document.RootElement.GetProperty("code").GetString();
    }

    [Fact]
    public async Task Health_ReturnsHealthy()
    {
        using var factory = new SkillApiFactory();
        using var client = factory.CreateClient();

        var response = await client.GetAsync("/health");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task AddSkill_ReturnsCreatedActiveSkillThatCanBeRetrieved()
    {
        using var factory = new SkillApiFactory();
        using var client = factory.CreateClient();

        var createResponse = await client.PostAsJsonAsync("/api/skills", SqlPayload);

        Assert.Equal(HttpStatusCode.Created, createResponse.StatusCode);
        Assert.NotNull(createResponse.Headers.Location);
        var created = await createResponse.Content.ReadFromJsonAsync<SkillResponse>(JsonOptions);
        Assert.Equal("SQL", created!.Name);
        Assert.Equal("Technical", created.Type);
        Assert.Equal(3, created.Level);
        Assert.Equal("Active", created.Status);

        var getResponse = await client.GetAsync($"/api/skills/{created.SkillId}");
        Assert.Equal(HttpStatusCode.OK, getResponse.StatusCode);

        var listResponse = await client.GetAsync($"/api/skills?studentId={SkillFixtures.StudentId}");
        Assert.Equal(HttpStatusCode.OK, listResponse.StatusCode);
        var skills = await listResponse.Content.ReadFromJsonAsync<List<SkillResponse>>(JsonOptions);
        Assert.Single(skills!);
    }

    [Fact]
    public async Task AddSkill_SameNameTwice_ReturnsConflict()
    {
        using var factory = new SkillApiFactory();
        using var client = factory.CreateClient();
        await client.PostAsJsonAsync("/api/skills", SqlPayload);

        var duplicate = new { studentId = SkillFixtures.StudentId, name = "sql", type = "Soft", level = 2 };
        var response = await client.PostAsJsonAsync("/api/skills", duplicate);

        Assert.Equal(HttpStatusCode.Conflict, response.StatusCode);
        Assert.Equal("DUPLICATE_SKILL", await ReadCodeAsync(response));
    }

    [Fact]
    public async Task AddSkill_MissingName_ReturnsBadRequest()
    {
        using var factory = new SkillApiFactory();
        using var client = factory.CreateClient();

        var response = await client.PostAsJsonAsync(
            "/api/skills",
            new { studentId = SkillFixtures.StudentId, type = "Technical", level = 3 });

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.Equal("REQUIRED_FIELD", await ReadCodeAsync(response));
    }

    [Fact]
    public async Task AddSkill_InvalidLevel_ReturnsBadRequest()
    {
        using var factory = new SkillApiFactory();
        using var client = factory.CreateClient();

        var response = await client.PostAsJsonAsync(
            "/api/skills",
            new { studentId = SkillFixtures.StudentId, name = "SQL", type = "Technical", level = 9 });

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.Equal("INVALID_LEVEL", await ReadCodeAsync(response));
    }

    [Fact]
    public async Task GetSkill_WhenMissing_ReturnsNotFound()
    {
        using var factory = new SkillApiFactory();
        using var client = factory.CreateClient();

        var response = await client.GetAsync("/api/skills/missing-skill");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        Assert.Equal("NOT_FOUND", await ReadCodeAsync(response));
    }

    [Fact]
    public async Task GetSkills_WithoutStudentId_ReturnsBadRequest()
    {
        using var factory = new SkillApiFactory();
        using var client = factory.CreateClient();

        var response = await client.GetAsync("/api/skills");

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.Equal("REQUIRED_FIELD", await ReadCodeAsync(response));
    }
}
