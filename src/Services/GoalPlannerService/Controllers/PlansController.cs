using Aspirra.GoalPlannerService.Contracts;
using Aspirra.GoalPlannerService.Interfaces;
using Aspirra.GoalPlannerService.Models;
using Microsoft.AspNetCore.Mvc;

namespace Aspirra.GoalPlannerService.Controllers
{
    [ApiController]
    [Route("api/plans")]
    public class PlansController : ControllerBase
    {
        private readonly IGoalPlanner _goalPlanner;

        public PlansController(IGoalPlanner goalPlanner)
        {
            _goalPlanner = goalPlanner;
        }

        [HttpPost("generate")]
        public async Task<ActionResult<PlanDraft>> Generate([FromBody] GeneratePlanRequest request)
        {
            var result = await _goalPlanner.GeneratePlanAsync(request);
            if (!result.IsSuccess)
            {
                return Failure(result.Error!);
            }

            var draft = result.Value!;
            return Created($"/api/plans/{draft.DraftId}", draft);
        }

        [HttpPut("{draftId}")]
        public async Task<ActionResult<PlanDraft>> Update(
            string draftId,
            [FromBody] PlanDraftInput input)
        {
            var result = await _goalPlanner.UpdateDraftAsync(draftId, input);
            if (!result.IsSuccess)
            {
                return Failure(result.Error!);
            }

            return Ok(result.Value);
        }

        [HttpPost("{draftId}/approve")]
        public async Task<ActionResult<PlanDraft>> Approve(string draftId)
        {
            var result = await _goalPlanner.ApproveAsync(draftId);
            if (!result.IsSuccess)
            {
                return Failure(result.Error!);
            }

            return Ok(result.Value);
        }

        [HttpPost("{draftId}/discard")]
        public async Task<IActionResult> Discard(string draftId)
        {
            var result = await _goalPlanner.DiscardAsync(draftId);
            if (!result.IsSuccess)
            {
                return Failure(result.Error!);
            }

            return NoContent();
        }

        [HttpGet("{draftId}")]
        public async Task<ActionResult<PlanDraft>> Get(string draftId)
        {
            var draft = await _goalPlanner.GetDraftAsync(draftId);
            if (draft is null)
            {
                return NotFound(new { code = "NOT_FOUND", message = "Plan draft not found" });
            }

            return Ok(draft);
        }

        [HttpGet("tasks")]
        public async Task<ActionResult<List<PlanTask>>> GetTasks([FromQuery] string? goalId)
        {
            var tasks = await _goalPlanner.GetTasksAsync(goalId ?? string.Empty);
            return Ok(tasks);
        }

        private ObjectResult Failure(ServiceError error) =>
            StatusCode(error.StatusCode, new { code = error.Code, message = error.Message });
    }
}
