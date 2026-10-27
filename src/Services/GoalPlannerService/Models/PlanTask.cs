namespace Aspirra.GoalPlannerService.Models
{
    public class PlanTask
    {
        public string Title { get; set; } = string.Empty;
        public int DurationHours { get; set; }
        public DateOnly Deadline { get; set; }
        public string DependsOn { get; set; } = string.Empty;
    }
}
