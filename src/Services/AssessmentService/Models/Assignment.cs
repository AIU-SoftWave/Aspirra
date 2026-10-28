namespace Aspirra.AssessmentService.Models
{
    public class Assignment
    {
        public string AssignmentId { get; set; } = string.Empty;
        public string CourseId { get; set; } = string.Empty;
        public string StudentId { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateOnly DueDate { get; set; }
        public string Priority { get; set; } = string.Empty;
        public AssignmentStatus Status { get; set; } = AssignmentStatus.NotStarted;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
