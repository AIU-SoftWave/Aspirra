# Design → Code Mapping (Part D)

Every diagram maps to a real artifact (code, configuration, Jira, or the database).

## Architecture Diagrams

| Diagram | Artifact(s) | Notes |
| --- | --- | --- |
| System Context (Level 0) - `docs/PM1/Part A — Design/Architecture Diagrams/System Context diagram.puml` / `.png` | `README.md`, Jira Project `SCRUM` | Actors (Student, Administrator), Aspirra system, external systems (AI Provider, Email Gateway), provided interface list on the system box and actor relations, required interfaces `IAIProvider` / `IEmailGateway` |
| Interfaces (Level 0) - `docs/PM1/Part A — Design/Architecture Diagrams/Interfaces/Interfaces.md` | `docs/proposal/04-B4-functional-requirements.md` (FR-01–FR-20), Jira Epics/Stories, `src/Services/*/Interfaces/` | Every provided interface with its high-level FR and grouped low-level FRs; every required interface operation annotated with `(used by: FR-XX.Y)`; Sprint 1 operations match the implemented interfaces |
| Container (Level 1) - `docs/PM1/Part A — Design/Container Diagrams/Aspirra-Container-Diagram.puml` / `.png` | `docker-compose.yml`, `src/Services/*`, `src/ApiGateway/`, EF Core migrations | One container per service, connected via provided/required interfaces, shared PostgreSQL instance with one schema per service |
| Component (Level 2) - `docs/PM1/Part A — Design/Component Diagrams/*.puml` / `*.png` (20 services) | `src/Services/*/`, `src/ApiGateway/` | Per-service components, provided/required interfaces, internals by owner |

## Class Diagrams (Level 3)

| Diagram | Artifact(s) | Notes |
| --- | --- | --- |
| System Class Diagram - `docs/PM1/Part A — Design/Class Diagram/00-system-class-diagram.puml` / `.png` | All services in `src/Services/`, interfaces per container | SOLID applied, cross-service dependencies via interfaces |
| Per-service Class Diagrams - `docs/PM1/Part A — Design/Class Diagram/0X-*-service.puml` / `.png` | Corresponding `src/Services/<Service>/` | Prepared by owner, aligned with component diagrams; implemented services reflect the real domain classes |

## ERD / Data Structure

| Diagram | Artifact(s) | Notes |
| --- | --- | --- |
| ERDs - `docs/PM1/Part A — Design/ERD/*.puml` / `*.png` (20 files) | `src/Services/*/Data/*DbContext.cs`, `src/Services/*/Models/`, `src/Services/*/Data/Migrations/` | Shared PostgreSQL instance, each service owns its own schema/tables (`student`, `academic`, `course`, `assessment`, `goal_planner`, `habit`), no cross-service DB reads directly (logical references only). EF Core migrations create and evolve each schema; the six implemented ERDs match the generated migrations. |

## DevOps

| Artifact | Artifact(s) | Notes |
| --- | --- | --- |
| Multi-stage Dockerfiles - `src/Services/*/Dockerfile`, `src/ApiGateway/Dockerfile` | `docker-compose.yml` | One per service/container as required. Sprint 1 services use real `net10.0` multi-stage builds; the remaining 14 Dockerfiles are placeholders for later sprints |
| Docker Compose - `docker-compose.yml` | 6 Sprint 1 services + `aspirra-db` (shared) | Single `docker compose up` brings up the implemented stack; the remaining 14 services are commented out until their sprints |
| Environment - `.env.example` | `docker-compose.yml` | Environment variables for the database, AI provider and email gateway |
| GitHub Actions - `.github/workflows/ci.yml` | GHCR (`ghcr.io/aiu-softwave/aspirra/*`) | Restores, builds and runs the 74-test suite, then builds/pushes the 6 Sprint 1 images to GHCR on push to develop/main |

## Project Management

| Artifact | Artifact(s) | Notes |
| --- | --- | --- |
| Jira | https://aspirra.atlassian.net (Project SCRUM, Board 1) | Epics per FR-01–FR-20, stories labeled `story`, Sprint 1 closed, traceable to backlog |
| Backlog/Estimation | `docs/proposal/08-C8-product-backlog.md`, `docs/proposal/09-C9-estimation-and-sprint-plan.md` | User stories US-XX.Y, story points, sprint plan |

## Sprint 1 Code Artifacts (Traceability)

| User Story (Jira) | Service | Code Artifacts | Maps To |
| --- | --- | --- | --- |
| US-01.1/01.2/01.3 (SCRUM-104/103/102) | StudentService | `src/Services/StudentService/Controllers/ProfilesController.cs`, `Interfaces/IStudentProfile.cs`, `Services/StudentProfileService.cs`, `Repositories/ProfileRepository.cs`, `Data/StudentDbContext.cs`, `Data/Migrations/`, `Models/Profile.cs`, `Models/TimeSlot.cs`, `Dockerfile`, `tests/StudentService.Tests/` | Component: `docs/PM1/Part A — Design/Component Diagrams/01-student-service-component.puml` / `.png`, Class: `docs/PM1/Part A — Design/Class Diagram/01-student-service.puml` / `.png`, ERD: `docs/PM1/Part A — Design/ERD/01-student-service-erd.puml` / `.png` |
| US-02.1/02.2/02.3 (SCRUM-101/105/106) | AcademicService | `src/Services/AcademicService/Controllers/GoalsController.cs`, `Interfaces/IGoal.cs`, `Services/GoalService.cs`, `Repositories/GoalRepository.cs`, `Data/AcademicDbContext.cs`, `Data/Migrations/`, `Models/Goal.cs`, `Models/GoalStatus.cs`, `Dockerfile`, `tests/AcademicService.Tests/` | Component: `docs/PM1/Part A — Design/Component Diagrams/02-academic-service-component.puml` / `.png`, Class: `docs/PM1/Part A — Design/Class Diagram/02-academic-service.puml` / `.png`, ERD: `docs/PM1/Part A — Design/ERD/02-academic-service-erd.puml` / `.png` |
| US-03.1/03.2 (SCRUM-108/109) | CourseService | `src/Services/CourseService/Controllers/CoursesController.cs`, `Interfaces/ICourse.cs`, `Interfaces/IAssessmentClient.cs`, `Services/CourseService.cs`, `Services/HttpAssessmentClient.cs`, `Repositories/CourseRepository.cs`, `Data/CourseDbContext.cs`, `Data/Migrations/`, `Models/Course.cs`, `Models/CourseEnrollment.cs`, `Dockerfile`, `tests/CourseService.Tests/` | Component: `docs/PM1/Part A — Design/Component Diagrams/03-course-service-component.puml` / `.png`, Class: `docs/PM1/Part A — Design/Class Diagram/03-course-service.puml` / `.png`, ERD: `docs/PM1/Part A — Design/ERD/03-course-service-erd.puml` / `.png` |
| US-04.1/04.2/04.3 (SCRUM-113/114/115) | AssessmentService | `src/Services/AssessmentService/Controllers/`, `Contracts/Requests.cs`, `Interfaces/IAssessment.cs`, `Services/AssessmentService.cs`, `Repositories/EfAssessmentRepository.cs`, `Data/AssessmentDbContext.cs`, `Data/Migrations/`, `Models/Assignment.cs`, `Models/Exam.cs`, `Models/AssignmentStatus.cs`, `Dockerfile`, `tests/AssessmentService.Tests/` | Component: `docs/PM1/Part A — Design/Component Diagrams/04-assessment-service-component.puml` / `.png`, Class: `docs/PM1/Part A — Design/Class Diagram/04-assessment-service.puml` / `.png`, ERD: `docs/PM1/Part A — Design/ERD/04-assessment-service-erd.puml` / `.png` |
| US-05.1/05.2/05.3 (SCRUM-117/118/119) | GoalPlannerService | `src/Services/GoalPlannerService/Controllers/PlansController.cs`, `Contracts/PlanContracts.cs`, `Interfaces/IGoalPlanner.cs`, `Services/GoalPlannerService.cs`, `Services/MockAIProvider.cs`, `Clients/HttpGoalClient.cs`, `Repositories/PlanDraftRepository.cs`, `Data/GoalPlannerDbContext.cs`, `Data/Migrations/`, `Models/PlanDraft.cs`, `Models/Milestone.cs`, `Models/PlanTask.cs`, `Dockerfile`, `tests/GoalPlannerService.Tests/` | Component: `docs/PM1/Part A — Design/Component Diagrams/05-goal-planner-service-component.puml` / `.png`, Class: `docs/PM1/Part A — Design/Class Diagram/05-goal-planner-service.puml` / `.png`, ERD: `docs/PM1/Part A — Design/ERD/05-goal-planner-service-erd.puml` / `.png` |
| US-13.1/13.2 (SCRUM-143/144) | HabitService | `src/Services/HabitService/Controllers/HabitsController.cs`, `Dtos/`, `Interfaces/IHabit.cs`, `Interfaces/IHabitRepository.cs`, `Services/HabitService.cs`, `Repositories/HabitRepository.cs`, `Data/HabitDbContext.cs`, `Data/Migrations/`, `Models/Habit.cs`, `Models/HabitLog.cs`, `Dockerfile`, `tests/HabitService.Tests/` | Component: `docs/PM1/Part A — Design/Component Diagrams/13-habit-service-component.puml` / `.png`, Class: `docs/PM1/Part A — Design/Class Diagram/13-habit-service.puml` / `.png`, ERD: `docs/PM1/Part A — Design/ERD/13-habit-service-erd.puml` / `.png` |
