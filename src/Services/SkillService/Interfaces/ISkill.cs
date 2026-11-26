using Aspirra.SkillService.Common;
using Aspirra.SkillService.Dtos;
using Aspirra.SkillService.Models;

namespace Aspirra.SkillService.Interfaces;

public interface ISkill
{
    Task<ServiceResult<Skill>> AddSkillAsync(string studentId, AddSkillRequest request, CancellationToken cancellationToken = default);
    Task<List<Skill>> GetSkillsAsync(string studentId, CancellationToken cancellationToken = default);
    Task<ServiceResult<Skill>> GetSkillAsync(string skillId, CancellationToken cancellationToken = default);
}
