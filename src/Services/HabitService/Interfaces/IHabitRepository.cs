using Aspirra.HabitService.Models;

namespace Aspirra.HabitService.Interfaces;

public interface IHabitRepository
{
    Task<Habit?> GetHabitAsync(string habitId, CancellationToken cancellationToken = default);
    Task<List<Habit>> GetHabitsByStudentAsync(string studentId, CancellationToken cancellationToken = default);
    Task<HabitLog?> GetLogAsync(string habitId, DateOnly date, CancellationToken cancellationToken = default);
    Task<List<HabitLog>> GetLogsAsync(string habitId, CancellationToken cancellationToken = default);
    Task AddHabitAsync(Habit habit, CancellationToken cancellationToken = default);
    Task AddLogAsync(HabitLog log, CancellationToken cancellationToken = default);
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}
