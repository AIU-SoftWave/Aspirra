namespace Aspirra.GoalPlannerService.Models
{
    public class Milestone
    {
        public string MilestoneId { get; set; } = string.Empty;
        public string DraftId { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public DateOnly Deadline { get; set; }
        public int OrderIndex { get; set; }
        public List<PlanTask> Tasks { get; set; } = new();
    }
}
