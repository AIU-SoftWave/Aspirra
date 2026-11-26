# Part D — Design-to-Code Mapping

This file maps every PM2 design artifact to the code and test that implement it. Each row is verifiable by path.

## Updated Class Diagrams & Message-to-Link Backbone (PM2-01)

The full scenario message → class-diagram link verification table is maintained in [`Part A — Design/Class Diagram/README.md`](../Part%20A%20%E2%80%94%20Design/Class%20Diagram/README.md#message--class-link-table-pm2-01--pm2-14).

| Class diagram | Implemented code verified against | Status |
| --- | --- | --- |
| `Part A — Design/Class Diagram/00-pm2-scenario-class-diagram.puml` | Cross-service view across all 5 PM2 scenarios (`Student` & `Scheduler` actors → controllers → interfaces → services → repositories/clients → domain) | Updated & rendered |
| `Part A — Design/Class Diagram/02-academic-service.puml` | `src/Services/AcademicService/` (`GoalsController`, `IGoal`, `GoalService`, `IGoalRepository`, `GoalRepository`, `AcademicDbContext`, `Goal`, `GoalStatus`) | Updated & rendered |
| `Part A — Design/Class Diagram/03-course-service.puml` | `src/Services/CourseService/` (`CoursesController`, `ICourse`, `CourseService`, `IAssessmentClient`, `HttpAssessmentClient`, `ICourseRepository`, `CourseRepository`, `CourseDbContext`, `Course`, `CourseEnrollment`, `CourseProgress`, `AssessmentItem`) | Updated & rendered |
| `Part A — Design/Class Diagram/05-goal-planner-service.puml` | `src/Services/GoalPlannerService/` (`PlansController`, `IGoalPlanner`, `GoalPlannerService`, `IGoalClient`, `HttpGoalClient`, `IAIProvider`, `MockAIProvider`, `IPlanDraftRepository`, `PlanDraftRepository`, `GoalPlannerDbContext`, `PlanDraft`, `Milestone`, `PlanTask`, `AiPlan`, `PlanStatus`) | Updated & rendered |
| `Part A — Design/Class Diagram/13-habit-service.puml` | `src/Services/HabitService/` (`HabitsController`, `IHabit`, `HabitService`, `IHabitRepository`, `HabitRepository`, `HabitDbContext`, `Habit`, `HabitLog`, `HabitFrequency`) | Updated & rendered |
| `Part A — Design/Class Diagram/skill-service.puml` | `src/Services/SkillService/` (Dockerfile only — planned minimal slice, reconcile with PM2-08) | Planned baseline updated & rendered |
| `Part A — Design/Class Diagram/notification-service.puml` | `src/Services/NotificationService/` (`NotificationsController`, `INotification`, `NotificationService`, `IAssessment`, `HttpAssessmentClient`, `IEmailGateway`, `LoggingEmailGateway`, `INotificationRepository`, `NotificationRepository`, `NotificationDbContext`, `Notification`, `NotificationStatus`, `Deadline`) | Reconciled with PM2-09 & rendered |

## Complete Goal (PM2-01–PM2-05)

| Design artifact | Code | Test |
| --- | --- | --- |
| `Part A — Design/Class Diagram/02-academic-service.puml` | `src/Services/AcademicService/Models/Goal.cs`, `Services/GoalService.cs`, `Controllers/GoalsController.cs`, `Repositories/GoalRepository.cs`, `Interfaces/IGoalRepository.cs` | `tests/AcademicService.Tests/GoalStateTransitionTests.cs` |
| `Part A — Design/State Diagrams/01-goal-state.puml` | `Goal.Complete()`, `Goal.Archive()`, `Goal.Restore()` in `Models/Goal.cs` | `GoalStateTransitionTests.cs`, `GoalMoqTests.cs` |
| `Part A — Design/Object Diagrams/01-ahmed-complete-goal-object.puml` | `Goal` entity state (`Status = Active`, `CompletionDate = null`) | `CompleteGoal_FromActive_ReturnsCompleted` |
| `Part A — Design/Sequence Diagrams/01-ahmed-complete-goal-sequence.puml` | `GoalsController.CompleteGoal` → `GoalService.CompleteGoalAsync` → `IGoalRepository.UpdateAsync` | `GoalMoqTests.CompleteGoal_FromActive_PersistsExactlyOnce` |
| `Part A — Design/Collaboration Diagrams/01-ahmed-complete-goal-collaboration.puml` | `GoalsController` → `GoalService` → `GoalRepository` → `AcademicDbContext` | `GoalStateTransitionTests`, `GoalMoqTests` |

Transition coverage (state diagram ↔ code ↔ test):

| Transition | Code | Test |
| --- | --- | --- |
| Active → Completed | `Goal.Complete()` sets `Completed`, `CompletionDate ??= today` | `CompleteGoal_FromActive_ReturnsCompleted` |
| Completed → Completed (idempotent) | `Goal.Complete()` keeps `CompletionDate` | `CompleteGoal_FromCompleted_IsIdempotent` |
| Archived → Completed (rejected) | `Goal.Complete()` throws `INVALID_TRANSITION` | `CompleteGoal_FromArchived_ThrowsInvalidTransitionAndDoesNotUpdate` |
| Active → Archived | `Goal.Archive()` sets `Archived` | `ArchiveGoal_FromActive_ReturnsArchived` |
| Completed → Archived | `Goal.Archive()` sets `Archived`, keeps date | `ArchiveGoal_FromCompleted_ReturnsArchived` |
| Archived → Active | `Goal.Restore()` sets `Active`, clears `CompletionDate` | `RestoreGoal_FromArchived_ReturnsActive` |
| Active → Restore (rejected) | `Goal.Restore()` throws `INVALID_TRANSITION` | `RestoreGoal_FromActive_ThrowsInvalidTransitionAndDoesNotUpdate` |
| Completed → Restore (rejected) | `Goal.Restore()` throws `INVALID_TRANSITION` | `RestoreGoal_FromCompleted_ThrowsInvalidTransitionAndDoesNotUpdate` |

## Generate Goal Plan (PM2-06, owner: Yomna)

| Design artifact | Code | Test |
| --- | --- | --- |
| `Part A — Design/Class Diagram/05-goal-planner-service.puml` | `src/Services/GoalPlannerService/Controllers/PlansController.cs`, `Services/GoalPlannerService.cs`, `Interfaces/IGoalPlanner.cs`, `Interfaces/IPlanDraftRepository.cs`, `Interfaces/IAIProvider.cs`, `Interfaces/IGoalClient.cs` | `tests/GoalPlannerService.Tests/PlansControllerTests.cs`, `GoalPlannerMoqTests.cs` |
| Object / sequence / collaboration / state diagrams (owner: Yomna) | `PlanDraft.Approve()`, `GoalPlannerService.ApproveAsync` | `GoalPlannerMoqTests.Approve_*` |

Moq approve-flow coverage already present:

| Interaction | Test |
| --- | --- |
| Approve with existing goal persists once | `Approve_WithExistingGoal_ApprovesAndSavesOnce` |
| Approve with missing goal returns 404, no save | `Approve_WhenGoalDoesNotExist_ReturnsGoalNotFoundAndDoesNotSave` |
| Approve with missing draft returns 404 | `Approve_WhenDraftMissing_ReturnsNotFound` |

## Complete Habit + streak (PM2-07, owner: Mostafa)

| Design artifact | Code | Test |
| --- | --- | --- |
| `Part A — Design/Class Diagram/13-habit-service.puml` | `src/Services/HabitService/Controllers/HabitsController.cs`, `Services/HabitService.cs`, `Interfaces/IHabitRepository.cs`, `Models/Habit.cs`, `Models/HabitLog.cs` | `tests/HabitService.Tests/HabitServiceTests.cs`, `HabitMoqTests.cs` |
| Object / sequence / collaboration / state diagrams (owner: Mostafa) | `HabitService.CompleteHabitAsync`, `CalculateStreak` | `HabitMoqTests.CompleteHabit_*` |

Moq complete-flow coverage already present:

| Interaction | Test |
| --- | --- |
| Complete with no log adds a log and saves twice | `CompleteHabit_WhenNoLogExists_AddsLogAndSaves` |
| Complete with completed log does not re-add | `CompleteHabit_WhenLogAlreadyCompleted_DoesNotAddLog` |
| Complete missing habit returns NOT_FOUND, no persist | `CompleteHabit_WhenHabitMissing_ReturnsNotFoundAndDoesNotPersist` |

## Add Skill (PM2-08, owner: Arwa) — blocked on implementation

| Design artifact | Code | Test |
| --- | --- | --- |
| `Part A — Design/Class Diagram/skill-service.puml` (planned baseline) | `src/Services/SkillService/` (Dockerfile only) | `tests/SkillService.Tests/` (to be created) |

Reconcile the planned class diagram with the implemented slice once PM2-08 code exists.

## Send Deadline Reminders (PM2-09, owner: Muhammed)

| Design artifact | Code | Test |
| --- | --- | --- |
| `Part A — Design/Class Diagram/notification-service.puml` | `src/Services/NotificationService/Models/Notification.cs`, `Models/Deadline.cs`, `Services/NotificationService.cs`, `Controllers/NotificationsController.cs`, `Clients/HttpAssessmentClient.cs`, `Gateways/LoggingEmailGateway.cs`, `Repositories/NotificationRepository.cs` | `tests/NotificationService.Tests/DeadlineRemindersMoqTests.cs`, `HttpAssessmentClientTests.cs`, `NotificationApiTests.cs` |
| `Part A — Design/State Diagrams/notification-state.puml` | `Notification.MarkSent()`, `Notification.MarkFailed()`, `Notification.CanRetry` | `NotificationStateTransitionTests.cs` |
| `Part A — Design/Object Diagrams/deadline-reminders-object.puml` | `Deadline` (`asg-3001`, "Database Project") and the created `Notification` | `NotificationFixtures` (test fixtures), `SendReminders_WhenEmailSucceeds_SavesSentNotification` |
| `Part A — Design/Sequence Diagrams/deadline-reminders-sequence.puml` | `NotificationsController.SendDeadlineReminders` → `NotificationService.SendDeadlineRemindersAsync` → `IAssessment.GetDeadlinesAsync` → `INotificationRepository.AddAsync` → `IEmailGateway.SendAsync` → `Notification.MarkSent/MarkFailed` → `SaveChangesAsync` | `DeadlineRemindersMoqTests` |
| `Part A — Design/Collaboration Diagrams/deadline-reminders-collaboration.puml` | `NotificationsController` → `NotificationService` → `HttpAssessmentClient` / `LoggingEmailGateway` / `NotificationRepository` | `DeadlineRemindersMoqTests` (Moq `Verify`) |

Message → Moq `Verify` → test:

| Message | Moq `Verify` | Test |
| --- | --- | --- |
| `1.2: GetDeadlinesAsync("student-1001", 1)` | `Times.Once` | `SendReminders_WhenEmailSucceeds_SavesSentNotification` |
| `1.3: AddAsync(notification)` | `Times.Once` | `SendReminders_WhenEmailSucceeds_SavesSentNotification` |
| `1.4: SendAsync(email, subject, body)` | `Times.Once` | `SendReminders_WhenEmailSucceeds_SavesSentNotification`, `SendReminders_WhenEmailFails_SavesFailedNotification` |
| `1.6: SaveChangesAsync()` | `Times.Once` | `SendReminders_WhenEmailSucceeds_SavesSentNotification` |
| Completed deadline → no reminder | `AddAsync` / `SendAsync` / `SaveChangesAsync` `Times.Never` | `SendReminders_WithCompletedDeadline_SendsNothing` |
| Invalid input → 400 | `GetDeadlinesAsync` `Times.Never` | `SendReminders_WithInvalidInput_IsRejectedBeforeAnyCall` |
| Retry of failed reminder | `SendAsync` / `SaveChangesAsync` `Times.Once` | `Retry_FromFailed_SendsAndMarksSent` |
| Retry of sent / pending → 400 | `SendAsync` / `SaveChangesAsync` `Times.Never` | `Retry_WhenNotFailed_IsRejectedWithoutSending` |

Transition coverage:

| Transition | Code | Test |
| --- | --- | --- |
| [*] → Pending | `new Notification()` (`Status = Pending`) | `NewNotification_StartsPending` |
| Pending → Sent | `Notification.MarkSent()` | `MarkSent_FromPending_BecomesSent` |
| Pending → Failed | `Notification.MarkFailed()` | `MarkFailed_FromPending_BecomesFailed` |
| Failed → Sent (retry) | `NotificationService.RetryAsync` + `MarkSent()` | `MarkSent_FromFailed_RetryBecomesSent`, `Retry_FromFailed_SendsAndMarksSent` |
| Failed → Failed (retry fails) | `MarkFailed()`, `SEND_FAILED` (502) | `MarkFailed_FromFailed_RetryFailsAgainAndStaysFailed`, `Retry_WhenEmailFailsAgain_StaysFailedAndReturnsSendFailed` |
| Sent → retry (rejected) | `CanRetry == false`, `INVALID_TRANSITION` (400) | `Retry_WhenNotFailed_IsRejectedWithoutSending(Sent)`, `MarkSent_FromSent_ThrowsInvalidTransition`, `MarkFailed_FromSent_ThrowsInvalidTransition` |
| Pending → retry (rejected) | `CanRetry == false`, `INVALID_TRANSITION` (400) | `Retry_WhenNotFailed_IsRejectedWithoutSending(Pending)`, `CanRetry_WhenNotFailed_IsFalse` |

## DevOps (PM2-10–PM2-12)

| Design artifact | Code | Test / evidence |
| --- | --- | --- |
| `Part B — DevOps/README.md` | `coverlet.runsettings`, `tests/*/*.csproj` (Moq, coverlet, JUnit logger) | 101 passing locally; coverage 37.1% |
| `Part B — DevOps/README.md` | `sonar-project.properties`, `.github/workflows/ci.yml` (sonar job) | blocked on `SONAR_TOKEN` |
| `Part B — DevOps/README.md` | `Jenkinsfile`, `devops/jenkins/Dockerfile`, `devops/jenkins/docker-compose.yml` | scaffolded; run pending |
