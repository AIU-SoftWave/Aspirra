using Aspirra.NotificationService.Common;

namespace Aspirra.NotificationService.Exceptions;

public class NotificationTransitionException : InvalidOperationException
{
    public NotificationTransitionException(string message) : base(message)
    {
    }

    public string Code => NotificationErrorCodes.InvalidTransition;
}
