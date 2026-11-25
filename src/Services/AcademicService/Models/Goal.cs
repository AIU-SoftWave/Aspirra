using Aspirra.AcademicService.Exceptions;

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
            if (Status == GoalStatus.Archived)
            {
                throw new GoalValidationException("INVALID_TRANSITION", "Archived goals cannot be completed.");
            }

            Status = GoalStatus.Completed;
            CompletionDate ??= DateOnly.FromDateTime(DateTime.UtcNow);
        }

        public void Archive()
        {
            if (Status == GoalStatus.Archived)
            {
                return;
            }

            Status = GoalStatus.Archived;
        }

        public void Restore()
        {
            if (Status != GoalStatus.Archived)
            {
                throw new GoalValidationException("INVALID_TRANSITION", "Only archived goals can be restored.");
            }

            Status = GoalStatus.Active;
            CompletionDate = null;
        }
    }
}
