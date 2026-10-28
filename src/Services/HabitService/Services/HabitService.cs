using Aspirra.HabitService.Common;
using Aspirra.HabitService.Dtos;
using Aspirra.HabitService.Interfaces;
using Aspirra.HabitService.Models;

namespace Aspirra.HabitService.Services;

public class HabitService : IHabit
{
    private readonly IHabitRepository _repository;

    public HabitService(IHabitRepository repository)
    {
        _repository = repository;
    }

    public async Task<ServiceResult<Habit>> CreateHabitAsync(
        string studentId,
        CreateHabitRequest request,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
        {
            return ServiceResult<Habit>.Fail(HabitErrorCodes.RequiredField, "Name is required.");
        }

        if (string.IsNullOrWhiteSpace(request.Frequency))
        {
            return ServiceResult<Habit>.Fail(HabitErrorCodes.RequiredField, "Frequency is required.");
        }

        if (!TryParseFrequency(request.Frequency, out var frequency))
        {
            return ServiceResult<Habit>.Fail(HabitErrorCodes.InvalidFrequency, "Invalid habit frequency");
        }

        var now = DateTime.UtcNow;
        var habit = new Habit
        {
            HabitId = Guid.NewGuid().ToString(),
            StudentId = studentId,
            GoalId = NormalizeOptional(request.GoalId),
            Name = request.Name.Trim(),
            Frequency = frequency,
            GoalTarget = NormalizeOptional(request.GoalTarget),
            ReminderTime = request.ReminderTime,
            CurrentStreak = 0,
            IsActive = true,
            CreatedAt = now,
            UpdatedAt = now
        };

        await _repository.AddHabitAsync(habit, cancellationToken);
        await _repository.SaveChangesAsync(cancellationToken);

        return ServiceResult<Habit>.Ok(habit);
    }

    public async Task<ServiceResult<Habit>> CompleteHabitAsync(
        string habitId,
        DateOnly? date = null,
        CancellationToken cancellationToken = default)
    {
        var habit = await _repository.GetHabitAsync(habitId, cancellationToken);
        if (habit is null)
        {
            return ServiceResult<Habit>.Fail(HabitErrorCodes.NotFound, "Habit not found");
        }

        var completionDate = date ?? DateOnly.FromDateTime(DateTime.UtcNow);
        var log = await _repository.GetLogAsync(habitId, completionDate, cancellationToken);

        if (log is null)
        {
            log = new HabitLog
            {
                LogId = Guid.NewGuid().ToString(),
                HabitId = habitId,
                LogDate = completionDate,
                Completed = true,
                CompletedAt = DateTime.UtcNow
            };
            await _repository.AddLogAsync(log, cancellationToken);
        }
        else if (!log.Completed)
        {
            log.Completed = true;
            log.CompletedAt = DateTime.UtcNow;
        }

        await _repository.SaveChangesAsync(cancellationToken);

        var logs = await _repository.GetLogsAsync(habitId, cancellationToken);
        habit.CurrentStreak = CalculateStreak(habit, completionDate, logs);
        habit.UpdatedAt = DateTime.UtcNow;
        await _repository.SaveChangesAsync(cancellationToken);

        return ServiceResult<Habit>.Ok(habit);
    }

    public Task<List<Habit>> GetHabitsAsync(string studentId, CancellationToken cancellationToken = default) =>
        _repository.GetHabitsByStudentAsync(studentId, cancellationToken);

    public async Task<List<HabitStreakResponse>> GetStreaksAsync(
        string studentId,
        DateOnly? today = null,
        CancellationToken cancellationToken = default)
    {
        var currentDay = today ?? DateOnly.FromDateTime(DateTime.UtcNow);
        var habits = await _repository.GetHabitsByStudentAsync(studentId, cancellationToken);
        var streaks = new List<HabitStreakResponse>(habits.Count);

        foreach (var habit in habits)
        {
            var logs = await _repository.GetLogsAsync(habit.HabitId, cancellationToken);
            streaks.Add(new HabitStreakResponse
            {
                HabitId = habit.HabitId,
                Name = habit.Name,
                CurrentStreak = habit.CurrentStreak,
                DoneToday = logs.Any(log => log.Completed && log.LogDate == currentDay)
            });
        }

        return streaks;
    }

    public async Task<ServiceResult<Habit>> LinkToGoalAsync(
        string habitId,
        string? goalId,
        CancellationToken cancellationToken = default)
    {
        var habit = await _repository.GetHabitAsync(habitId, cancellationToken);
        if (habit is null)
        {
            return ServiceResult<Habit>.Fail(HabitErrorCodes.NotFound, "Habit not found");
        }

        if (string.IsNullOrWhiteSpace(goalId))
        {
            return ServiceResult<Habit>.Fail(HabitErrorCodes.RequiredField, "GoalId is required.");
        }

        habit.GoalId = goalId.Trim();
        habit.UpdatedAt = DateTime.UtcNow;
        await _repository.SaveChangesAsync(cancellationToken);

        return ServiceResult<Habit>.Ok(habit);
    }

    private static bool TryParseFrequency(string value, out HabitFrequency frequency)
    {
        var trimmed = value.Trim();
        if (string.Equals(trimmed, nameof(HabitFrequency.Daily), StringComparison.OrdinalIgnoreCase))
        {
            frequency = HabitFrequency.Daily;
            return true;
        }

        if (string.Equals(trimmed, nameof(HabitFrequency.Weekly), StringComparison.OrdinalIgnoreCase))
        {
            frequency = HabitFrequency.Weekly;
            return true;
        }

        frequency = default;
        return false;
    }

    private static int CalculateStreak(Habit habit, DateOnly completionDate, IEnumerable<HabitLog> logs)
    {
        var completedDates = logs
            .Where(log => log.Completed)
            .Select(log => log.LogDate)
            .ToHashSet();

        var createdDate = DateOnly.FromDateTime(habit.CreatedAt.ToUniversalTime());
        var interval = habit.Frequency == HabitFrequency.Weekly ? 7 : 1;
        var anchor = completionDate;

        if (habit.Frequency == HabitFrequency.Weekly)
        {
            var scheduledDay = habit.CreatedAt.ToUniversalTime().DayOfWeek;
            var offset = ((int)anchor.DayOfWeek - (int)scheduledDay + 7) % 7;
            anchor = anchor.AddDays(-offset);
        }

        var streak = 0;
        for (var current = anchor; current >= createdDate; current = current.AddDays(-interval))
        {
            if (!completedDates.Contains(current))
            {
                break;
            }

            streak++;
        }

        return streak;
    }

    private static string? NormalizeOptional(string? value) =>
        string.IsNullOrWhiteSpace(value) ? null : value.Trim();
}
