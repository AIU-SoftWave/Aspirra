namespace Aspirra.NotificationService.Dtos;

public sealed class SendDeadlineRemindersRequest
{
    public string? StudentId { get; set; }
    public string? Email { get; set; }
    public int? Days { get; set; }
}
