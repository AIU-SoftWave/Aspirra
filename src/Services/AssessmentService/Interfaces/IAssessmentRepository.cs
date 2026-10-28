using Aspirra.AssessmentService.Models;

namespace Aspirra.AssessmentService.Interfaces
{
    public interface IAssessmentRepository
    {
        Task AddAssignmentAsync(Assignment assignment, CancellationToken cancellationToken = default);
        Task AddExamAsync(Exam exam, CancellationToken cancellationToken = default);
        Task<Assignment?> GetAssignmentAsync(string assignmentId, CancellationToken cancellationToken = default);
        Task<Exam?> GetExamAsync(string examId, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Assignment>> GetAssignmentsAsync(string? studentId, string? courseId, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Assignment>> GetAssignmentsByCourseAsync(string courseId, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Exam>> GetExamsAsync(string? studentId, string? courseId, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Exam>> GetUpcomingExamsAsync(string? studentId, DateOnly fromDate, CancellationToken cancellationToken = default);
        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
