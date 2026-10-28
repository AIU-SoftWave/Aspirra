using Aspirra.AssessmentService.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Aspirra.AssessmentService.Controllers
{
    [ApiController]
    [Route("api/assessments")]
    public class AssessmentsController : ControllerBase
    {
        private readonly IAssessment _assessment;

        public AssessmentsController(IAssessment assessment)
        {
            _assessment = assessment;
        }

        [HttpGet]
        public async Task<IActionResult> GetAssessments([FromQuery] string? studentId, [FromQuery] string? courseId, CancellationToken cancellationToken)
        {
            var assessments = await _assessment.GetAssessmentsAsync(studentId, courseId, cancellationToken);
            return Ok(assessments);
        }
    }
}
