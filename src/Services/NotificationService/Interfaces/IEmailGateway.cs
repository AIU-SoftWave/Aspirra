namespace Aspirra.NotificationService.Interfaces;

// Required interface: external email gateway.
public interface IEmailGateway
{
    Task SendAsync(string to, string subject, string body, CancellationToken cancellationToken = default);
}
