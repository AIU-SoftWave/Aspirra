using Aspirra.AssessmentService.Contracts;
using Aspirra.AssessmentService.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Aspirra.AssessmentService.Controllers
{
    [ApiController]
    [Route("api/exams")]
    public class ExamsController : ControllerBase
    {
        private readonly IAssessment _assessment;

        public ExamsController(IAssessment assessment)
        {
            _assessment = assessment;
        }

        [HttpPost]
        public async Task<IActionResult> CreateExam([FromBody] CreateExamRequest request, CancellationToken cancellationToken)
        {
            var result = await _assessment.AddExamAsync(request, cancellationToken);
            if (!result.IsSuccess)
            {
                return ToErrorResult(result.Error!);
            }

            return StatusCode(StatusCodes.Status201Created, result.Value);
        }

        [HttpGet("upcoming")]
        public async Task<IActionResult> GetUpcomingExams([FromQuery] string? studentId, CancellationToken cancellationToken)
        {
            var exams = await _assessment.GetUpcomingExamsAsync(studentId, cancellationToken);
            return Ok(exams);
        }

        [HttpPost("{examId}/reminder")]
        public async Task<IActionResult> SetExamReminder(string examId, [FromBody] SetExamReminderRequest request, CancellationToken cancellationToken)
        {
            var result = await _assessment.SetExamReminderAsync(examId, request.DaysBefore, cancellationToken);
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
