using System.Net.Http.Json;
using System.Text.Json;
using Aspirra.CourseService.Interfaces;
using Aspirra.CourseService.Models;

namespace Aspirra.CourseService.Services;

public class HttpAssessmentClient : IAssessmentClient
{
    private static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web);
    private readonly HttpClient _httpClient;

    public HttpAssessmentClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IReadOnlyList<AssessmentItem>> GetAssignmentsAsync(string courseId)
    {
        try
        {
            var path = $"api/courses/{Uri.EscapeDataString(courseId)}/assignments";
            using var response = await _httpClient.GetAsync(path);

            if (!response.IsSuccessStatusCode)
            {
                return Array.Empty<AssessmentItem>();
            }

            var assignments = await response.Content.ReadFromJsonAsync<List<AssessmentItem>>(JsonOptions);
            return assignments ?? new List<AssessmentItem>();
        }
        catch (Exception ex) when (ex is HttpRequestException or TaskCanceledException or JsonException or NotSupportedException)
        {
            return Array.Empty<AssessmentItem>();
        }
    }
}
