# Collaboration Diagrams

One collaboration (communication) diagram per PM2 scenario. Show the same objects and messages as the sequence diagram, but in a structural layout with numbered messages.

## Required files

- `01-ahmed-complete-goal-collaboration.puml` — Ahmed — `#57` — done
- `generate-goal-plan-collaboration.puml` — Yomna — `#59`
- `complete-habit-collaboration.puml` — Mostafa — `#60`
- `add-skill-collaboration.puml` — Arwa — `#61`
- `deadline-reminders-collaboration.puml` — Muhammed — `#62` — done

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

## Message → verify → test (Send Deadline Reminders)

| Message | Moq `Verify` | Test |
| --- | --- | --- |
| `1.2: GetDeadlinesAsync("student-1001", 1)` | `Times.Once` | `SendReminders_WhenEmailSucceeds_SavesSentNotification` |
| `1.3 [not completed]: AddAsync(reminder)` | `Times.Once` | `SendReminders_WhenEmailSucceeds_SavesSentNotification` |
| `1.4: SendAsync("student1001@aiu.edu.eg", subject, body)` | `Times.Once` | `SendReminders_WhenEmailSucceeds_SavesSentNotification` |
| `1.5 [email sent]: MarkSent(now)` | status `Sent`, `Attempts = 1` | `SendReminders_WhenEmailSucceeds_SavesSentNotification` |
| `1.6: SaveChangesAsync()` | `Times.Once` | `SendReminders_WhenEmailSucceeds_SavesSentNotification` |
| Email failed → `MarkFailed(now)` | `SaveChangesAsync` `Times.Once`, status `Failed` | `SendReminders_WhenEmailFails_SavesFailedNotification` |
| Completed deadline → no reminder | `AddAsync` / `SendAsync` `Times.Never` | `SendReminders_WithCompletedDeadline_SendsNothing` |
