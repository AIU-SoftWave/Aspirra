using System.Net;
using System.Text;
using Aspirra.NotificationService.Clients;

namespace Aspirra.NotificationService.Tests;

public class HttpAssessmentClientTests
{
    private sealed class StubHandler : HttpMessageHandler
    {
        private readonly Func<HttpRequestMessage, HttpResponseMessage> _respond;

        public StubHandler(Func<HttpRequestMessage, HttpResponseMessage> respond)
        {
            _respond = respond;
        }

        public Uri? LastRequestUri { get; private set; }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            LastRequestUri = request.RequestUri;
            return Task.FromResult(_respond(request));
        }
    }

    private static HttpAssessmentClient CreateClient(StubHandler handler) =>
        new(new HttpClient(handler) { BaseAddress = new Uri("http://assessment-service:8080") });

    private static string SummaryJson(DateOnly today) =>
        $$"""
        {
          "assignments": [
            { "assignmentId": "asg-3001", "title": "Database Project", "dueDate": "{{today.AddDays(1):yyyy-MM-dd}}", "status": "InProgress" },
            { "assignmentId": "asg-3002", "title": "ERD Sheet", "dueDate": "{{today.AddDays(1):yyyy-MM-dd}}", "status": "Completed" },
            { "assignmentId": "asg-3003", "title": "Final Report", "dueDate": "{{today.AddDays(10):yyyy-MM-dd}}", "status": "NotStarted" }
          ],
          "exams": [
            { "examId": "exam-4001", "title": "Data Mining Midterm", "examDate": "{{today:yyyy-MM-dd}}" }
          ]
        }
        """;

    [Fact]
    public async Task GetDeadlines_ReturnsAssignmentsAndExamsInsideTheWindow()
    {
        var today = DateOnly.FromDateTime(DateTime.UtcNow);
        var handler = new StubHandler(_ => new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent(SummaryJson(today), Encoding.UTF8, "application/json")
        });

        var deadlines = await CreateClient(handler).GetDeadlinesAsync("student-1001", 1);

        Assert.Equal(3, deadlines.Count);
        Assert.Equal("exam-4001", deadlines[0].AssessmentId);
        Assert.Contains(deadlines, d => d.AssessmentId == "asg-3002" && d.IsCompleted);
        Assert.DoesNotContain(deadlines, d => d.AssessmentId == "asg-3003");
        Assert.Equal("/api/assessments?studentId=student-1001", handler.LastRequestUri!.PathAndQuery);
    }

    [Fact]
    public async Task GetDeadlines_WhenServiceReturnsError_ReturnsEmpty()
    {
        var handler = new StubHandler(_ => new HttpResponseMessage(HttpStatusCode.InternalServerError));

        var deadlines = await CreateClient(handler).GetDeadlinesAsync("student-1001", 1);

        Assert.Empty(deadlines);
    }

    [Fact]
    public async Task GetDeadlines_WhenServiceIsDown_ReturnsEmpty()
    {
        var handler = new StubHandler(_ => throw new HttpRequestException("connection refused"));

        var deadlines = await CreateClient(handler).GetDeadlinesAsync("student-1001", 1);

        Assert.Empty(deadlines);
    }

    [Fact]
    public void ToDeadlines_WithNullSummary_ReturnsEmpty()
    {
        Assert.Empty(HttpAssessmentClient.ToDeadlines(null, "student-1001", new DateOnly(2026, 11, 26), 1));
    }
}
