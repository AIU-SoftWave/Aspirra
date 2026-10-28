namespace Aspirra.CourseService.Models;

public enum ServiceResultStatus
{
    Success,
    NotFound,
    RequiredField,
    DuplicateCourse
}

public sealed class ServiceResult<T>
{
    private ServiceResult(ServiceResultStatus status, T? value, string? code, string? message)
    {
        Status = status;
        Value = value;
        Code = code;
        Message = message;
    }

    public ServiceResultStatus Status { get; }
    public T? Value { get; }
    public string? Code { get; }
    public string? Message { get; }
    public bool IsSuccess => Status == ServiceResultStatus.Success;

    public static ServiceResult<T> Success(T value) =>
        new(ServiceResultStatus.Success, value, null, null);

    public static ServiceResult<T> NotFound(string message) =>
        new(ServiceResultStatus.NotFound, default, "NOT_FOUND", message);

    public static ServiceResult<T> RequiredField(string field) =>
        new(ServiceResultStatus.RequiredField, default, "REQUIRED_FIELD", $"{field} is required.");

    public static ServiceResult<T> DuplicateCourse(string message) =>
        new(ServiceResultStatus.DuplicateCourse, default, "DUPLICATE_COURSE", message);
}
