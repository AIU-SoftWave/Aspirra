# Class Diagrams

Updated PM2 class diagrams. Every message in the object, sequence, and collaboration diagrams must map to a link here.

## Files

| File | Scope | Owner |
| --- | --- | --- |
| `00-pm2-scenario-class-diagram.puml` | Cross-service scenario view | Ahmed |
| `02-academic-service.puml` | Goal, GoalStatus, IGoalRepository, GoalService, GoalsController | Ahmed |
| `03-course-service.puml` | Course boundary | Ahmed |
| `05-goal-planner-service.puml` | PlanDraft, Milestone, PlanTask, IGoalPlanner, IAIProvider, IPlanDraftRepository, IGoalClient | Yomna |
| `13-habit-service.puml` | Habit, HabitLog, IHabitRepository, HabitService, HabitsController | Mostafa |
| `skill-service.puml` | Planned minimal Skill slice (reconcile with PM2-08) | Arwa |
| `notification-service.puml` | Planned minimal Notification slice (reconcile with PM2-09) | Muhammed |

## Rules

- Use implemented names, not planned names.
- Show interface realizations and dependency direction.
- Include multiplicity and visibility where it clarifies the design.
- Render each `.puml` to `.png`; commit both.
