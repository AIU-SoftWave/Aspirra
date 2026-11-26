using Aspirra.SkillService.Common;
using Aspirra.SkillService.Dtos;
using Aspirra.SkillService.Interfaces;
using Aspirra.SkillService.Models;
using Moq;

namespace Aspirra.SkillService.Tests;

// Each mocked message of add-skill-collaboration.puml is checked with Moq Verify.
public class AddSkillMoqTests
{
    private static Mock<ISkillRepository> RepositoryWithoutDuplicate()
    {
        var repository = new Mock<ISkillRepository>(MockBehavior.Strict);
        repository
            .Setup(r => r.GetByNameAsync(SkillFixtures.StudentId, SkillFixtures.SkillName, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Skill?)null);
        repository
            .Setup(r => r.AddAsync(It.IsAny<Skill>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Skill skill, CancellationToken _) => skill);
        repository
            .Setup(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);
        return repository;
    }

    [Fact]
    public async Task AddSkill_WithValidInput_SavesActiveSkillOnce()
    {
        var repository = RepositoryWithoutDuplicate();
        var service = new Services.SkillService(repository.Object);

        var result = await service.AddSkillAsync(SkillFixtures.StudentId, SkillFixtures.AddSqlRequest());

        Assert.True(result.Success);
        Assert.Equal(SkillFixtures.SkillName, result.Value!.Name);
        Assert.Equal(SkillType.Technical, result.Value.Type);
        Assert.Equal(3, result.Value.Level);
        Assert.Equal(SkillStatus.Active, result.Value.Status);
        Assert.False(string.IsNullOrWhiteSpace(result.Value.SkillId));

        repository.Verify(
            r => r.GetByNameAsync(SkillFixtures.StudentId, SkillFixtures.SkillName, It.IsAny<CancellationToken>()),
            Times.Once);
        repository.Verify(
            r => r.AddAsync(
                It.Is<Skill>(s => s.Name == SkillFixtures.SkillName &&
                                  s.StudentId == SkillFixtures.StudentId &&
                                  s.Status == SkillStatus.Active),
                It.IsAny<CancellationToken>()),
            Times.Once);
        repository.Verify(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task AddSkill_TrimsNameAndParsesTypeIgnoringCase()
    {
        var repository = RepositoryWithoutDuplicate();
        var service = new Services.SkillService(repository.Object);
        var request = SkillFixtures.AddSqlRequest();
        request.Name = "  SQL  ";
        request.Type = "technical";

        var result = await service.AddSkillAsync(SkillFixtures.StudentId, request);

        Assert.True(result.Success);
        Assert.Equal("SQL", result.Value!.Name);
        repository.Verify(
            r => r.GetByNameAsync(SkillFixtures.StudentId, "SQL", It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task AddSkill_UsesStudentIdFromRequest_WhenArgumentIsEmpty()
    {
        var repository = RepositoryWithoutDuplicate();
        var service = new Services.SkillService(repository.Object);

        var result = await service.AddSkillAsync(string.Empty, SkillFixtures.AddSqlRequest());

        Assert.True(result.Success);
        Assert.Equal(SkillFixtures.StudentId, result.Value!.StudentId);
    }

    [Fact]
    public async Task AddSkill_WhenDuplicateName_ReturnsDuplicateAndDoesNotPersist()
    {
        var repository = new Mock<ISkillRepository>(MockBehavior.Strict);
        repository
            .Setup(r => r.GetByNameAsync(SkillFixtures.StudentId, SkillFixtures.SkillName, It.IsAny<CancellationToken>()))
            .ReturnsAsync(SkillFixtures.ExistingSqlSkill());
        var service = new Services.SkillService(repository.Object);

        var result = await service.AddSkillAsync(SkillFixtures.StudentId, SkillFixtures.AddSqlRequest());

        Assert.False(result.Success);
        Assert.Equal(SkillErrorCodes.DuplicateSkill, result.Code);
        Assert.Equal("Skill already added", result.Message);
        repository.Verify(
            r => r.GetByNameAsync(SkillFixtures.StudentId, SkillFixtures.SkillName, It.IsAny<CancellationToken>()),
            Times.Once);
        repository.Verify(r => r.AddAsync(It.IsAny<Skill>(), It.IsAny<CancellationToken>()), Times.Never);
        repository.Verify(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }

    [Theory]
    [InlineData(null, "SQL", "Technical", 3, SkillErrorCodes.RequiredField)]
    [InlineData("23101371", null, "Technical", 3, SkillErrorCodes.RequiredField)]
    [InlineData("23101371", "   ", "Technical", 3, SkillErrorCodes.RequiredField)]
    [InlineData("23101371", "SQL", null, 3, SkillErrorCodes.RequiredField)]
    [InlineData("23101371", "SQL", "Hardware", 3, SkillErrorCodes.InvalidType)]
    [InlineData("23101371", "SQL", "7", 3, SkillErrorCodes.InvalidType)]
    [InlineData("23101371", "SQL", "Technical", null, SkillErrorCodes.RequiredField)]
    [InlineData("23101371", "SQL", "Technical", 0, SkillErrorCodes.InvalidLevel)]
    [InlineData("23101371", "SQL", "Technical", 6, SkillErrorCodes.InvalidLevel)]
    public async Task AddSkill_WithMissingOrInvalidField_IsRejectedBeforeRepository(
        string? studentId,
        string? name,
        string? type,
        int? level,
        string expectedCode)
    {
        var repository = new Mock<ISkillRepository>(MockBehavior.Strict);
        var service = new Services.SkillService(repository.Object);
        var request = new AddSkillRequest { StudentId = studentId, Name = name, Type = type, Level = level };

        var result = await service.AddSkillAsync(studentId ?? string.Empty, request);

        Assert.False(result.Success);
        Assert.Equal(expectedCode, result.Code);
        repository.Verify(
            r => r.GetByNameAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()),
            Times.Never);
        repository.Verify(r => r.AddAsync(It.IsAny<Skill>(), It.IsAny<CancellationToken>()), Times.Never);
        repository.Verify(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task GetSkill_WhenMissing_ReturnsNotFound()
    {
        var repository = new Mock<ISkillRepository>(MockBehavior.Strict);
        repository
            .Setup(r => r.GetByIdAsync("missing", It.IsAny<CancellationToken>()))
            .ReturnsAsync((Skill?)null);
        var service = new Services.SkillService(repository.Object);

        var result = await service.GetSkillAsync("missing");

        Assert.False(result.Success);
        Assert.Equal(SkillErrorCodes.NotFound, result.Code);
        repository.Verify(r => r.GetByIdAsync("missing", It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task GetSkill_WithEmptyId_ReturnsNotFoundWithoutRepositoryCall()
    {
        var repository = new Mock<ISkillRepository>(MockBehavior.Strict);
        var service = new Services.SkillService(repository.Object);

        var result = await service.GetSkillAsync(" ");

        Assert.False(result.Success);
        Assert.Equal(SkillErrorCodes.NotFound, result.Code);
        repository.Verify(r => r.GetByIdAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task GetSkill_WhenExists_ReturnsSkill()
    {
        var repository = new Mock<ISkillRepository>(MockBehavior.Strict);
        repository
            .Setup(r => r.GetByIdAsync(SkillFixtures.SkillId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(SkillFixtures.ExistingSqlSkill());
        var service = new Services.SkillService(repository.Object);

        var result = await service.GetSkillAsync(SkillFixtures.SkillId);

        Assert.True(result.Success);
        Assert.Equal(SkillFixtures.SkillName, result.Value!.Name);
        repository.Verify(r => r.GetByIdAsync(SkillFixtures.SkillId, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task GetSkills_ReturnsSkillsOfStudent()
    {
        var repository = new Mock<ISkillRepository>(MockBehavior.Strict);
        repository
            .Setup(r => r.GetByStudentAsync(SkillFixtures.StudentId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<Skill> { SkillFixtures.ExistingSqlSkill() });
        var service = new Services.SkillService(repository.Object);

        var skills = await service.GetSkillsAsync(SkillFixtures.StudentId);

        Assert.Single(skills);
        repository.Verify(r => r.GetByStudentAsync(SkillFixtures.StudentId, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task GetSkills_WithoutStudentId_ReturnsEmptyWithoutRepositoryCall()
    {
        var repository = new Mock<ISkillRepository>(MockBehavior.Strict);
        var service = new Services.SkillService(repository.Object);

        var skills = await service.GetSkillsAsync(string.Empty);

        Assert.Empty(skills);
        repository.Verify(r => r.GetByStudentAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Never);
    }
}
