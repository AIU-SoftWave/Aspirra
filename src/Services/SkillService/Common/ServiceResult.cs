namespace Aspirra.SkillService.Common;

public static class SkillErrorCodes
{
    public const string RequiredField = "REQUIRED_FIELD";
    public const string InvalidType = "INVALID_TYPE";
    public const string InvalidLevel = "INVALID_LEVEL";
    public const string DuplicateSkill = "DUPLICATE_SKILL";
    public const string InvalidTransition = "INVALID_TRANSITION";
    public const string NotFound = "NOT_FOUND";
}

public sealed class ServiceResult<T>
{
    private ServiceResult(bool success, T? value, string? code, string? message)
    {
        Success = success;
        Value = value;
        Code = code;
        Message = message;
    }

    public bool Success { get; }
    public T? Value { get; }
    public string? Code { get; }
    public string? Message { get; }

    public static ServiceResult<T> Ok(T value) => new(true, value, null, null);

    public static ServiceResult<T> Fail(string code, string message) => new(false, default, code, message);
}
