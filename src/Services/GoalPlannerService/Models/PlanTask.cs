namespace Aspirra.GoalPlannerService.Models
{
    public class PlanTask
    {
        public string TaskId { get; set; } = string.Empty;
        public string MilestoneId { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public int DurationHours { get; set; }
        public DateOnly Deadline { get; set; }
        public string? DependsOnTaskId { get; set; }
        public int OrderIndex { get; set; }
    }
}
