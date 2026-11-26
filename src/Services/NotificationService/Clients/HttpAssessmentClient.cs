using System.Net.Http.Json;
using System.Text.Json;
using Aspirra.NotificationService.Interfaces;
using Aspirra.NotificationService.Models;

namespace Aspirra.NotificationService.Clients;

// Reads the student's assignments and exams from the assessment service (GET api/assessments).
public class HttpAssessmentClient : IAssessment
{
    private static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web);
    private readonly HttpClient _httpClient;

    public HttpAssessmentClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<Deadline>> GetDeadlinesAsync(string studentId, int days, CancellationToken cancellationToken = default)
    {
        try
        {
            var path = $"api/assessments?studentId={Uri.EscapeDataString(studentId)}";
            using var response = await _httpClient.GetAsync(path, cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                return new List<Deadline>();
            }

            var summary = await response.Content.ReadFromJsonAsync<AssessmentSummaryDto>(JsonOptions, cancellationToken);
            return ToDeadlines(summary, studentId, DateOnly.FromDateTime(DateTime.UtcNow), days);
        }
        catch (Exception ex) when (ex is HttpRequestException or TaskCanceledException or JsonException or NotSupportedException)
        {
            return new List<Deadline>();
        }
    }

    internal static List<Deadline> ToDeadlines(AssessmentSummaryDto? summary, string studentId, DateOnly today, int days)
    {
        if (summary is null)
        {
            return new List<Deadline>();
        }

        var lastDay = today.AddDays(days);
        var assignments = summary.Assignments.Select(assignment => new Deadline
        {
            AssessmentId = assignment.AssignmentId,
            StudentId = studentId,
            Title = assignment.Title,
            Kind = "Assignment",
            DueDate = assignment.DueDate,
            IsCompleted = string.Equals(assignment.Status, "Completed", StringComparison.OrdinalIgnoreCase)
        });
        var exams = summary.Exams.Select(exam => new Deadline
        {
            AssessmentId = exam.ExamId,
            StudentId = studentId,
            Title = exam.Title,
            Kind = "Exam",
            DueDate = exam.ExamDate,
            IsCompleted = false
        });

        return assignments
            .Concat(exams)
            .Where(deadline => deadline.DueDate >= today && deadline.DueDate <= lastDay)
            .OrderBy(deadline => deadline.DueDate)
            .ToList();
    }

    internal sealed class AssessmentSummaryDto
    {
        public List<AssignmentDto> Assignments { get; set; } = new();
        public List<ExamDto> Exams { get; set; } = new();
    }

    internal sealed class AssignmentDto
    {
        public string AssignmentId { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public DateOnly DueDate { get; set; }
        public string Status { get; set; } = string.Empty;
    }

    internal sealed class ExamDto
    {
        public string ExamId { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public DateOnly ExamDate { get; set; }
    }
}
