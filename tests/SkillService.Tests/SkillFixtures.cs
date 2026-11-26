using Aspirra.SkillService.Dtos;
using Aspirra.SkillService.Models;

namespace Aspirra.SkillService.Tests;

// The objects of the Add Skill object diagram (add-skill-object.puml) used as test fixtures.
internal static class SkillFixtures
{
    public const string StudentId = "23101371";
    public const string SkillId = "skill-2001";
    public const string SkillName = "SQL";

    public static AddSkillRequest AddSqlRequest() => new()
    {
        StudentId = StudentId,
        Name = SkillName,
        Type = "Technical",
        Level = 3
    };

    public static Skill ExistingSqlSkill() => new()
    {
        SkillId = SkillId,
        StudentId = StudentId,
        Name = SkillName,
        Type = SkillType.Technical,
        Level = 3,
        Status = SkillStatus.Active,
        CreatedAt = new DateTime(2026, 11, 20, 10, 0, 0, DateTimeKind.Utc),
        UpdatedAt = new DateTime(2026, 11, 20, 10, 0, 0, DateTimeKind.Utc)
    };
}
