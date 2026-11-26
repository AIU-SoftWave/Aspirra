using Aspirra.SkillService.Common;
using Aspirra.SkillService.Dtos;
using Aspirra.SkillService.Interfaces;
using Aspirra.SkillService.Models;

namespace Aspirra.SkillService.Services;

public class SkillService : ISkill
{
    private readonly ISkillRepository _repository;

    public SkillService(ISkillRepository repository)
    {
        _repository = repository;
    }

    public async Task<ServiceResult<Skill>> AddSkillAsync(
        string studentId,
        AddSkillRequest request,
        CancellationToken cancellationToken = default)
    {
        var ownerId = string.IsNullOrWhiteSpace(studentId) ? request.StudentId : studentId;
        if (string.IsNullOrWhiteSpace(ownerId))
        {
            return ServiceResult<Skill>.Fail(SkillErrorCodes.RequiredField, "StudentId is required.");
        }

        if (string.IsNullOrWhiteSpace(request.Name))
        {
            return ServiceResult<Skill>.Fail(SkillErrorCodes.RequiredField, "Name is required.");
        }

        if (string.IsNullOrWhiteSpace(request.Type))
        {
            return ServiceResult<Skill>.Fail(SkillErrorCodes.RequiredField, "Type is required.");
        }

        if (!Enum.TryParse<SkillType>(request.Type.Trim(), true, out var type) || !Enum.IsDefined(type))
        {
            return ServiceResult<Skill>.Fail(SkillErrorCodes.InvalidType, "Type must be Technical or Soft.");
        }

        if (request.Level is null)
        {
            return ServiceResult<Skill>.Fail(SkillErrorCodes.RequiredField, "Level is required.");
        }

        if (request.Level < Skill.MinLevel || request.Level > Skill.MaxLevel)
        {
            return ServiceResult<Skill>.Fail(SkillErrorCodes.InvalidLevel, "Level must be between 1 and 5.");
        }

        var now = DateTime.UtcNow;
        var skill = new Skill
        {
            SkillId = Guid.NewGuid().ToString(),
            StudentId = ownerId.Trim(),
            Name = request.Name.Trim(),
            Type = type,
            Level = request.Level.Value,
            Status = SkillStatus.New,
            CreatedAt = now,
            UpdatedAt = now
        };

        var existing = await _repository.GetByNameAsync(skill.StudentId, skill.Name, cancellationToken);
        if (existing is not null)
        {
            return ServiceResult<Skill>.Fail(SkillErrorCodes.DuplicateSkill, "Skill already added");
        }

        skill.Activate();
        await _repository.AddAsync(skill, cancellationToken);
        await _repository.SaveChangesAsync(cancellationToken);

        return ServiceResult<Skill>.Ok(skill);
    }

    public async Task<List<Skill>> GetSkillsAsync(string studentId, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(studentId))
        {
            return new List<Skill>();
        }

        return await _repository.GetByStudentAsync(studentId.Trim(), cancellationToken);
    }

    public async Task<ServiceResult<Skill>> GetSkillAsync(string skillId, CancellationToken cancellationToken = default)
    {
        var skill = string.IsNullOrWhiteSpace(skillId)
            ? null
            : await _repository.GetByIdAsync(skillId, cancellationToken);

        return skill is null
            ? ServiceResult<Skill>.Fail(SkillErrorCodes.NotFound, "Skill not found")
            : ServiceResult<Skill>.Ok(skill);
    }
}
