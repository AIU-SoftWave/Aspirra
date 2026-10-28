namespace Aspirra.GoalPlannerService.Models
{
    public class AiPlan
    {
        public List<AiMilestone> Milestones { get; set; } = new();
    }

    public class AiMilestone
    {
        public string Title { get; set; } = string.Empty;
        public DateOnly Deadline { get; set; }
        public List<AiTask> Tasks { get; set; } = new();
    }

    public class AiTask
    {
        public string Title { get; set; } = string.Empty;
        public int DurationHours { get; set; }
        public DateOnly Deadline { get; set; }
    }
}
