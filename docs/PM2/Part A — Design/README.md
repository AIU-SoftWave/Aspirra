# Part A — Design

PM2 requires object, sequence, collaboration, and state diagrams for five scenarios, backed by updated class diagrams.

## Scenario matrix

| Scenario | Owner | Jira | GitHub | Object | Sequence | Collaboration | State |
| --- | --- | --- | --- | --- | --- | --- | --- |
| Complete Goal | Ahmed | `SCRUM-176`–`SCRUM-179` | `#55`–`#58` | `01-ahmed-complete-goal-object.puml` | `01-ahmed-complete-goal-sequence.puml` | `01-ahmed-complete-goal-collaboration.puml` | `01-goal-state.puml` |
| Generate Goal Plan | Yomna | `SCRUM-180` | `#59` | `generate-goal-plan-object.puml` | `generate-goal-plan-sequence.puml` | `generate-goal-plan-collaboration.puml` | `plan-draft-state.puml` |
| Complete Habit + streak | Mostafa | `SCRUM-181` | `#60` | `complete-habit-object.puml` | `complete-habit-sequence.puml` | `complete-habit-collaboration.puml` | `habit-state.puml` |
| Add Skill | Arwa | `SCRUM-182` | `#61` | `add-skill-object.puml` | `add-skill-sequence.puml` | `add-skill-collaboration.puml` | `skill-state.puml` |
| Send Deadline Reminders | Muhammed | `SCRUM-183` | `#62` | `deadline-reminders-object.puml` | `deadline-reminders-sequence.puml` | `deadline-reminders-collaboration.puml` | `notification-state.puml` |

Each diagram has a `.puml` source and a `.png` render in the matching directory.

## Class diagrams

The class diagrams are the structural backbone. Update them first so every message in the scenario diagrams has a real link.

Expected files in `Class Diagram/`:

- `00-pm2-scenario-class-diagram.puml` — actor → controller → interface → service → repository/client → domain.
- `02-academic-service.puml` — Goal, `GoalStatus`, `IGoalRepository`, `GoalService`, `GoalsController`, `IGoal`.
- `03-course-service.puml` — only if the Complete Goal scenario crosses the Course client boundary.
- `05-goal-planner-service.puml` — `PlanDraft`, `Milestone`, `PlanTask`, `IGoalPlanner`, `IAIProvider`, `IPlanDraftRepository`, `IGoalClient`.
- `13-habit-service.puml` — `Habit`, `HabitLog`, `IHabitRepository`, `HabitService`, `HabitsController`.
- `skill-service.puml` and `notification-service.puml` — new minimal slices for PM2-08 and PM2-09.

## Required scenario content

### Complete Goal

1. Student calls `POST /api/goals/{id}/complete`.
2. `GoalsController` validates the request and calls `IGoal.CompleteGoalAsync`.
3. `GoalService` loads the goal through `IGoalRepository`.
4. Valid `Active → Completed`; `Completed → Completed` is idempotent; `Archived → Completed` is rejected.
5. Repository `UpdateAsync` and `SaveChangesAsync` persist the result; the controller returns 200 or 400.

### Generate Goal Plan

1. Student calls `POST /api/plans/generate`.
2. `PlansController` calls `IGoalPlanner.GeneratePlanAsync`.
3. Service loads the goal through `IGoalClient`, calls `IAIProvider.GenerateAsync`, validates the draft, and saves through `IPlanDraftRepository`.
4. Student approves; `Draft → Approved` is valid; re-approval is idempotent; editing an approved plan is rejected.

### Complete Habit + streak

1. Student calls `POST /api/habits/{id}/complete`.
2. `HabitsController` calls `IHabit.CompleteHabitAsync`.
3. Service loads the habit through `IHabitRepository`, appends a `HabitLog`, and recalculates the streak.
4. Same-day duplicate completion is idempotent (no duplicate log); consecutive-day completion increments the streak; a gap resets it.

### Add Skill

1. Student calls `POST /api/skills`.
2. `SkillsController` calls `ISkill.AddSkillAsync`.
3. Service validates the name, checks duplicates through `ISkillRepository`, saves, and returns the created skill.
4. Duplicate skill name is rejected with 409/400; invalid input is rejected with 400.

### Send Deadline Reminders

1. Scheduler calls `POST /api/notifications/deadline-reminders`.
2. `NotificationsController` calls `INotification.SendDeadlineRemindersAsync`.
3. Service gets deadlines through `IAssessment`, builds notifications, sends through `IEmailGateway`, and saves through `INotificationRepository`.
4. Success: `Pending → Sent`; send failure: `Pending → Failed`; retry: `Failed → Sent`.

## Diagram quality bar

- Use the same participant names in object, sequence, and collaboration diagrams.
- Show return messages where the result affects control flow.
- Show invalid branches in state diagrams with guards and target tests.
- Do not invent classes or methods; verify against the code first.
- Every `.puml` renders to `.png` before review.

## Render

```bash
plantuml -tpng "docs/PM2/Part A — Design/Sequence Diagrams/01-ahmed-complete-goal-sequence.puml"
```

Review the PNG, not only the source. Fix layout, clipped labels, and missing arrows before committing.
