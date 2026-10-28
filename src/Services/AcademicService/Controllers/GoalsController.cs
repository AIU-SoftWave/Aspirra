using Aspirra.AcademicService.Exceptions;
using Aspirra.AcademicService.Interfaces;
using Aspirra.AcademicService.Models;
using Microsoft.AspNetCore.Mvc;

namespace Aspirra.AcademicService.Controllers
{
    [ApiController]
    [Route("api/goals")]
    public class GoalsController : ControllerBase
    {
        private readonly IGoal _goalService;

        public GoalsController(IGoal goalService)
        {
            _goalService = goalService;
        }

        [HttpGet]
        public async Task<IActionResult> GetGoals([FromQuery] string? studentId, [FromQuery] string? status, CancellationToken cancellationToken)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(studentId))
                {
                    return BadRequest(new { code = "REQUIRED_FIELD", message = "StudentId is required." });
                }

                var goals = await _goalService.GetGoalsAsync(studentId, status, cancellationToken);
                return Ok(goals);
            }
            catch (GoalValidationException exception)
            {
                return BadRequest(new { code = exception.Code, message = exception.Message });
            }
        }

        [HttpGet("{goalId}")]
        public async Task<IActionResult> GetGoal(string goalId, CancellationToken cancellationToken)
        {
            var goal = await _goalService.GetGoalAsync(goalId, cancellationToken);
            if (goal is null)
            {
                return NotFound(new { code = "NOT_FOUND", message = "Goal not found" });
            }

            return Ok(goal);
        }

        [HttpPost]
        public async Task<IActionResult> CreateGoal([FromBody] Goal data, [FromQuery] string? studentId, CancellationToken cancellationToken)
        {
            try
            {
                var goal = await _goalService.CreateGoalAsync(studentId ?? data.StudentId, data, cancellationToken);
                return CreatedAtAction(nameof(GetGoal), new { goalId = goal.GoalId }, goal);
            }
            catch (GoalValidationException exception)
            {
                return BadRequest(new { code = exception.Code, message = exception.Message });
            }
        }

        [HttpPut("{goalId}")]
        public async Task<IActionResult> UpdateGoal(string goalId, [FromBody] Goal data, CancellationToken cancellationToken)
        {
            try
            {
                var goal = await _goalService.UpdateGoalAsync(goalId, data, cancellationToken);
                return Ok(goal);
            }
            catch (GoalNotFoundException)
            {
                return NotFound(new { code = "NOT_FOUND", message = "Goal not found" });
            }
            catch (GoalValidationException exception)
            {
                return BadRequest(new { code = exception.Code, message = exception.Message });
            }
        }

        [HttpPost("{goalId}/complete")]
        public Task<IActionResult> CompleteGoal(string goalId, CancellationToken cancellationToken)
        {
            return ExecuteStatusChangeAsync(() => _goalService.CompleteGoalAsync(goalId, cancellationToken));
        }

        [HttpPost("{goalId}/archive")]
        public Task<IActionResult> ArchiveGoal(string goalId, CancellationToken cancellationToken)
        {
            return ExecuteStatusChangeAsync(() => _goalService.ArchiveGoalAsync(goalId, cancellationToken));
        }

        [HttpPost("{goalId}/restore")]
        public Task<IActionResult> RestoreGoal(string goalId, CancellationToken cancellationToken)
        {
            return ExecuteStatusChangeAsync(() => _goalService.RestoreGoalAsync(goalId, cancellationToken));
        }

        [HttpDelete("{goalId}")]
        public async Task<IActionResult> DeleteGoal(string goalId, CancellationToken cancellationToken)
        {
            try
            {
                await _goalService.DeleteGoalAsync(goalId, cancellationToken);
                return NoContent();
            }
            catch (GoalNotFoundException)
            {
                return NotFound(new { code = "NOT_FOUND", message = "Goal not found" });
            }
        }

        private async Task<IActionResult> ExecuteStatusChangeAsync(Func<Task<Goal>> action)
        {
            try
            {
                var goal = await action();
                return Ok(goal);
            }
            catch (GoalNotFoundException)
            {
                return NotFound(new { code = "NOT_FOUND", message = "Goal not found" });
            }
        }
    }
}
