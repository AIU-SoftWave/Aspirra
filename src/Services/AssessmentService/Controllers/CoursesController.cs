using Aspirra.AssessmentService.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Aspirra.AssessmentService.Controllers
{
    [ApiController]
    [Route("api/courses")]
    public class CoursesController : ControllerBase
    {
        private readonly IAssessment _assessment;

        public CoursesController(IAssessment assessment)
        {
            _assessment = assessment;
        }

        [HttpGet("{courseId}/assignments")]
        public async Task<IActionResult> GetAssignmentsByCourse(string courseId, CancellationToken cancellationToken)
        {
            var assignments = await _assessment.GetAssignmentsByCourseAsync(courseId, cancellationToken);
            return Ok(assignments);
        }
    }
}
