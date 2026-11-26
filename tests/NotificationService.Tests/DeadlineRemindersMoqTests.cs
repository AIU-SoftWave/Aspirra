using Aspirra.NotificationService.Common;
using Aspirra.NotificationService.Interfaces;
using Aspirra.NotificationService.Models;
using Moq;

namespace Aspirra.NotificationService.Tests;

// Each mocked message of deadline-reminders-collaboration.puml is checked with Moq Verify.
public class DeadlineRemindersMoqTests
{
    private readonly Mock<IAssessment> _assessment = new(MockBehavior.Strict);
    private readonly Mock<IEmailGateway> _emailGateway = new(MockBehavior.Strict);
    private readonly Mock<INotificationRepository> _repository = new(MockBehavior.Strict);

    private Services.NotificationService CreateService() =>
        new(_assessment.Object, _emailGateway.Object, _repository.Object);

    private void SetupDeadlines(params Deadline[] deadlines) =>
        _assessment
            .Setup(a => a.GetDeadlinesAsync(NotificationFixtures.StudentId, NotificationFixtures.Days, It.IsAny<CancellationToken>()))
            .ReturnsAsync(deadlines.ToList());

    private void SetupRepositoryWrites()
    {
        _repository
            .Setup(r => r.AddAsync(It.IsAny<Notification>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Notification notification, CancellationToken _) => notification);
        _repository
            .Setup(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);
    }

    private void SetupEmailSucceeds() =>
        _emailGateway
            .Setup(g => g.SendAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

    private void SetupEmailFails() =>
        _emailGateway
            .Setup(g => g.SendAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new HttpRequestException("SMTP server unavailable"));

    [Fact]
    public async Task SendReminders_WhenEmailSucceeds_SavesSentNotification()
    {
        SetupDeadlines(NotificationFixtures.DatabaseProjectDeadline());
        SetupRepositoryWrites();
        SetupEmailSucceeds();

        var result = await CreateService().SendDeadlineRemindersAsync(
            NotificationFixtures.StudentId, NotificationFixtures.Email, NotificationFixtures.Days);

        Assert.True(result.Success);
        var notification = Assert.Single(result.Value!);
        Assert.Equal(NotificationStatus.Sent, notification.Status);
        Assert.Equal(1, notification.Attempts);
        Assert.NotNull(notification.SentAt);
        Assert.Equal(NotificationFixtures.AssignmentId, notification.AssessmentId);

        _assessment.Verify(
            a => a.GetDeadlinesAsync(NotificationFixtures.StudentId, NotificationFixtures.Days, It.IsAny<CancellationToken>()),
            Times.Once);
        _repository.Verify(
            r => r.AddAsync(
                It.Is<Notification>(n => n.AssessmentId == NotificationFixtures.AssignmentId &&
                                         n.Recipient == NotificationFixtures.Email),
                It.IsAny<CancellationToken>()),
            Times.Once);
        _emailGateway.Verify(
            g => g.SendAsync(
                NotificationFixtures.Email,
                It.Is<string>(subject => subject.Contains("Database Project")),
                It.IsAny<string>(),
                It.IsAny<CancellationToken>()),
            Times.Once);
        _repository.Verify(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task SendReminders_WhenEmailFails_SavesFailedNotification()
    {
        SetupDeadlines(NotificationFixtures.DatabaseProjectDeadline());
        SetupRepositoryWrites();
        SetupEmailFails();

        var result = await CreateService().SendDeadlineRemindersAsync(
            NotificationFixtures.StudentId, NotificationFixtures.Email, NotificationFixtures.Days);

        Assert.True(result.Success);
        var notification = Assert.Single(result.Value!);
        Assert.Equal(NotificationStatus.Failed, notification.Status);
        Assert.Equal(1, notification.Attempts);
        Assert.Null(notification.SentAt);
        _emailGateway.Verify(
            g => g.SendAsync(NotificationFixtures.Email, It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()),
            Times.Once);
        _repository.Verify(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task SendReminders_WithCompletedDeadline_SendsNothing()
    {
        SetupDeadlines(NotificationFixtures.CompletedDeadline());

        var result = await CreateService().SendDeadlineRemindersAsync(
            NotificationFixtures.StudentId, NotificationFixtures.Email, NotificationFixtures.Days);

        Assert.True(result.Success);
        Assert.Empty(result.Value!);
        _repository.Verify(r => r.AddAsync(It.IsAny<Notification>(), It.IsAny<CancellationToken>()), Times.Never);
        _emailGateway.Verify(
            g => g.SendAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()),
            Times.Never);
        _repository.Verify(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task SendReminders_WithNoDeadlines_DoesNotSave()
    {
        SetupDeadlines();

        var result = await CreateService().SendDeadlineRemindersAsync(
            NotificationFixtures.StudentId, NotificationFixtures.Email, NotificationFixtures.Days);

        Assert.True(result.Success);
        Assert.Empty(result.Value!);
        _repository.Verify(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task SendReminders_WithTwoDeadlines_SendsTwoEmailsAndSavesOnce()
    {
        var exam = NotificationFixtures.DatabaseProjectDeadline();
        exam.AssessmentId = "exam-4001";
        exam.Title = "Data Mining Midterm";
        exam.Kind = "Exam";
        SetupDeadlines(NotificationFixtures.DatabaseProjectDeadline(), exam);
        SetupRepositoryWrites();
        SetupEmailSucceeds();

        var result = await CreateService().SendDeadlineRemindersAsync(
            NotificationFixtures.StudentId, NotificationFixtures.Email, NotificationFixtures.Days);

        Assert.Equal(2, result.Value!.Count);
        Assert.All(result.Value, notification => Assert.Equal(NotificationStatus.Sent, notification.Status));
        _repository.Verify(r => r.AddAsync(It.IsAny<Notification>(), It.IsAny<CancellationToken>()), Times.Exactly(2));
        _emailGateway.Verify(
            g => g.SendAsync(NotificationFixtures.Email, It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()),
            Times.Exactly(2));
        _repository.Verify(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Theory]
    [InlineData("", "student1001@aiu.edu.eg", 1, NotificationErrorCodes.RequiredField)]
    [InlineData("student-1001", "", 1, NotificationErrorCodes.RequiredField)]
    [InlineData("student-1001", "not-an-email", 1, NotificationErrorCodes.RequiredField)]
    [InlineData("student-1001", "student1001@aiu.edu.eg", 0, NotificationErrorCodes.InvalidDays)]
    [InlineData("student-1001", "student1001@aiu.edu.eg", 31, NotificationErrorCodes.InvalidDays)]
    public async Task SendReminders_WithInvalidInput_IsRejectedBeforeAnyCall(
        string studentId,
        string email,
        int days,
        string expectedCode)
    {
        var result = await CreateService().SendDeadlineRemindersAsync(studentId, email, days);

        Assert.False(result.Success);
        Assert.Equal(expectedCode, result.Code);
        _assessment.Verify(
            a => a.GetDeadlinesAsync(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<CancellationToken>()),
            Times.Never);
        _repository.Verify(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task Retry_FromFailed_SendsAndMarksSent()
    {
        var notification = NotificationFixtures.NotificationWithStatus(NotificationStatus.Failed);
        _repository
            .Setup(r => r.GetByIdAsync(NotificationFixtures.NotificationId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(notification);
        SetupRepositoryWrites();
        SetupEmailSucceeds();

        var result = await CreateService().RetryAsync(NotificationFixtures.NotificationId);

        Assert.True(result.Success);
        Assert.Equal(NotificationStatus.Sent, notification.Status);
        Assert.Equal(2, notification.Attempts);
        _emailGateway.Verify(
            g => g.SendAsync(NotificationFixtures.Email, notification.Subject, notification.Message, It.IsAny<CancellationToken>()),
            Times.Once);
        _repository.Verify(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Retry_WhenEmailFailsAgain_StaysFailedAndReturnsSendFailed()
    {
        var notification = NotificationFixtures.NotificationWithStatus(NotificationStatus.Failed);
        _repository
            .Setup(r => r.GetByIdAsync(NotificationFixtures.NotificationId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(notification);
        SetupRepositoryWrites();
        SetupEmailFails();

        var result = await CreateService().RetryAsync(NotificationFixtures.NotificationId);

        Assert.False(result.Success);
        Assert.Equal(NotificationErrorCodes.SendFailed, result.Code);
        Assert.Equal(NotificationStatus.Failed, notification.Status);
        Assert.Equal(2, notification.Attempts);
        _repository.Verify(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Theory]
    [InlineData(NotificationStatus.Sent)]
    [InlineData(NotificationStatus.Pending)]
    public async Task Retry_WhenNotFailed_IsRejectedWithoutSending(NotificationStatus status)
    {
        var notification = NotificationFixtures.NotificationWithStatus(status);
        _repository
            .Setup(r => r.GetByIdAsync(NotificationFixtures.NotificationId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(notification);

        var result = await CreateService().RetryAsync(NotificationFixtures.NotificationId);

        Assert.False(result.Success);
        Assert.Equal(NotificationErrorCodes.InvalidTransition, result.Code);
        Assert.Equal(status, notification.Status);
        _emailGateway.Verify(
            g => g.SendAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()),
            Times.Never);
        _repository.Verify(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task Retry_WhenMissing_ReturnsNotFound()
    {
        _repository
            .Setup(r => r.GetByIdAsync("missing", It.IsAny<CancellationToken>()))
            .ReturnsAsync((Notification?)null);

        var result = await CreateService().RetryAsync("missing");

        Assert.False(result.Success);
        Assert.Equal(NotificationErrorCodes.NotFound, result.Code);
        _repository.Verify(r => r.GetByIdAsync("missing", It.IsAny<CancellationToken>()), Times.Once);
        _repository.Verify(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task GetNotification_WhenExists_ReturnsIt()
    {
        _repository
            .Setup(r => r.GetByIdAsync(NotificationFixtures.NotificationId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(NotificationFixtures.NotificationWithStatus(NotificationStatus.Sent));

        var result = await CreateService().GetNotificationAsync(NotificationFixtures.NotificationId);

        Assert.True(result.Success);
        _repository.Verify(r => r.GetByIdAsync(NotificationFixtures.NotificationId, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task GetNotification_WithEmptyId_ReturnsNotFoundWithoutRepositoryCall()
    {
        var result = await CreateService().GetNotificationAsync(" ");

        Assert.False(result.Success);
        Assert.Equal(NotificationErrorCodes.NotFound, result.Code);
        _repository.Verify(r => r.GetByIdAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Never);
    }
}
