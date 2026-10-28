using Microsoft.AspNetCore.Mvc;

namespace Aspirra.AssessmentService.Controllers
{
    [ApiController]
    [Route("health")]
    public class HealthController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetHealth()
        {
            return Ok(new { status = "healthy" });
        }
    }
}
