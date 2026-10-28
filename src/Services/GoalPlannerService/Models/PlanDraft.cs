namespace Aspirra.GoalPlannerService.Models
{
    public class PlanDraft
    {
        public string DraftId { get; set; } = string.Empty;
        public string StudentId { get; set; } = string.Empty;
        public string? GoalId { get; set; }
        public string GoalText { get; set; } = string.Empty;
        public PlanStatus Status { get; set; } = PlanStatus.Draft;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public List<Milestone> Milestones { get; set; } = new();

        public void Approve()
        {
            Status = PlanStatus.Approved;
        }
    }
}
