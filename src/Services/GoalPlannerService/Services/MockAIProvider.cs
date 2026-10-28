using Aspirra.GoalPlannerService.Interfaces;
using Aspirra.GoalPlannerService.Models;

namespace Aspirra.GoalPlannerService.Services
{
    public class MockAIProvider : IAIProvider
    {
        public Task<AiPlan> GenerateAsync(string prompt)
        {
            var goal = string.IsNullOrWhiteSpace(prompt) ? "your goal" : prompt.Trim();
            var today = DateOnly.FromDateTime(DateTime.UtcNow);

            var plan = new AiPlan
            {
                Milestones = new List<AiMilestone>
                {
                    new()
                    {
                        Title = $"Build foundations for {goal}",
                        Deadline = today.AddDays(7),
                        Tasks = new List<AiTask>
                        {
                            new()
                            {
                                Title = $"Research and outline {goal}",
                                DurationHours = 2,
                                Deadline = today.AddDays(3)
                            },
                            new()
                            {
                                Title = $"Complete first practice steps for {goal}",
                                DurationHours = 3,
                                Deadline = today.AddDays(6)
                            }
                        }
                    },
                    new()
                    {
                        Title = $"Advance and review {goal}",
                        Deadline = today.AddDays(14),
                        Tasks = new List<AiTask>
                        {
                            new()
                            {
                                Title = $"Apply {goal} in a practical exercise",
                                DurationHours = 3,
                                Deadline = today.AddDays(10)
                            },
                            new()
                            {
                                Title = $"Review progress and refine {goal}",
                                DurationHours = 4,
                                Deadline = today.AddDays(13)
                            }
                        }
                    }
                }
            };

            return Task.FromResult(plan);
        }
    }
}
