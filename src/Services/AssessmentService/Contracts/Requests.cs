using System.Text.Json.Serialization;

namespace Aspirra.AssessmentService.Contracts
{
    public sealed class CreateAssignmentRequest
    {
        public string? Title { get; set; }
        public string? CourseId { get; set; }
        public string? StudentId { get; set; }
        public string? Description { get; set; }
        public DateOnly? DueDate { get; set; }
        public string? Priority { get; set; }
    }

    public sealed class CreateExamRequest
    {
        public string? Title { get; set; }
        public string? CourseId { get; set; }
        public string? StudentId { get; set; }
        public DateOnly? ExamDate { get; set; }

        [JsonPropertyName("date")]
        public DateOnly? Date { get; set; }

        public TimeOnly? ExamTime { get; set; }

        [JsonPropertyName("time")]
        public TimeOnly? Time { get; set; }

        public string? Location { get; set; }
        public int? PreparationReminderDays { get; set; }
        public int? ReminderDays { get; set; }

        [JsonIgnore]
        public DateOnly? EffectiveExamDate => ExamDate ?? Date;

        [JsonIgnore]
        public TimeOnly? EffectiveExamTime => ExamTime ?? Time;

        [JsonIgnore]
        public int? EffectivePreparationReminderDays => PreparationReminderDays ?? ReminderDays;
    }

    public sealed class UpdateAssignmentStatusRequest
    {
        public string? Status { get; set; }
    }

    public sealed class SetExamReminderRequest
    {
        public int? DaysBefore { get; set; }
    }
}
