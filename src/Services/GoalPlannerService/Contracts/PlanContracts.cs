namespace Aspirra.GoalPlannerService.Contracts
{
    public sealed class GeneratePlanRequest
    {
        public string StudentId { get; set; } = string.Empty;
        public string? GoalId { get; set; }
        public string GoalText { get; set; } = string.Empty;
    }

    public sealed class PlanDraftInput
    {
        public string? GoalId { get; set; }
        public string? GoalText { get; set; }
        public List<MilestoneInput> Milestones { get; set; } = new();
    }

    public sealed class MilestoneInput
    {
        public string Title { get; set; } = string.Empty;
        public DateOnly Deadline { get; set; }
        public int OrderIndex { get; set; }
        public List<PlanTaskInput> Tasks { get; set; } = new();
    }

    public sealed class PlanTaskInput
    {
        public string Title { get; set; } = string.Empty;
        public int DurationHours { get; set; }
        public DateOnly Deadline { get; set; }
        public string? DependsOnTaskId { get; set; }
        public int OrderIndex { get; set; }
    }

    public sealed record ServiceError(int StatusCode, string Code, string Message);

    public sealed class ServiceResult<T>
    {
        private ServiceResult(T? value, ServiceError? error)
        {
            Value = value;
            Error = error;
        }

        public T? Value { get; }
        public ServiceError? Error { get; }
        public bool IsSuccess => Error is null;

        public static ServiceResult<T> Success(T value) => new(value, null);

        public static ServiceResult<T> Failure(int statusCode, string code, string message) =>
            new(default, new ServiceError(statusCode, code, message));
    }
}
