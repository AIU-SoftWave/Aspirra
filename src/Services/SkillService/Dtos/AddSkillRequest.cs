namespace Aspirra.SkillService.Dtos;

public sealed class AddSkillRequest
{
    public string? StudentId { get; set; }
    public string? Name { get; set; }
    public string? Type { get; set; }
    public int? Level { get; set; }
}
