namespace Aspirra.CourseService.Models;

public class CourseProgress
{
    public string? Message { get; set; }
    public IReadOnlyList<AssessmentItem> Completed { get; set; } = Array.Empty<AssessmentItem>();
    public IReadOnlyList<AssessmentItem> Outstanding { get; set; } = Array.Empty<AssessmentItem>();
}
