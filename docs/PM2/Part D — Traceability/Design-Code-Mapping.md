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
| `Part A — Design/Class Diagram/notification-service.puml` | `src/Services/NotificationService/` (Dockerfile only — planned minimal slice, reconcile with PM2-09) | Planned baseline updated & rendered |

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
| `Part A — Design/Class Diagram/13-habit-service.puml` | `src/Services/HabitService/Controllers/HabitsController.cs`, `Services/HabitService.cs`, `Interfaces/IHabitRepository.cs`, `Models/Habit.cs`, `Models/HabitLog.cs` | `tests/HabitService.Tests/HabitServiceTests.cs`, `CompleteHabitMoqTests.cs` |
| Object / sequence / collaboration / state diagrams (owner: Mostafa) | `HabitService.CompleteHabitAsync`, `CalculateStreak` | `CompleteHabitMoqTests.CompleteHabit_*`, `CompleteHabitMoqTests.GetStreaks_*` |

Moq complete-flow coverage already present:

| Interaction | Test |
| --- | --- |
| First completion creates one log and verifies repository persistence | `CompleteHabit_FirstCompletion_AddsLogAndRecalculatesStreak` |
| Same-day duplicate and incomplete-log reuse | `CompleteHabit_SameDayDuplicate_DoesNotAddAnotherLogOrIncrementStreak`, `CompleteHabit_IncompleteLog_UpdatesExistingLogWithoutAddingAnother` |
| Consecutive daily and weekly schedules; missed and in-progress periods | `CompleteHabit_ConsecutiveDailyCompletions_CountsEachScheduledDay`, `GetStreaks_ConsecutiveWeeklySchedules_CountsScheduledDays`, `GetStreaks_MissedDailySchedule_ReturnsZero`, `GetStreaks_MissedWeeklySchedule_ReturnsZeroAfterPeriodEnds`, `GetStreaks_DailyScheduleStillInProgress_PreservesPreviousStreak`, `GetStreaks_WeeklyScheduleStillInProgress_DoesNotResetEarly` |
| Missing habit has no persistence; streak query uses the student scope | `CompleteHabit_MissingHabit_ReturnsNotFoundWithoutRepositoryWrites`, `GetStreaks_UsesStudentScopedHabitQuery` |

## Add Skill (PM2-08, owner: Arwa) — blocked on implementation

| Design artifact | Code | Test |
| --- | --- | --- |
| `Part A — Design/Class Diagram/skill-service.puml` (planned baseline) | `src/Services/SkillService/` (Dockerfile only) | `tests/SkillService.Tests/` (to be created) |

Reconcile the planned class diagram with the implemented slice once PM2-08 code exists.

## Send Deadline Reminders (PM2-09, owner: Muhammed) — blocked on implementation

| Design artifact | Code | Test |
| --- | --- | --- |
| `Part A — Design/Class Diagram/notification-service.puml` (planned baseline) | `src/Services/NotificationService/` (Dockerfile only) | `tests/NotificationService.Tests/` (to be created) |

Reconcile the planned class diagram with the implemented slice once PM2-09 code exists.

## DevOps (PM2-10–PM2-12)

| Design artifact | Code | Test / evidence |
| --- | --- | --- |
| `Part B — DevOps/README.md` | `coverlet.runsettings`, `tests/*/*.csproj` (Moq, coverlet, JUnit logger) | 101 passing locally; coverage 37.1% |
| `Part B — DevOps/README.md` | `sonar-project.properties`, `.github/workflows/ci.yml` (sonar job) | blocked on `SONAR_TOKEN` |
| `Part B — DevOps/README.md` | `Jenkinsfile`, `devops/jenkins/Dockerfile`, `devops/jenkins/docker-compose.yml` | scaffolded; run pending |
