namespace Aspirra.CourseService.Models;

public class CourseEnrollment
{
    public string EnrollmentId { get; set; } = string.Empty;
    public string CourseId { get; set; } = string.Empty;
    public string StudentId { get; set; } = string.Empty;
    public DateOnly EnrollmentDate { get; set; }
    public string Status { get; set; } = string.Empty;
}
