using Aspirra.HabitService.Common;
using Aspirra.HabitService.Dtos;
using Aspirra.HabitService.Models;

namespace Aspirra.HabitService.Interfaces;

public interface IHabit
{
    Task<ServiceResult<Habit>> CreateHabitAsync(string studentId, CreateHabitRequest request, CancellationToken cancellationToken = default);
    Task<ServiceResult<Habit>> CompleteHabitAsync(string habitId, DateOnly? date = null, CancellationToken cancellationToken = default);
    Task<List<Habit>> GetHabitsAsync(string studentId, CancellationToken cancellationToken = default);
    Task<List<HabitStreakResponse>> GetStreaksAsync(string studentId, DateOnly? today = null, CancellationToken cancellationToken = default);
    Task<ServiceResult<Habit>> LinkToGoalAsync(string habitId, string? goalId, CancellationToken cancellationToken = default);
}
