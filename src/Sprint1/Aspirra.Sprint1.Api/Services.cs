namespace Aspirra.Sprint1.Api;

public static class ProfileValidator
{
    public static string? Validate(ProfileRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.AcademicLevel) || string.IsNullOrWhiteSpace(request.Major))
            return "Required field";
        if (request.Availability?.Any(slot => slot.EndTime < slot.StartTime) == true)
            return "Invalid time slot";
        return null;
    }
}

public static class GoalValidator
{
    public static string? Validate(GoalRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Title)) return "Required field";
        if (request.TargetDate <= DateOnly.FromDateTime(DateTime.UtcNow)) return "Target date must be in the future";
        if (string.IsNullOrWhiteSpace(request.Priority)) return "Required field";
        return null;
    }
}

public sealed class StreakCalculator
{
    public int Calculate(IReadOnlyCollection<DateOnly> completions, string frequency)
    {
        if (completions.Count == 0) return 0;
        var dates = completions.OrderDescending().ToList();
        var streak = 1;
        for (var index = 1; index < dates.Count; index++)
        {
            var expected = frequency.Equals("daily", StringComparison.OrdinalIgnoreCase)
                ? dates[index - 1].AddDays(-1)
                : dates[index - 1].AddDays(-7);
            if (dates[index] != expected) break;
            streak++;
        }
        return streak;
    }
}

public interface IAIProvider
{
    Task<IReadOnlyList<Milestone>> Generate(string goalText, CancellationToken cancellationToken);
}

public sealed class ConfiguredAIProvider(IConfiguration configuration) : IAIProvider
{
    public Task<IReadOnlyList<Milestone>> Generate(string goalText, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(configuration["AI_PROVIDER_API_KEY"]))
            throw new InvalidOperationException("AI provider is unavailable");

        var deadline = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(30));
        IReadOnlyList<Milestone> result =
        [
            new Milestone(Guid.NewGuid(), "First milestone", deadline,
                [new PlanTask(Guid.NewGuid(), goalText, 1, deadline)])
        ];
        return Task.FromResult(result);
    }
}

public sealed class GoalPlannerService(Sprint1Store store, IAIProvider aiProvider)
{
    public async Task<PlanDraft> GenerateDraft(string studentId, string goalText, CancellationToken cancellationToken = default)
    {
        var milestones = await aiProvider.Generate(goalText, cancellationToken);
        var draft = new PlanDraft(Guid.NewGuid(), studentId, goalText, "Draft", milestones.ToList());
        store.Drafts[draft.Id] = draft;
        return draft;
    }

    public PlanDraft? UpdateDraft(Guid id, DraftEditRequest request)
    {
        if (!store.Drafts.TryGetValue(id, out var draft)) return null;
        var updated = draft with { Milestones = request.Milestones ?? draft.Milestones };
        store.Drafts[id] = updated;
        return updated;
    }

    public Plan? Approve(Guid id)
    {
        if (!store.Drafts.TryGetValue(id, out var draft)) return null;
        var plan = new Plan(Guid.NewGuid(), draft.StudentId, draft.GoalText, draft.Milestones);
        store.Drafts.Remove(id);
        return plan;
    }

    public bool Discard(Guid id) => store.Drafts.Remove(id);
}
