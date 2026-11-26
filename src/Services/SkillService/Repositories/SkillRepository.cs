using Aspirra.SkillService.Data;
using Aspirra.SkillService.Interfaces;
using Aspirra.SkillService.Models;
using Microsoft.EntityFrameworkCore;

namespace Aspirra.SkillService.Repositories;

public class SkillRepository : ISkillRepository
{
    private readonly SkillDbContext _context;

    public SkillRepository(SkillDbContext context)
    {
        _context = context;
    }

    public Task<Skill?> GetByNameAsync(string studentId, string name, CancellationToken cancellationToken = default)
    {
        var normalizedName = name.Trim().ToLower();
        return _context.Skills.FirstOrDefaultAsync(
            skill => skill.StudentId == studentId && skill.Name.ToLower() == normalizedName,
            cancellationToken);
    }

    public Task<Skill?> GetByIdAsync(string skillId, CancellationToken cancellationToken = default) =>
        _context.Skills.FirstOrDefaultAsync(skill => skill.SkillId == skillId, cancellationToken);

    public Task<List<Skill>> GetByStudentAsync(string studentId, CancellationToken cancellationToken = default) =>
        _context.Skills
            .AsNoTracking()
            .Where(skill => skill.StudentId == studentId)
            .OrderBy(skill => skill.Name)
            .ToListAsync(cancellationToken);

    public async Task<Skill> AddAsync(Skill skill, CancellationToken cancellationToken = default)
    {
        await _context.Skills.AddAsync(skill, cancellationToken);
        return skill;
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken = default) =>
        _context.SaveChangesAsync(cancellationToken);
}
