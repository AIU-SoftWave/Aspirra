# Collaboration Diagrams

One collaboration (communication) diagram per PM2 scenario. Show the same objects and messages as the sequence diagram, but in a structural layout with numbered messages.

## Required files

- `01-ahmed-complete-goal-collaboration.puml` — Ahmed — `#57` — done
- `generate-goal-plan-collaboration.puml` — Yomna — `#59`
- `complete-habit-collaboration.puml` — Mostafa — `#60`
- `add-skill-collaboration.puml` — Arwa — `#61`
- `deadline-reminders-collaboration.puml` — Muhammed — `#62`

## Rules

- Number messages in the same order as the sequence diagram.
- Label each link with the message and interface used.
- Do not add objects that are absent from the sequence diagram.
- Render and review the PNG before committing.

## Message → verify → test (Complete Goal)

| Message | Moq `Verify` | Test |
| --- | --- | --- |
| `GetByIdAsync("goal-1001")` | `GetByIdAsync` setup returns the goal | `CompleteGoal_FromActive_ReturnsCompleted` |
| `Complete()` | `UpdateAsync(goal, ...)` called exactly once | `CompleteGoal_FromActive_PersistsExactlyOnce` |
| `UpdateAsync(goal)` | `Times.Once` | `CompleteGoal_FromActive_PersistsExactlyOnce` |
| Archived → `Complete()` | `UpdateAsync` `Times.Never` | `CompleteGoal_FromArchived_DoesNotPersist` |
| Missing goal → 404 | `UpdateAsync` `Times.Never` | `CompleteGoal_MissingGoal_ThrowsNotFound` |
