using Aspirra.SkillService.Models;

namespace Aspirra.SkillService.Interfaces;

public interface ISkillRepository
{
    Task<Skill?> GetByNameAsync(string studentId, string name, CancellationToken cancellationToken = default);
    Task<Skill?> GetByIdAsync(string skillId, CancellationToken cancellationToken = default);
    Task<List<Skill>> GetByStudentAsync(string studentId, CancellationToken cancellationToken = default);
    Task<Skill> AddAsync(Skill skill, CancellationToken cancellationToken = default);
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}
