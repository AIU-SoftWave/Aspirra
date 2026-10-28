using Aspirra.AssessmentService.Contracts;
using Aspirra.AssessmentService.Models;

namespace Aspirra.AssessmentService.Interfaces
{
    public interface IAssessment
    {
        Task<AssessmentResult<Assignment>> AddAssignmentAsync(CreateAssignmentRequest request, CancellationToken cancellationToken = default);
        Task<AssessmentResult<Exam>> AddExamAsync(CreateExamRequest request, CancellationToken cancellationToken = default);
        Task<AssessmentResult<Assignment>> SetAssignmentStatusAsync(string assignmentId, string? status, CancellationToken cancellationToken = default);
        Task<AssessmentResult<Exam>> SetExamReminderAsync(string examId, int? daysBefore, CancellationToken cancellationToken = default);
        Task<AssessmentSummary> GetAssessmentsAsync(string? studentId, string? courseId, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<CourseAssignmentItem>> GetAssignmentsByCourseAsync(string courseId, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Exam>> GetUpcomingExamsAsync(string? studentId, CancellationToken cancellationToken = default);
    }
}
