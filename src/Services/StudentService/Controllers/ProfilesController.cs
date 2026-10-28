using Aspirra.StudentService.Interfaces;
using Aspirra.StudentService.Models;
using Microsoft.AspNetCore.Mvc;

namespace Aspirra.StudentService.Controllers
{
    [ApiController]
    [Route("api/profiles")]
    public class ProfilesController : ControllerBase
    {
        private readonly IStudentProfile _profileService;

        public ProfilesController(IStudentProfile profileService)
        {
            _profileService = profileService;
        }

        [HttpPost("{studentId}")]
        public async Task<IActionResult> CreateProfile(
            string studentId,
            [FromBody] Profile? profile,
            CancellationToken cancellationToken)
        {
            var result = await _profileService.CreateProfileAsync(
                studentId,
                profile ?? new Profile(),
                cancellationToken);

            if (!result.IsSuccess)
            {
                return ToErrorResult(result);
            }

            return CreatedAtAction(
                nameof(GetProfile),
                new { studentId = result.Value!.StudentId },
                ProfileResponse.From(result.Value));
        }

        [HttpPut("{studentId}")]
        public async Task<IActionResult> UpdateProfile(
            string studentId,
            [FromBody] Profile? profile,
            CancellationToken cancellationToken)
        {
            var result = await _profileService.UpdateProfileAsync(
                studentId,
                profile ?? new Profile(),
                cancellationToken);

            return result.IsSuccess
                ? Ok(ProfileResponse.From(result.Value!))
                : ToErrorResult(result);
        }

        [HttpGet("{studentId}")]
        public async Task<IActionResult> GetProfile(string studentId, CancellationToken cancellationToken)
        {
            var result = await _profileService.GetProfileAsync(studentId, cancellationToken);

            return result.IsSuccess
                ? Ok(ProfileResponse.From(result.Value!))
                : ToErrorResult(result);
        }

        [HttpGet("{studentId}/availability")]
        public async Task<IActionResult> GetAvailability(string studentId, CancellationToken cancellationToken)
        {
            var result = await _profileService.GetAvailabilityAsync(studentId, cancellationToken);

            return result.IsSuccess
                ? Ok(result.Value!.Select(TimeSlotResponse.From))
                : ToErrorResult(result);
        }

        private IActionResult ToErrorResult<T>(ServiceResult<T> result)
        {
            return result.Status switch
            {
                ServiceResultStatus.NotFound => NotFound(new { code = result.Code, message = result.Message }),
                ServiceResultStatus.RequiredField => BadRequest(new { code = result.Code, message = result.Message }),
                ServiceResultStatus.DuplicateProfile => Conflict(new { code = result.Code, message = result.Message }),
                ServiceResultStatus.InvalidTimeSlot => BadRequest(new { code = result.Code, message = result.Message }),
                _ => StatusCode(StatusCodes.Status500InternalServerError)
            };
        }
    }
}
