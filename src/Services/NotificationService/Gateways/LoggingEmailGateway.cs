using Aspirra.NotificationService.Interfaces;

namespace Aspirra.NotificationService.Gateways;

// Development email gateway: writes the email to the log instead of calling a real email provider.
public class LoggingEmailGateway : IEmailGateway
{
    private readonly ILogger<LoggingEmailGateway> _logger;

    public LoggingEmailGateway(ILogger<LoggingEmailGateway> logger)
    {
        _logger = logger;
    }

    public Task SendAsync(string to, string subject, string body, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Email delivery simulated.");
        return Task.CompletedTask;
    }
}
