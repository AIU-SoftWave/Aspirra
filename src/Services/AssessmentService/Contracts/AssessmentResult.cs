using Aspirra.AssessmentService.Models;

namespace Aspirra.AssessmentService.Contracts
{
    public static class AssessmentErrorCodes
    {
        public const string RequiredField = "REQUIRED_FIELD";
        public const string InvalidDate = "INVALID_DATE";
        public const string InvalidStatus = "INVALID_STATUS";
        public const string InvalidReminder = "INVALID_REMINDER";
        public const string NotFound = "NOT_FOUND";
    }

    public sealed record AssessmentError(string Code, string Message);

    public sealed class AssessmentResult<T>
    {
        private AssessmentResult(T? value, AssessmentError? error)
        {
            Value = value;
            Error = error;
        }

        public T? Value { get; }
        public AssessmentError? Error { get; }
        public bool IsSuccess => Error is null;

        public static AssessmentResult<T> Success(T value) => new(value, null);
        public static AssessmentResult<T> Failure(string code, string message) => new(default, new AssessmentError(code, message));
    }

    public sealed record AssessmentSummary(IReadOnlyList<Assignment> Assignments, IReadOnlyList<Exam> Exams);

    public sealed record CourseAssignmentItem(string AssignmentId, string Title, AssignmentStatus Status);
}
