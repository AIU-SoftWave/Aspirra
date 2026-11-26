using Aspirra.NotificationService.Common;
using Aspirra.NotificationService.Exceptions;
using Aspirra.NotificationService.Models;

namespace Aspirra.NotificationService.Tests;

// One test per transition of notification-state.puml, valid and invalid.
public class NotificationStateTransitionTests
{
    private static readonly DateTime Now = new(2026, 11, 26, 9, 0, 0, DateTimeKind.Utc);

    [Fact]
    public void NewNotification_StartsPending()
    {
        Assert.Equal(NotificationStatus.Pending, new Notification().Status);
    }

    [Fact]
    public void MarkSent_FromPending_BecomesSent()
    {
        var notification = NotificationFixtures.NotificationWithStatus(NotificationStatus.Pending);

        notification.MarkSent(Now);

        Assert.Equal(NotificationStatus.Sent, notification.Status);
        Assert.Equal(Now, notification.SentAt);
        Assert.Equal(1, notification.Attempts);
    }

    [Fact]
    public void MarkFailed_FromPending_BecomesFailed()
    {
        var notification = NotificationFixtures.NotificationWithStatus(NotificationStatus.Pending);

        notification.MarkFailed(Now);

        Assert.Equal(NotificationStatus.Failed, notification.Status);
        Assert.Null(notification.SentAt);
        Assert.Equal(1, notification.Attempts);
    }

    [Fact]
    public void MarkSent_FromFailed_RetryBecomesSent()
    {
        var notification = NotificationFixtures.NotificationWithStatus(NotificationStatus.Failed);

        Assert.True(notification.CanRetry);
        notification.MarkSent(Now);

        Assert.Equal(NotificationStatus.Sent, notification.Status);
        Assert.Equal(2, notification.Attempts);
    }

    [Fact]
    public void MarkFailed_FromFailed_RetryFailsAgainAndStaysFailed()
    {
        var notification = NotificationFixtures.NotificationWithStatus(NotificationStatus.Failed);

        notification.MarkFailed(Now);

        Assert.Equal(NotificationStatus.Failed, notification.Status);
        Assert.Equal(2, notification.Attempts);
    }

    [Fact]
    public void MarkSent_FromSent_ThrowsInvalidTransition()
    {
        var notification = NotificationFixtures.NotificationWithStatus(NotificationStatus.Sent);

        var exception = Assert.Throws<NotificationTransitionException>(() => notification.MarkSent(Now));

        Assert.Equal(NotificationErrorCodes.InvalidTransition, exception.Code);
        Assert.Equal(NotificationStatus.Sent, notification.Status);
    }

    [Fact]
    public void MarkFailed_FromSent_ThrowsInvalidTransition()
    {
        var notification = NotificationFixtures.NotificationWithStatus(NotificationStatus.Sent);

        Assert.Throws<NotificationTransitionException>(() => notification.MarkFailed(Now));
        Assert.Equal(NotificationStatus.Sent, notification.Status);
    }

    [Theory]
    [InlineData(NotificationStatus.Sent)]
    [InlineData(NotificationStatus.Pending)]
    public void CanRetry_WhenNotFailed_IsFalse(NotificationStatus status)
    {
        var notification = NotificationFixtures.NotificationWithStatus(status);

        Assert.False(notification.CanRetry);
    }
}
