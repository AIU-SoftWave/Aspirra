using Aspirra.HabitService.Common;
using Aspirra.HabitService.Dtos;
using Aspirra.HabitService.Interfaces;
using Aspirra.HabitService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Aspirra.HabitService.Controllers;

[ApiController]
[Route("api/habits")]
public class HabitsController : ControllerBase
{
    private readonly IHabit _habitService;

    public HabitsController(IHabit habitService)
    {
        _habitService = habitService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateHabit(
        [FromBody] CreateHabitRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _habitService.CreateHabitAsync(
            request.StudentId ?? string.Empty,
            request,
            cancellationToken);

        if (!result.Success)
        {
            return ToErrorResult(result);
        }

        return StatusCode(StatusCodes.Status201Created, ToResponse(result.Value!));
    }

    [HttpPost("{habitId}/complete")]
    public async Task<IActionResult> CompleteHabit(
        string habitId,
        [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] CompleteHabitRequest? request,
        CancellationToken cancellationToken)
    {
        var result = await _habitService.CompleteHabitAsync(habitId, request?.Date, cancellationToken);

        if (!result.Success)
        {
            return ToErrorResult(result);
        }

        return Ok(ToResponse(result.Value!));
    }

    [HttpGet]
    public async Task<IActionResult> GetHabits(
        [FromQuery] string? studentId,
        CancellationToken cancellationToken)
    {
        var habits = await _habitService.GetHabitsAsync(studentId ?? string.Empty, cancellationToken);
        return Ok(habits.Select(ToResponse));
    }

    [HttpGet("streaks")]
    public async Task<IActionResult> GetStreaks(
        [FromQuery] string? studentId,
        CancellationToken cancellationToken)
    {
        var streaks = await _habitService.GetStreaksAsync(studentId ?? string.Empty, null, cancellationToken);
        return Ok(streaks);
    }

    [HttpPost("{habitId}/link-goal")]
    public async Task<IActionResult> LinkToGoal(
        string habitId,
        [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] LinkGoalRequest? request,
        CancellationToken cancellationToken)
    {
        var result = await _habitService.LinkToGoalAsync(habitId, request?.GoalId, cancellationToken);

        if (!result.Success)
        {
            return ToErrorResult(result);
        }

        return Ok(ToResponse(result.Value!));
    }

    private IActionResult ToErrorResult<T>(ServiceResult<T> result)
    {
        var body = new { code = result.Code, message = result.Message };

        return result.Code == HabitErrorCodes.NotFound
            ? NotFound(body)
            : BadRequest(body);
    }

    private static HabitResponse ToResponse(Habit habit) => new()
    {
        HabitId = habit.HabitId,
        StudentId = habit.StudentId,
        GoalId = habit.GoalId,
        Name = habit.Name,
        Frequency = habit.Frequency.ToString(),
        GoalTarget = habit.GoalTarget,
        ReminderTime = habit.ReminderTime,
        CurrentStreak = habit.CurrentStreak,
        IsActive = habit.IsActive,
        CreatedAt = habit.CreatedAt,
        UpdatedAt = habit.UpdatedAt
    };
}
