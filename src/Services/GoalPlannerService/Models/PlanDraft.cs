namespace Aspirra.GoalPlannerService.Models
{
    public class PlanDraft
    {
        public string DraftId { get; set; } = string.Empty;
        public string GoalId { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public List<Milestone> Milestones { get; set; } = new();
        public void Approve() { }
    }
}
