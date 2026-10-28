namespace Aspirra.AcademicService.Models
{
    public class Goal
    {
        public string GoalId { get; set; } = string.Empty;
        public string StudentId { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateOnly TargetDate { get; set; }
        public string Priority { get; set; } = string.Empty;
        public GoalStatus Status { get; set; } = GoalStatus.Active;
        public DateOnly? CompletionDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public void Complete()
        {
            Status = GoalStatus.Completed;
            CompletionDate ??= DateOnly.FromDateTime(DateTime.UtcNow);
        }

        public void Archive() => Status = GoalStatus.Archived;
        public void Restore() => Status = GoalStatus.Active;
    }
}
