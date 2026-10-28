using Aspirra.CourseService.Models;

namespace Aspirra.CourseService.Interfaces;

public interface IAssessmentClient
{
    Task<IReadOnlyList<AssessmentItem>> GetAssignmentsAsync(string courseId);
}
