namespace Aspirra.AssessmentService.Models
{
    public class Exam
    {
        public string ExamId { get; set; } = string.Empty;
        public string CourseId { get; set; } = string.Empty;
        public string StudentId { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public DateOnly ExamDate { get; set; }
        public TimeOnly ExamTime { get; set; }
        public string Location { get; set; } = string.Empty;
        public int? PreparationReminderDays { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
