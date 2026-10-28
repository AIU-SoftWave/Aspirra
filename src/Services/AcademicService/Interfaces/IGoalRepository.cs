using Aspirra.AcademicService.Models;

namespace Aspirra.AcademicService.Interfaces
{
    public interface IGoalRepository
    {
        Task<Goal> AddAsync(Goal goal, CancellationToken cancellationToken = default);
        Task<Goal?> GetByIdAsync(string goalId, CancellationToken cancellationToken = default);
        Task Update(Goal goal, CancellationToken cancellationToken = default);
        Task Delete(Goal goal, CancellationToken cancellationToken = default);
        Task UpdateAsync(Goal goal, CancellationToken cancellationToken = default);
        Task DeleteAsync(Goal goal, CancellationToken cancellationToken = default);
        IQueryable<Goal> Query();
    }
}
