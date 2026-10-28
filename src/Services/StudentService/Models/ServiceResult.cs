namespace Aspirra.StudentService.Models
{
    public enum ServiceResultStatus
    {
        Success,
        NotFound,
        RequiredField,
        DuplicateProfile,
        InvalidTimeSlot
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

        public static ServiceResult<T> DuplicateProfile(string message) =>
            new(ServiceResultStatus.DuplicateProfile, default, "DUPLICATE_PROFILE", message);

        public static ServiceResult<T> InvalidTimeSlot() =>
            new(ServiceResultStatus.InvalidTimeSlot, default, "INVALID_TIME_SLOT", "Invalid time slot");
    }
}
