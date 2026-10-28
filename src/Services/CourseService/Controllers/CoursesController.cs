using Aspirra.CourseService.Interfaces;
using Aspirra.CourseService.Models;
using Microsoft.AspNetCore.Mvc;

namespace Aspirra.CourseService.Controllers;

[ApiController]
[Route("api/courses")]
public class CoursesController : ControllerBase
{
    private readonly ICourse _courseService;

    public CoursesController(ICourse courseService)
    {
        _courseService = courseService;
    }

    [HttpGet]
    public async Task<IActionResult> GetCourses([FromQuery] string? studentId)
    {
        var courses = await _courseService.GetCoursesAsync(studentId);
        return Ok(courses);
    }

    [HttpGet("{courseId}")]
    public async Task<IActionResult> GetCourse(string courseId)
    {
        var result = await _courseService.GetCourseAsync(courseId);
        return result.IsSuccess ? Ok(result.Value) : ToErrorResult(result);
    }

    [HttpGet("{courseId}/progress")]
    public async Task<IActionResult> GetCourseProgress(string courseId)
    {
        var result = await _courseService.GetCourseProgressAsync(courseId);
        return result.IsSuccess ? Ok(result.Value) : ToErrorResult(result);
    }

    [HttpPost]
    public async Task<IActionResult> AddCourse([FromBody] Course course)
    {
        var result = await _courseService.AddCourseAsync(course);

        return result.IsSuccess
            ? CreatedAtAction(nameof(GetCourse), new { courseId = result.Value!.CourseId }, result.Value)
            : ToErrorResult(result);
    }

    [HttpPut("{courseId}")]
    public async Task<IActionResult> UpdateCourse(string courseId, [FromBody] Course course)
    {
        var result = await _courseService.UpdateCourseAsync(courseId, course);
        return result.IsSuccess ? Ok(result.Value) : ToErrorResult(result);
    }

    [HttpDelete("{courseId}")]
    public async Task<IActionResult> RemoveCourse(string courseId)
    {
        var result = await _courseService.RemoveCourseAsync(courseId);
        return result.IsSuccess ? NoContent() : ToErrorResult(result);
    }

    private IActionResult ToErrorResult<T>(ServiceResult<T> result) => result.Status switch
    {
        ServiceResultStatus.NotFound => NotFound(new { code = result.Code, message = result.Message }),
        ServiceResultStatus.RequiredField => BadRequest(new { code = result.Code, message = result.Message }),
        ServiceResultStatus.DuplicateCourse => Conflict(new { code = result.Code, message = result.Message }),
        _ => StatusCode(StatusCodes.Status500InternalServerError)
    };
}
