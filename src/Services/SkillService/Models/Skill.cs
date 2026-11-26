using Aspirra.SkillService.Exceptions;

namespace Aspirra.SkillService.Models;

public class Skill
{
    public const int MinLevel = 1;
    public const int MaxLevel = 5;

    public string SkillId { get; set; } = string.Empty;
    public string StudentId { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public SkillType Type { get; set; }
    public int Level { get; set; }
    public SkillStatus Status { get; set; } = SkillStatus.New;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public void Activate()
    {
        if (Status != SkillStatus.New)
        {
            throw new SkillTransitionException("Only a new skill can be activated.");
        }

        Status = SkillStatus.Active;
        UpdatedAt = DateTime.UtcNow;
    }
}
