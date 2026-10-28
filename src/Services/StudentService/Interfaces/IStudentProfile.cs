using Aspirra.StudentService.Models;

namespace Aspirra.StudentService.Interfaces
{
    public interface IStudentProfile
    {
        Task<ServiceResult<Profile>> CreateProfileAsync(string studentId, Profile data, CancellationToken cancellationToken = default);
        Task<ServiceResult<Profile>> UpdateProfileAsync(string studentId, Profile data, CancellationToken cancellationToken = default);
        Task<ServiceResult<Profile>> GetProfileAsync(string studentId, CancellationToken cancellationToken = default);
        Task<ServiceResult<List<TimeSlot>>> GetAvailabilityAsync(string studentId, CancellationToken cancellationToken = default);
    }
}
