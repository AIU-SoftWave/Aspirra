# Part D — Traceability

The traceability matrix proves that every PM2 design artifact maps to code, tests, and evidence.

## Matrix columns

| Column | Meaning |
| --- | --- |
| Requirement / scenario | FR, user story, or PM2 scenario |
| Design artifact | `.puml` and `.png` paths |
| Code | Controller, service, interface, repository, domain files |
| Test | Test project and test method names |
| Evidence | CI, coverage, Sonar, Jenkins, or Jira link |
| Task | Jira key and GitHub issue |

## Initial matrix

Fill the test and evidence columns only when they exist.

| Scenario | Design | Code | Test | Evidence | Task |
| --- | --- | --- | --- | --- | --- |
| Complete Goal | `Part A — Design/Object Diagrams/01-ahmed-complete-goal-object.puml`, `Sequence Diagrams/01-ahmed-complete-goal-sequence.puml`, `Collaboration Diagrams/01-ahmed-complete-goal-collaboration.puml`, `State Diagrams/01-goal-state.puml` | `src/Services/AcademicService/Models/Goal.cs`, `Services/GoalService.cs`, `Controllers/GoalsController.cs`, `Interfaces/IGoalRepository.cs` | `tests/AcademicService.Tests/GoalStateTransitionTests.cs`, `AcademicApiTests.cs`, `GoalMoqTests.cs` | _pending_ | `SCRUM-175`–`SCRUM-179`, `#54`–`#58` |
| Generate Goal Plan | `Part A — Design/Object Diagrams/generate-goal-plan-object.puml`, `Sequence Diagrams/generate-goal-plan-sequence.puml`, `Collaboration Diagrams/generate-goal-plan-collaboration.puml`, `State Diagrams/plan-draft-state.puml` | `src/Services/GoalPlannerService/Controllers/PlansController.cs`, `Services/GoalPlannerService.cs`, `Interfaces/IGoalPlanner.cs` | `tests/GoalPlannerService.Tests/PlansControllerTests.cs`, `GoalPlannerMoqTests.cs` | _pending_ | `SCRUM-180`, `#59` |
| Complete Habit + streak | `Part A — Design/Object Diagrams/complete-habit-object.puml`, `Sequence Diagrams/complete-habit-sequence.puml`, `Collaboration Diagrams/complete-habit-collaboration.puml`, `State Diagrams/habit-state.puml` | `src/Services/HabitService/Controllers/HabitsController.cs`, `Services/HabitService.cs`, `Interfaces/IHabitRepository.cs`, `Models/Habit.cs`, `Models/HabitLog.cs` | `tests/HabitService.Tests/HabitServiceTests.cs`, `HabitMoqTests.cs` | _pending_ | `SCRUM-181`, `#60` |
| Add Skill | `Part A — Design/Object Diagrams/add-skill-object.puml`, `Sequence Diagrams/add-skill-sequence.puml`, `Collaboration Diagrams/add-skill-collaboration.puml`, `State Diagrams/skill-state.puml` | `src/Services/SkillService/Controllers/SkillsController.cs`, `Services/SkillService.cs`, `Interfaces/ISkillRepository.cs`, `Models/Skill.cs` | `tests/SkillService.Tests/AddSkillMoqTests.cs` | _pending_ | `SCRUM-182`, `#61` |
| Send Deadline Reminders | `Part A — Design/Object Diagrams/deadline-reminders-object.puml`, `Sequence Diagrams/deadline-reminders-sequence.puml`, `Collaboration Diagrams/deadline-reminders-collaboration.puml`, `State Diagrams/notification-state.puml` | `src/Services/NotificationService/Controllers/NotificationsController.cs`, `Services/NotificationService.cs`, `Interfaces/IAssessment.cs`, `Interfaces/IEmailGateway.cs`, `Interfaces/INotificationRepository.cs`, `Models/Notification.cs` | `tests/NotificationService.Tests/DeadlineRemindersMoqTests.cs` | _pending_ | `SCRUM-183`, `#62` |
| Moq + coverage | `Part B — DevOps/README.md` | `tests/*/*.csproj`, `coverlet.runsettings` | Academic/GoalPlanner/Habit Moq tests + existing suites (101 passing locally) | _pending_ | `SCRUM-184`, `#63` |
| SonarQube | `Part B — DevOps/README.md` | `sonar-project.properties`, `.github/workflows/ci.yml` | N/A | _pending_ | `SCRUM-185`, `#64` |
| Jenkins | `Part B — DevOps/README.md` | `Jenkinsfile`, `devops/jenkins/` | N/A | _pending_ | `SCRUM-186`, `#65` |
| Sprint management | `Part C — Project Management/README.md` | N/A | N/A | _pending_ | `SCRUM-187`, `#66` |
| Report | `report.md`, `report.pdf` | N/A | Final test output | _pending_ | `SCRUM-189`, `#68` |

## Rules

- Every PM2 task appears at least once.
- Every diagram path exists.
- Every code path exists.
- Every test name exists.
- Every evidence link resolves.
- If something is deferred, say so in the row; do not leave a fake link.
