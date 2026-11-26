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
| `skill-service.puml` | Skill, SkillType, SkillStatus, ISkill, ISkillRepository, SkillService, SkillsController | Arwa |
| `notification-service.puml` | Planned minimal Notification slice (reconcile with PM2-09) | Muhammed |

## Rules

- Use implemented names, not planned names.
- Show interface realizations and dependency direction.
- Include multiplicity and visibility where it clarifies the design.
- Render each `.puml` to `.png`; commit both.

## Message → Class-Link Table (PM2-01 / PM2-14)

Every message in PM2-02 through PM2-09 traverses an explicit link in the updated class diagrams:

| Scenario | Message | Sender → Receiver | Class-Diagram Link | Diagram(s) |
| --- | --- | --- | --- | --- |
| Complete Goal (PM2-02–05) | `1: POST /api/goals/{goalId}/complete` | `Student` → `GoalsController` | `Student --> GoalsController` | `00-pm2-scenario-class-diagram.puml` |
| Complete Goal (PM2-02–05) | `1.1: CompleteGoalAsync(goalId)` | `GoalsController` → `IGoal` (`GoalService`) | `GoalsController --> IGoal`, `IGoal <\|.. GoalService` | `00-pm2-scenario-class-diagram.puml`, `02-academic-service.puml` |
| Complete Goal (PM2-02–05) | `1.2: GetByIdAsync(goalId)` | `GoalService` → `IGoalRepository` | `GoalService --> IGoalRepository`, `IGoalRepository "1" -- "0..*" Goal` | `00-pm2-scenario-class-diagram.puml`, `02-academic-service.puml` |
| Complete Goal (PM2-02–05) | `1.3: Complete()` | `GoalService` → `Goal` | `GoalService --> Goal`, `Goal --> GoalStatus` | `00-pm2-scenario-class-diagram.puml`, `02-academic-service.puml` |
| Complete Goal (PM2-02–05) | `1.4: UpdateAsync(goal)` | `GoalService` → `IGoalRepository` | `GoalService --> IGoalRepository`, `GoalRepository ..\|> IGoalRepository` | `00-pm2-scenario-class-diagram.puml`, `02-academic-service.puml` |
| Generate Goal Plan (PM2-06) | `POST /api/plans/generate` / `approve` / `update` | `Student` → `PlansController` | `Student --> PlansController` | `00-pm2-scenario-class-diagram.puml` |
| Generate Goal Plan (PM2-06) | `GeneratePlanAsync(request)` / `ApproveAsync(draftId)` / `UpdateDraftAsync(draftId, input)` | `PlansController` → `IGoalPlanner` (`GoalPlannerService`) | `PlansController --> IGoalPlanner`, `IGoalPlanner <\|.. GoalPlannerService` | `00-pm2-scenario-class-diagram.puml`, `05-goal-planner-service.puml` |
| Generate Goal Plan (PM2-06) | `GoalExistsAsync(goalId)` | `GoalPlannerService` → `IGoalClient` | `GoalPlannerService --> IGoalClient`, `HttpGoalClient ..\|> IGoalClient` | `00-pm2-scenario-class-diagram.puml`, `05-goal-planner-service.puml` |
| Generate Goal Plan (PM2-06) | `GenerateAsync(prompt)` | `GoalPlannerService` → `IAIProvider` | `GoalPlannerService --> IAIProvider`, `IAIProvider ..> AiPlan` | `00-pm2-scenario-class-diagram.puml`, `05-goal-planner-service.puml` |
| Generate Goal Plan (PM2-06) | `Approve()` | `GoalPlannerService` → `PlanDraft` | `GoalPlannerService --> PlanDraft`, `PlanDraft --> PlanStatus` | `00-pm2-scenario-class-diagram.puml`, `05-goal-planner-service.puml` |
| Generate Goal Plan (PM2-06) | `AddAsync(draft)` / `GetByIdAsync(draftId)` / `SaveChangesAsync()` | `GoalPlannerService` → `IPlanDraftRepository` | `GoalPlannerService --> IPlanDraftRepository`, `IPlanDraftRepository "1" -- "0..*" PlanDraft` | `00-pm2-scenario-class-diagram.puml`, `05-goal-planner-service.puml` |
| Complete Habit + streak (PM2-07) | `POST /api/habits/{habitId}/complete` | `Student` → `HabitsController` | `Student --> HabitsController` | `00-pm2-scenario-class-diagram.puml` |
| Complete Habit + streak (PM2-07) | `CompleteHabitAsync(habitId, date)` | `HabitsController` → `IHabit` (`HabitService`) | `HabitsController --> IHabit`, `IHabit <\|.. HabitService` | `00-pm2-scenario-class-diagram.puml`, `13-habit-service.puml` |
| Complete Habit + streak (PM2-07) | `GetHabitAsync` / `GetLogAsync` / `AddLogAsync` / `GetLogsAsync` / `SaveChangesAsync` | `HabitService` → `IHabitRepository` | `HabitService --> IHabitRepository`, `IHabitRepository "1" -- "0..*" Habit`, `IHabitRepository "1" -- "0..*" HabitLog` | `00-pm2-scenario-class-diagram.puml`, `13-habit-service.puml` |
| Complete Habit + streak (PM2-07) | Update `CurrentStreak` / append `HabitLog` | `HabitService` → `Habit`, `HabitLog` | `HabitService --> Habit`, `HabitService --> HabitLog`, `Habit *-- "0..*" HabitLog` | `00-pm2-scenario-class-diagram.puml`, `13-habit-service.puml` |
| Add Skill (PM2-08) | `POST /api/skills` | `Student` → `SkillsController` | `Student --> SkillsController` | `00-pm2-scenario-class-diagram.puml` |
| Add Skill (PM2-08) | `AddSkillAsync(studentId, request)` | `SkillsController` → `ISkill` (`SkillService`) | `SkillsController --> ISkill`, `ISkill <\|.. SkillService` | `00-pm2-scenario-class-diagram.puml`, `skill-service.puml` |
| Add Skill (PM2-08) | `GetByNameAsync` / `Activate()` / `AddAsync` / `SaveChangesAsync` | `SkillService` → `ISkillRepository` / `Skill` | `SkillService --> ISkillRepository`, `SkillService --> Skill`, `ISkillRepository "1" -- "0..*" Skill` | `00-pm2-scenario-class-diagram.puml`, `skill-service.puml` |
| Send Deadline Reminders (PM2-09, planned slice) | `POST /api/notifications/deadline-reminders` | `Scheduler` → `NotificationsController` | `Scheduler --> NotificationsController` | `00-pm2-scenario-class-diagram.puml` |
| Send Deadline Reminders (PM2-09, planned slice) | `SendDeadlineRemindersAsync(studentId, days)` | `NotificationsController` → `INotification` (`NotificationService`) | `NotificationsController --> INotification`, `INotification <\|.. NotificationService` | `00-pm2-scenario-class-diagram.puml`, `notification-service.puml` |
| Send Deadline Reminders (PM2-09, planned slice) | `GetDeadlinesAsync(studentId, days)` | `NotificationService` → `IAssessment` | `NotificationService --> IAssessment`, `IAssessment ..> Deadline` | `00-pm2-scenario-class-diagram.puml`, `notification-service.puml` |
| Send Deadline Reminders (PM2-09, planned slice) | `SendAsync(to, subject, body)` | `NotificationService` → `IEmailGateway` | `NotificationService --> IEmailGateway` | `00-pm2-scenario-class-diagram.puml`, `notification-service.puml` |
| Send Deadline Reminders (PM2-09, planned slice) | `AddAsync` / `SaveChangesAsync` & status update | `NotificationService` → `INotificationRepository` / `Notification` | `NotificationService --> INotificationRepository`, `NotificationService --> Notification`, `Notification --> NotificationStatus` | `00-pm2-scenario-class-diagram.puml`, `notification-service.puml` |
