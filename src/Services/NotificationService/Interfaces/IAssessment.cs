using Aspirra.NotificationService.Models;

namespace Aspirra.NotificationService.Interfaces;

// Required interface: deadlines provided by the assessment service.
public interface IAssessment
{
    Task<List<Deadline>> GetDeadlinesAsync(string studentId, int days, CancellationToken cancellationToken = default);
}
