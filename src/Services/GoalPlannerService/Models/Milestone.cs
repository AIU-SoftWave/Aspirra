namespace Aspirra.GoalPlannerService.Models
{
    public class Milestone
    {
        public string Title { get; set; } = string.Empty;
        public DateOnly Deadline { get; set; }
        public List<PlanTask> Tasks { get; set; } = new();
    }
}
