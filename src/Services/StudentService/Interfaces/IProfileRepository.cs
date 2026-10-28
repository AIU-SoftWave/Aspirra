using Aspirra.StudentService.Models;

namespace Aspirra.StudentService.Interfaces
{
    public interface IProfileRepository
    {
        Task AddAsync(Profile profile, CancellationToken cancellationToken = default);
        Task<Profile?> GetByIdAsync(string studentId, CancellationToken cancellationToken = default);
        void Update(Profile profile);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
