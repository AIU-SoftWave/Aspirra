using Aspirra.AssessmentService.Contracts;
using Aspirra.AssessmentService.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Aspirra.AssessmentService.Controllers
{
    [ApiController]
    [Route("api/assignments")]
    public class AssignmentsController : ControllerBase
    {
        private readonly IAssessment _assessment;

        public AssignmentsController(IAssessment assessment)
        {
            _assessment = assessment;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAssignment([FromBody] CreateAssignmentRequest request, CancellationToken cancellationToken)
        {
            var result = await _assessment.AddAssignmentAsync(request, cancellationToken);
            if (!result.IsSuccess)
            {
                return ToErrorResult(result.Error!);
            }

            return StatusCode(StatusCodes.Status201Created, result.Value);
        }

        [HttpPatch("{assignmentId}/status")]
        public async Task<IActionResult> SetAssignmentStatus(string assignmentId, [FromBody] UpdateAssignmentStatusRequest request, CancellationToken cancellationToken)
        {
            var result = await _assessment.SetAssignmentStatusAsync(assignmentId, request.Status, cancellationToken);
            if (!result.IsSuccess)
            {
                return ToErrorResult(result.Error!);
            }

            return Ok(result.Value);
        }

        private IActionResult ToErrorResult(AssessmentError error)
        {
            var payload = new { code = error.Code, message = error.Message };

            if (error.Code == AssessmentErrorCodes.NotFound)
            {
                return NotFound(payload);
            }

            return BadRequest(payload);
        }
    }
}
