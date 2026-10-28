using Aspirra.HabitService.Data;
using Aspirra.HabitService.Interfaces;
using Aspirra.HabitService.Models;
using Microsoft.EntityFrameworkCore;

namespace Aspirra.HabitService.Repositories;

public class HabitRepository : IHabitRepository
{
    private readonly HabitDbContext _context;

    public HabitRepository(HabitDbContext context)
    {
        _context = context;
    }

    public Task<Habit?> GetHabitAsync(string habitId, CancellationToken cancellationToken = default) =>
        _context.Habits.FirstOrDefaultAsync(habit => habit.HabitId == habitId, cancellationToken);

    public Task<List<Habit>> GetHabitsByStudentAsync(string studentId, CancellationToken cancellationToken = default) =>
        _context.Habits
            .AsNoTracking()
            .Where(habit => habit.StudentId == studentId)
            .OrderByDescending(habit => habit.CreatedAt)
            .ToListAsync(cancellationToken);

    public Task<HabitLog?> GetLogAsync(string habitId, DateOnly date, CancellationToken cancellationToken = default) =>
        _context.HabitLogs.FirstOrDefaultAsync(
            log => log.HabitId == habitId && log.LogDate == date,
            cancellationToken);

    public Task<List<HabitLog>> GetLogsAsync(string habitId, CancellationToken cancellationToken = default) =>
        _context.HabitLogs
            .AsNoTracking()
            .Where(log => log.HabitId == habitId)
            .ToListAsync(cancellationToken);

    public Task AddHabitAsync(Habit habit, CancellationToken cancellationToken = default) =>
        _context.Habits.AddAsync(habit, cancellationToken).AsTask();

    public Task AddLogAsync(HabitLog log, CancellationToken cancellationToken = default) =>
        _context.HabitLogs.AddAsync(log, cancellationToken).AsTask();

    public Task SaveChangesAsync(CancellationToken cancellationToken = default) =>
        _context.SaveChangesAsync(cancellationToken);
}
