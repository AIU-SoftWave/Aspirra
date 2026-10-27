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
        public void Complete() => Status = GoalStatus.Completed;
        public void Archive() => Status = GoalStatus.Archived;
    }
}
