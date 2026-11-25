# Aspirra — Student Success & Career Planner

## Project Milestone 2 (PM2) — Software Design & DevOps

**Course:** CSE454 — Advanced Software Engineering  
**Institution:** Alamein International University  
**Faculty:** Computer Science & Engineering  
**Repository:** https://github.com/AIU-SoftWave/Aspirra  
**Jira project:** https://aspirra.atlassian.net (project `SCRUM`, board `1`)  
**Jira epic:** [SCRUM-174](https://aspirra.atlassian.net/browse/SCRUM-174)  
**GitHub parent:** [#53](https://github.com/AIU-SoftWave/Aspirra/issues/53)  
**Submission date:** 2026-11-28  
**Tag / commit:** `pm2-submission` / _pending_

> This is the report scaffold. Replace every `_pending_` and `TODO` only with verified evidence. Do not invent test counts, coverage, quality-gate results, or CI runs.

### Team

| Member | GitHub | PM2 role | Owned services |
| --- | --- | --- | --- |
| Ahmed Mohamed Ahmed (Team Leader) | `AbooSalh` | PM2 lead; Complete Goal scenario; class/state diagrams; tests; Sonar; Jenkins; report | Student, Academic, Course, Assessment |
| Yomna Mohamed Fekry | `YomnaElmenawy` | Generate Goal Plan design set and tests | Goal Planner, Study Planner, Scheduling, Adaptation |
| Arwa Mohey Hamdy | `ArwaMohey` | Add Skill design set, code slice, and tests | Career, Skill, Portfolio, Job Preparation |
| Mostafa Khamis Abozead | `Mostafa-abozead` | Complete Habit + streak design set and tests | Habit, Analytics, Prediction, AI Coach |
| Muhammed Hany Hamdy | `muhammedhanii` | Send Deadline Reminders design set, code slice, and tests | Dashboard, Notification, Study Group, Administration |

### Tools used

Jira · GitHub Issues and Milestone · GitHub Actions · Docker & Docker Compose · SonarQube Cloud · Jenkins · PlantUML · .NET 10 · xUnit · Moq · coverlet

---

## Contents

1. Executive Summary
2. Part A — Design
   - A.1 Scenario selection
   - A.2 Updated class diagrams
   - A.3 Object diagrams
   - A.4 Sequence diagrams
   - A.5 Collaboration diagrams
   - A.6 State diagrams and invalid transitions
   - A.7 Design quality
3. Part B — DevOps
   - B.1 Test strategy and Moq
   - B.2 Coverage
   - B.3 SonarQube Cloud
   - B.4 Jenkins
   - B.5 GitHub Actions vs Jenkins
   - B.6 Reproduction
4. Part C — Project Management
5. Part D — Traceability
6. Verification and Evidence
7. Team Contributions
8. Appendices

---

## 1. Executive Summary

PM2 extends Aspirra's Sprint 1 foundation with a complete design-and-DevOps increment. The milestone selects five end-to-end scenarios, documents each with object, sequence, collaboration, and state diagrams, updates the class diagrams to support them, adds Moq interaction tests with coverage, and verifies the pipeline through SonarQube Cloud and Jenkins.

The PM2 work is tracked as Jira epic `SCRUM-174` and 15 GitHub sub-issues (`#54`–`#68`) under milestone `#2`. Jira is authoritative for status and story points; GitHub mirrors the deliverables and acceptance criteria.

| PM2 area | Result |
| --- | --- |
| Part A — Design | 5 scenarios × 4 diagram types, plus updated class diagrams |
| Part B — DevOps | Moq tests, coverage, SonarQube quality gate, Jenkins pipeline, Actions comparison |
| Part C — Project Management | Sprint 1 actuals, Sprint 2 plan/execution, velocity, revised plan |
| Part D — Traceability | Requirement → diagram → code → test → evidence matrix |

**Scope:** 48 SP committed in Sprint 2.

**Honest status:** In progress — PM2-01 through PM2-05 are complete (issues #54–#58 closed, Jira SCRUM-175–179 Done; #54 class diagrams merged via PR #71, with Skill/Notification planned slices to be reconciled once PM2-08/09 code lands). PM2-10 tooling and Moq tests are in place (101 tests passing, 37.1% line coverage locally); Skill/Notification tests remain pending PM2-08/09 implementation. SonarQube (PM2-11) passed its quality gate on PR #69; Jenkins (PM2-12) controller image is built and awaiting a live pipeline run.

---

## 2. Part A — Design

### A.1 Scenario selection

The five scenarios were chosen because they exercise different architectural concerns:

| Scenario | Why it matters | Owner |
| --- | --- | --- |
| Complete Goal | Domain state machine and invalid-transition handling | Ahmed |
| Generate Goal Plan | AI provider, draft persistence, approve flow | Yomna |
| Complete Habit + streak | Time-based state and streak consistency | Mostafa |
| Add Skill | Simple CRUD with duplicate and validation rules | Arwa |
| Send Deadline Reminders | Cross-service read, email gateway, retry state | Muhammed |

Each scenario uses the same participants across its object, sequence, and collaboration diagrams. Its state diagram captures lifecycle rules; its tests cover valid and invalid branches.

### A.2 Updated class diagrams

The class diagrams were updated first so every message in the scenario diagrams maps to an existing link. The complete message → class-link mapping table is maintained in [`Part A — Design/Class Diagram/README.md`](Part%20A%20%E2%80%94%20Design/Class%20Diagram/README.md).

| Diagram | Scope | Status |
| --- | --- | --- |
| `Part A — Design/Class Diagram/00-pm2-scenario-class-diagram.puml` | Cross-service scenario view | Updated & verified; PNG rendered |
| `Part A — Design/Class Diagram/02-academic-service.puml` | Goal and completion flow | Updated & verified; PNG rendered |
| `Part A — Design/Class Diagram/03-course-service.puml` | Course boundary | Updated & verified; PNG rendered |
| `Part A — Design/Class Diagram/05-goal-planner-service.puml` | Plan generation and approval | Updated & verified; PNG rendered |
| `Part A — Design/Class Diagram/13-habit-service.puml` | Habit completion and streak | Updated & verified; PNG rendered |
| `Part A — Design/Class Diagram/skill-service.puml` | Add Skill planned slice | Planned baseline updated; reconcile with PM2-08 |
| `Part A — Design/Class Diagram/notification-service.puml` | Deadline reminders planned slice | Planned baseline updated; reconcile with PM2-09 |

#### Cross-service PM2 scenario class diagram

![PM2 Scenario Class Diagram](Part%20A%20%E2%80%94%20Design/Class%20Diagram/00-pm2-scenario-class-diagram.png)

#### Academic Service (`academic-service`, FR-02)

![Academic Service Class Diagram](Part%20A%20%E2%80%94%20Design/Class%20Diagram/02-academic-service.png)

#### Course Service (`course-service`, FR-03)

![Course Service Class Diagram](Part%20A%20%E2%80%94%20Design/Class%20Diagram/03-course-service.png)

#### Goal Planner Service (`goal-planner-service`, FR-05)

![Goal Planner Service Class Diagram](Part%20A%20%E2%80%94%20Design/Class%20Diagram/05-goal-planner-service.png)

#### Habit Service (`habit-service`, FR-13)

![Habit Service Class Diagram](Part%20A%20%E2%80%94%20Design/Class%20Diagram/13-habit-service.png)

#### Skill Service (`skill-service`, FR-10 — planned minimal slice)

![Skill Service Class Diagram](Part%20A%20%E2%80%94%20Design/Class%20Diagram/skill-service.png)

#### Notification Service (`notification-service`, FR-18 — planned minimal slice)

![Notification Service Class Diagram](Part%20A%20%E2%80%94%20Design/Class%20Diagram/notification-service.png)

### A.3 Object diagrams

| Scenario | Object diagram | Key values | Status |
| --- | --- | --- | --- |
| Complete Goal | `Object Diagrams/01-ahmed-complete-goal-object.puml` | `goal1.status = Active`, completion timestamp | Done (#55 closed) |
| Generate Goal Plan | `Object Diagrams/generate-goal-plan-object.puml` | draft status, milestones, tasks | TODO |
| Complete Habit + streak | `Object Diagrams/complete-habit-object.puml` | habit, log, streak count | TODO |
| Add Skill | `Object Diagrams/add-skill-object.puml` | skill name, owner | TODO |
| Send Deadline Reminders | `Object Diagrams/deadline-reminders-object.puml` | notification status, deadline | TODO |

### A.4 Sequence diagrams

| Scenario | Sequence diagram | Branches shown | Status |
| --- | --- | --- | --- |
| Complete Goal | `Sequence Diagrams/01-ahmed-complete-goal-sequence.puml` | valid, idempotent, archived rejection, not found | Done (#56 closed) |
| Generate Goal Plan | `Sequence Diagrams/generate-goal-plan-sequence.puml` | success, AI unavailable, invalid plan, re-approve, edit-after-approve | TODO |
| Complete Habit + streak | `Sequence Diagrams/complete-habit-sequence.puml` | first completion, consecutive day, duplicate, gap reset | TODO |
| Add Skill | `Sequence Diagrams/add-skill-sequence.puml` | success, duplicate, invalid input | TODO |
| Send Deadline Reminders | `Sequence Diagrams/deadline-reminders-sequence.puml` | success, send failure, retry | TODO |

### A.5 Collaboration diagrams

| Scenario | Collaboration diagram | Message order matches sequence | Status |
| --- | --- | --- | --- |
| Complete Goal | `Collaboration Diagrams/01-ahmed-complete-goal-collaboration.puml` | Yes | Done (#57 closed) |
| Generate Goal Plan | `Collaboration Diagrams/generate-goal-plan-collaboration.puml` | Yes / TODO | TODO |
| Complete Habit + streak | `Collaboration Diagrams/complete-habit-collaboration.puml` | Yes / TODO | TODO |
| Add Skill | `Collaboration Diagrams/add-skill-collaboration.puml` | Yes / TODO | TODO |
| Send Deadline Reminders | `Collaboration Diagrams/deadline-reminders-collaboration.puml` | Yes / TODO | TODO |

### A.6 State diagrams and invalid transitions

#### Goal

Diagram: `Part A — Design/State Diagrams/01-goal-state.puml` / `.png`.

Valid transitions:

- `Active → Completed`
- `Completed → Completed` (idempotent)
- `Active → Archived`
- `Completed → Archived`
- `Archived → Active`

Invalid transitions that return 400:

- `Archived → Completed`
- `Active → Restore`
- `Completed → Restore`

A missing goal returns 404.

| Transition | Expected result | Test | Status |
| --- | --- | --- | --- |
| Active → Completed | 200, `CompletionDate` set | `CompleteGoal_FromActive_ReturnsCompleted` | Passing locally |
| Completed → Completed | 200, timestamp unchanged | `CompleteGoal_FromCompleted_IsIdempotent` | Passing locally |
| Archived → Completed | 400 | `CompleteGoal_FromArchived_ThrowsInvalidTransitionAndDoesNotUpdate` | Passing locally |
| Active → Archived | 200 | `ArchiveGoal_FromActive_ReturnsArchived` | Passing locally |
| Completed → Archived | 200 | `ArchiveGoal_FromCompleted_ReturnsArchived` | Passing locally |
| Archived → Active | 200 | `RestoreGoal_FromArchived_ReturnsActive` | Passing locally |
| Active → Restore | 400 | `RestoreGoal_FromActive_ThrowsInvalidTransitionAndDoesNotUpdate` | Passing locally |
| Completed → Restore | 400 | `RestoreGoal_FromCompleted_ThrowsInvalidTransitionAndDoesNotUpdate` | Passing locally |
| Missing goal | 404 | `CompleteGoal_MissingGoal_ThrowsNotFound` | Passing locally |

#### Other scenarios

| Scenario | State file | Valid transitions | Invalid transitions | Status |
| --- | --- | --- | --- | --- |
| Generate Goal Plan | `State Diagrams/plan-draft-state.puml` | Draft → Approved | edit after approve | TODO |
| Complete Habit + streak | `State Diagrams/habit-state.puml` | Pending → Completed | same-day duplicate is idempotent; gap resets streak | TODO |
| Add Skill | `State Diagrams/skill-state.puml` | New → Active | duplicate name | TODO |
| Send Deadline Reminders | `State Diagrams/notification-state.puml` | Pending → Sent, Pending → Failed, Failed → Sent | invalid retry of Sent | TODO |

### A.7 Design quality

- Controllers depend on provided interfaces, not concrete services.
- Services depend on repository and client interfaces, which makes Moq interaction tests possible.
- Domain state changes stay in the domain/service layer; controllers map results to HTTP responses.
- Cross-service reads use interfaces (`IGoalClient`, `IAssessment`), not direct database access.
- Invalid transitions are explicit and tested, not silently ignored.

---

## 3. Part B — DevOps

### B.1 Test strategy and Moq

Every scenario has interaction tests using xUnit and Moq. Tests follow Arrange / Act / Assert and name the behavior under test.

| Service | Test project | Interaction verified | Status |
| --- | --- | --- | --- |
| Academic | `tests/AcademicService.Tests/` | `IGoalRepository` load/update/save; controller 400/404 mapping | Moq tests added (`GoalMoqTests.cs`) |
| GoalPlanner | `tests/GoalPlannerService.Tests/` | `IAIProvider`, `IPlanDraftRepository`, `IGoalClient` | Moq approve-flow tests added |
| Habit | `tests/HabitService.Tests/` | `IHabitRepository`, streak update | Moq complete-flow tests added |
| Skill | `tests/SkillService.Tests/` | `ISkillRepository`, duplicate check | Blocked on PM2-08 implementation |
| Notification | `tests/NotificationService.Tests/` | `IAssessment`, `IEmailGateway`, `INotificationRepository` | Blocked on PM2-09 implementation |

**Current verified local run (2026-11-24):** 101 passed, 0 failed across Academic (37), Assessment (11), Course (10), GoalPlanner (16), Habit (13), and Student (14). Moq is now referenced by all six existing test projects with interaction tests in Academic, GoalPlanner, and Habit.
**Final test count:** _pending_ — update after PM2-08/09 and CI.

### B.2 Coverage

Command:

```bash
dotnet test Aspirra.slnx -c Release \
  --settings coverlet.runsettings \
  --collect:"XPlat Code Coverage" \
  --results-directory artifacts/coverage
```

| Metric | Value |
| --- | --- |
| Line coverage | **37.1%** overall (local opencover, 2026-11-24) |
| Branch coverage | _pending_ |
| Coverage artifact | `artifacts/coverage/**/coverage.opencover.xml` — generated locally |

Per-module line coverage (local, before PM2-08/09):

| Module | Line coverage |
| --- | --- |
| AcademicService | 51.8% (197/380) |
| GoalPlannerService | 39.1% (289/740) |
| AssessmentService | 37.3% (216/579) |
| StudentService | 35.4% (219/618) |
| HabitService | 33.8% (182/538) |
| CourseService | 28.5% (143/501) |

### B.3 SonarQube Cloud

| Item | Value |
| --- | --- |
| Organization | `aboosalh` |
| Project key | `AIU-SoftWave_Aspirra` |
| Quality gate | **Passed** (PR #69) |
| New bugs | 0 |
| New vulnerabilities | 0 |
| New code smells | 2 |
| Coverage (new code) | 89.5% (threshold 80%) |
| Coverage (overall) | 56.7% |
| Dashboard | https://sonarcloud.io/project/overview?id=AIU-SoftWave_Aspirra |
| PR dashboard | https://sonarcloud.io/dashboard?id=AIU-SoftWave_Aspirra&pullRequest=69 |

Quality gate conditions (all OK): new reliability rating A, new security rating A, new maintainability rating A, new coverage 89.5% ≥ 80%, new duplication 0.0%, security hotspots reviewed 100%.

### B.4 Jenkins

| Item | Value |
| --- | --- |
| Jenkins URL | _pending_ |
| Job | `aspirra-pm2` (create from `Jenkinsfile`) |
| Build result | _pending_ (pipeline run) |
| Image build | **Verified** — `aspirra-jenkins:latest` (1.63 GB), .NET SDK 10.0.404, dotnet-sonarscanner 12.1.0 |
| Duration | _pending_ |
| Console excerpt | _pending_ |

Pipeline stages: checkout → restore → build → test with coverage → SonarScanner (guarded by `sonar-token`) → archive.

**Status:** `Jenkinsfile` and the Docker controller (`devops/jenkins/Dockerfile`, `devops/jenkins/docker-compose.yml`) are scaffolded; the controller image builds successfully (verified 2026-11-25). The Jenkins *pipeline* has not been executed yet, so URL, build result, duration, and console excerpt remain pending.

### B.5 GitHub Actions vs Jenkins

| Aspect | GitHub Actions | Jenkins |
| --- | --- | --- |
| Trigger | Push/PR to `develop` | _pending_ |
| Environment | `ubuntu-latest`, .NET 10 | Docker agent, .NET 10, JDK 21 |
| Restore/build/test | Yes | _pending_ |
| Coverage | _pending_ | _pending_ |
| Sonar | _pending_ | _pending_ |
| Artifacts | _pending_ | _pending_ |
| Duration | _pending_ | _pending_ |
| Result | _pending_ | _pending_ |

**Comparison:** _pending_ — explain differences in runtime, environment control, artifact retention, and reproducibility after both runs exist.

### B.6 Reproduction

```bash
git clone https://github.com/AIU-SoftWave/Aspirra.git
cd Aspirra
git checkout pm2-submission
dotnet restore Aspirra.slnx
dotnet build Aspirra.slnx -c Release --no-restore
dotnet test Aspirra.slnx -c Release --no-build
docker compose up --build
```

**Observed result:** _pending_

---

## 4. Part C — Project Management

### C.1 Sprint 1 actuals

| Item | Value |
| --- | --- |
| Sprint | Sprint 1 — Foundation |
| Dates | 2026-10-22 → 2026-11-04 |
| Stories | 16 |
| Committed SP | 46 |
| Completed SP | 46 |
| Estimation error | 0% |

### C.2 Sprint 2 plan and execution

| Item | Value |
| --- | --- |
| Sprint | Sprint 2 — Design & DevOps |
| Dates | 2026-11-12 → 2026-11-28 |
| Tasks | 15 |
| Committed SP | 48 |
| Completed SP | _pending_ |
| State | Active |

Task-level table: see [`Part C — Project Management/README.md`](Part%20C%20%E2%80%94%20Project%20Management/README.md).

### C.3 Velocity and revised plan

| Sprint | Committed | Completed | Estimation error |
| --- | --- | --- | --- |
| Sprint 1 | 46 | 46 | 0% |
| Sprint 2 | 48 | _pending_ | _pending_ |

The C9 product plan shifts by one sprint because Sprint 2 is a process/design sprint:

| Planned sprint | Scope | SP |
| --- | --- | --- |
| Sprint 3 | C9 Sprint 2 | 50 |
| Sprint 4 | C9 Sprint 3 | 49 |
| Sprint 5 | C9 Sprint 4 | 49 |
| Sprint 6 | C9 Sprint 5 | 31 |

### C.4 Risks and trade-offs

- The design sprint consumes the schedule buffer; new product scope requires an explicit trade-off.
- Arwa/Muhammed code slices may land late; checkpoints are Nov 26 12:00 (diagrams + code skeleton) and Nov 27 12:00 (implementation and tests).
- SonarQube and Jenkins credentials are external dependencies.
- The PDF must be rebuilt after the final commit; a stale PDF is not acceptable.

---

## 5. Part D — Traceability

The full matrix is in [`Part D — Traceability/README.md`](Part%20D%20%E2%80%94%20Traceability/README.md) with the detailed design-to-code mapping in [`Part D — Traceability/Design-Code-Mapping.md`](Part%20D%20%E2%80%94%20Traceability/Design-Code-Mapping.md).

Summary:

| Scenario | Design artifacts | Code | Tests | Jira / GitHub |
| --- | --- | --- | --- | --- |
| Complete Goal | object, sequence, collaboration, state, class | AcademicService | AcademicService.Tests | `SCRUM-175`–`SCRUM-179`, `#54`–`#58` |
| Generate Goal Plan | object, sequence, collaboration, state, class | GoalPlannerService | GoalPlannerService.Tests | `SCRUM-180`, `#59` |
| Complete Habit + streak | object, sequence, collaboration, state, class | HabitService | HabitService.Tests | `SCRUM-181`, `#60` |
| Add Skill | object, sequence, collaboration, state, class | SkillService | SkillService.Tests | `SCRUM-182`, `#61` |
| Send Deadline Reminders | object, sequence, collaboration, state, class | NotificationService | NotificationService.Tests | `SCRUM-183`, `#62` |
| DevOps | Part B | CI, Sonar, Jenkins | All PM2 tests | `SCRUM-184`–`SCRUM-186`, `#63`–`#65` |
| PM / report | Parts C–D, report | N/A | Final verification | `SCRUM-187`–`SCRUM-189`, `#66`–`#68` |

---

## 6. Verification and Evidence

| Evidence | Link | Verified |
| --- | --- | --- |
| Final `dotnet test` | _pending_ | [ ] |
| GitHub Actions run | _pending_ | [ ] |
| Coverage artifact | _pending_ | [ ] |
| SonarQube quality gate | _pending_ | [ ] |
| Jenkins build | _pending_ | [ ] |
| Sprint 1 burndown | _pending_ | [ ] |
| Sprint 2 burndown | _pending_ | [ ] |
| Tag `pm2-submission` | _pending_ | [ ] |

**Commit SHA submitted:** _pending_

---

## 7. Team Contributions

| Member | PM2 contribution | Evidence |
| --- | --- | --- |
| Ahmed | Class/state diagrams, Complete Goal scenario, Moq tests, Sonar, Jenkins, report | `#54`–`#58`, `#63`–`#68` |
| Yomna | Generate Goal Plan design set and tests | `#59` |
| Arwa | Add Skill design set, code slice, tests | `#61` |
| Mostafa | Complete Habit + streak design set and tests | `#60` |
| Muhammed | Send Deadline Reminders design set, code slice, tests | `#62` |

Detailed per-member statements: [`members/`](members/).

---

## 8. Appendices

### Appendix A — Issue map

See [`README.md`](README.md) for the full Jira/GitHub/owner/SP map.

### Appendix B — Commands

```bash
dotnet restore Aspirra.slnx
dotnet build Aspirra.slnx -c Release --no-restore
dotnet test Aspirra.slnx -c Release --no-build
plantuml -tpng "docs/PM2/Part A — Design/<folder>/<file>.puml"
```

### Appendix C — Evidence index

See [`SUBMISSION.md`](SUBMISSION.md) for the final submission checklist.
