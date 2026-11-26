using Aspirra.SkillService.Common;
using Aspirra.SkillService.Exceptions;
using Aspirra.SkillService.Interfaces;
using Aspirra.SkillService.Models;
using Moq;

namespace Aspirra.SkillService.Tests;

// One test per transition of skill-state.puml, valid and invalid.
public class SkillStateTransitionTests
{
    [Fact]
    public void NewSkill_StartsInNewState()
    {
        var skill = new Skill();

        Assert.Equal(SkillStatus.New, skill.Status);
    }

    [Fact]
    public void Activate_FromNew_BecomesActive()
    {
        var skill = new Skill { Status = SkillStatus.New };

        skill.Activate();

        Assert.Equal(SkillStatus.Active, skill.Status);
    }

    [Fact]
    public void Activate_FromActive_ThrowsInvalidTransition()
    {
        var skill = SkillFixtures.ExistingSqlSkill();

        var exception = Assert.Throws<SkillTransitionException>(() => skill.Activate());

        Assert.Equal(SkillErrorCodes.InvalidTransition, exception.Code);
        Assert.Equal(SkillStatus.Active, skill.Status);
    }

    [Fact]
    public async Task AddSkill_WhenDuplicate_SkillIsNeverActivatedOrSaved()
    {
        Skill? captured = null;
        var repository = new Mock<ISkillRepository>(MockBehavior.Strict);
        repository
            .Setup(r => r.GetByNameAsync(SkillFixtures.StudentId, SkillFixtures.SkillName, It.IsAny<CancellationToken>()))
            .ReturnsAsync(SkillFixtures.ExistingSqlSkill());
        repository
            .Setup(r => r.AddAsync(It.IsAny<Skill>(), It.IsAny<CancellationToken>()))
            .Callback((Skill skill, CancellationToken _) => captured = skill)
            .ReturnsAsync((Skill skill, CancellationToken _) => skill);
        var service = new Services.SkillService(repository.Object);

        var result = await service.AddSkillAsync(SkillFixtures.StudentId, SkillFixtures.AddSqlRequest());

        Assert.False(result.Success);
        Assert.Equal(SkillErrorCodes.DuplicateSkill, result.Code);
        Assert.Null(captured);
        repository.Verify(r => r.AddAsync(It.IsAny<Skill>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task AddSkill_WhenUnique_IsSavedInActiveState()
    {
        Skill? captured = null;
        var repository = new Mock<ISkillRepository>(MockBehavior.Strict);
        repository
            .Setup(r => r.GetByNameAsync(SkillFixtures.StudentId, SkillFixtures.SkillName, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Skill?)null);
        repository
            .Setup(r => r.AddAsync(It.IsAny<Skill>(), It.IsAny<CancellationToken>()))
            .Callback((Skill skill, CancellationToken _) => captured = skill)
            .ReturnsAsync((Skill skill, CancellationToken _) => skill);
        repository
            .Setup(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);
        var service = new Services.SkillService(repository.Object);

        await service.AddSkillAsync(SkillFixtures.StudentId, SkillFixtures.AddSqlRequest());

        Assert.NotNull(captured);
        Assert.Equal(SkillStatus.Active, captured!.Status);
        repository.Verify(r => r.AddAsync(It.IsAny<Skill>(), It.IsAny<CancellationToken>()), Times.Once);
        repository.Verify(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}
