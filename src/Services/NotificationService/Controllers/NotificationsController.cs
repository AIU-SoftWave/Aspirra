using Aspirra.NotificationService.Common;
using Aspirra.NotificationService.Dtos;
using Aspirra.NotificationService.Interfaces;
using Aspirra.NotificationService.Models;
using Microsoft.AspNetCore.Mvc;

namespace Aspirra.NotificationService.Controllers;

[ApiController]
[Route("api/notifications")]
public class NotificationsController : ControllerBase
{
    private const int DefaultDays = 1;
    private readonly INotification _notificationService;

    public NotificationsController(INotification notificationService)
    {
        _notificationService = notificationService;
    }

    [HttpPost("deadline-reminders")]
    public async Task<IActionResult> SendDeadlineReminders(
        [FromBody] SendDeadlineRemindersRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _notificationService.SendDeadlineRemindersAsync(
            request.StudentId ?? string.Empty,
            request.Email ?? string.Empty,
            request.Days ?? DefaultDays,
            cancellationToken);

        if (!result.Success)
        {
            return ToErrorResult(result);
        }

        return Ok(result.Value!.Select(ToResponse));
    }

    [HttpPost("{notificationId}/retry")]
    public async Task<IActionResult> Retry(string notificationId, CancellationToken cancellationToken)
    {
        var result = await _notificationService.RetryAsync(notificationId, cancellationToken);

        if (!result.Success)
        {
            return ToErrorResult(result);
        }

        return Ok(ToResponse(result.Value!));
    }

    [HttpGet("{notificationId}")]
    public async Task<IActionResult> GetNotification(string notificationId, CancellationToken cancellationToken)
    {
        var result = await _notificationService.GetNotificationAsync(notificationId, cancellationToken);

        if (!result.Success)
        {
            return ToErrorResult(result);
        }

        return Ok(ToResponse(result.Value!));
    }

    private IActionResult ToErrorResult<T>(ServiceResult<T> result)
    {
        var body = new { code = result.Code, message = result.Message };

        return result.Code switch
        {
            NotificationErrorCodes.NotFound => NotFound(body),
            NotificationErrorCodes.SendFailed => StatusCode(StatusCodes.Status502BadGateway, body),
            _ => BadRequest(body)
        };
    }

    private static NotificationResponse ToResponse(Notification notification) => new()
    {
        NotificationId = notification.NotificationId,
        StudentId = notification.StudentId,
        AssessmentId = notification.AssessmentId,
        Recipient = notification.Recipient,
        Type = notification.Type,
        Subject = notification.Subject,
        Message = notification.Message,
        Status = notification.Status.ToString(),
        Attempts = notification.Attempts,
        SentAt = notification.SentAt,
        CreatedAt = notification.CreatedAt,
        UpdatedAt = notification.UpdatedAt
    };
}
