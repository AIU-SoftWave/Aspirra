using Aspirra.SkillService.Common;
using Aspirra.SkillService.Dtos;
using Aspirra.SkillService.Interfaces;
using Aspirra.SkillService.Models;
using Microsoft.AspNetCore.Mvc;

namespace Aspirra.SkillService.Controllers;

[ApiController]
[Route("api/skills")]
public class SkillsController : ControllerBase
{
    private readonly ISkill _skillService;

    public SkillsController(ISkill skillService)
    {
        _skillService = skillService;
    }

    [HttpPost]
    public async Task<IActionResult> AddSkill(
        [FromBody] AddSkillRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _skillService.AddSkillAsync(
            request.StudentId ?? string.Empty,
            request,
            cancellationToken);

        if (!result.Success)
        {
            return ToErrorResult(result);
        }

        var skill = result.Value!;
        return CreatedAtAction(nameof(GetSkill), new { skillId = skill.SkillId }, ToResponse(skill));
    }

    [HttpGet]
    public async Task<IActionResult> GetSkills(
        [FromQuery] string? studentId,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(studentId))
        {
            return BadRequest(new { code = SkillErrorCodes.RequiredField, message = "StudentId is required." });
        }

        var skills = await _skillService.GetSkillsAsync(studentId, cancellationToken);
        return Ok(skills.Select(ToResponse));
    }

    [HttpGet("{skillId}")]
    public async Task<IActionResult> GetSkill(string skillId, CancellationToken cancellationToken)
    {
        var result = await _skillService.GetSkillAsync(skillId, cancellationToken);

        if (!result.Success)
        {
            return ToErrorResult(result);
        }

        return Ok(ToResponse(result.Value!));
    }

    private IActionResult ToErrorResult<T>(ServiceResult<T> result)
    {
        var body = new { code = result.Code, message = result.Message };

        return result.Code switch
        {
            SkillErrorCodes.NotFound => NotFound(body),
            SkillErrorCodes.DuplicateSkill => Conflict(body),
            _ => BadRequest(body)
        };
    }

    private static SkillResponse ToResponse(Skill skill) => new()
    {
        SkillId = skill.SkillId,
        StudentId = skill.StudentId,
        Name = skill.Name,
        Type = skill.Type.ToString(),
        Level = skill.Level,
        Status = skill.Status.ToString(),
        CreatedAt = skill.CreatedAt,
        UpdatedAt = skill.UpdatedAt
    };
}
