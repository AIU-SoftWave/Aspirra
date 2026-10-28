# PM1 Submission Index - Aspirra

This directory contains all deliverables for PM1 (Part A–D). The consolidated submission report is [`report.md`](report.md), with all diagrams embedded, and the print-ready version is [`report.pdf`](report.pdf).

## Part A — Design [50 marks]

### Architecture Diagrams
- [System Context Diagram (Level 0) - PNG](Part%20A%20%E2%80%94%20Design/Architecture%20Diagrams/System%20Context%20diagram.png)
- [System Context Diagram (Level 0) - C4 PlantUML](Part%20A%20%E2%80%94%20Design/Architecture%20Diagrams/System%20Context%20diagram.puml)
- [Interfaces Document (Level 0)](Part%20A%20%E2%80%94%20Design/Architecture%20Diagrams/Interfaces/Interfaces.md)

### Container Diagrams (Level 1)
- [Aspirra Container Diagram (C4 PlantUML)](Part%20A%20%E2%80%94%20Design/Container%20Diagrams/Aspirra-Container-Diagram.puml)
- [Aspirra Container Diagram (PNG)](Part%20A%20%E2%80%94%20Design/Container%20Diagrams/Aspirra-Container-Diagram.png)

### Component Diagrams (Level 2)
- [Component Diagram Template](Part%20A%20%E2%80%94%20Design/Component%20Diagrams/00-COMPONENT-DIAGRAM-TEMPLATE.puml)
- [All 20 Service Component Diagrams (PlantUML + PNG)](Part%20A%20%E2%80%94%20Design/Component%20Diagrams/) (by owner per AGENTS.md)

### Class Diagrams (Level 3)
- [System Class Diagram (after SOLID)](Part%20A%20%E2%80%94%20Design/Class%20Diagram/00-system-class-diagram.puml)
- [All 20 Per-Service Class Diagrams (PlantUML + PNG)](Part%20A%20%E2%80%94%20Design/Class%20Diagram/) (01-20)

### ERD / Data Structure
- [ERD Template](Part%20A%20%E2%80%94%20Design/ERD/00-ERD-Template.puml)
- [All 20 Service ERDs (PlantUML + PNG)](Part%20A%20%E2%80%94%20Design/ERD/) - Shared DB, each service owns its own schema, logical references only; implemented ERDs match EF Core migrations

## Part B — DevOps [20 marks]

- [Dockerfiles (all services)](../src/Services/) - Multi-stage Dockerfiles for all 20 services (6 Sprint 1 services production-ready, 14 placeholders)
- [API Gateway Dockerfile](../src/ApiGateway/Dockerfile)
- [docker-compose.yml](../docker-compose.yml) - Single `docker compose up` command; Sprint 1 services + shared Postgres (remaining services commented out until later sprints)
- [.env.example](../.env.example)
- [GitHub Actions CI (GHCR)](../.github/workflows/ci.yml) - Restores, builds and tests the solution, then builds and pushes the 6 Sprint 1 images to ghcr.io/aiu-softwave/aspirra

## Part C — Project Management [20 marks]

- [Sprint Plan Table](Part%20C%20%E2%80%94%20Project%20Management/Sprint-Plan-Table.md) - Sprint 1 completed (46 SP, all done, 0% estimation error)
- [Jira Board & Burndown](Part%20C%20%E2%80%94%20Project%20Management/Jira-Board-and-Burndown.md) - Board 1, Sprint 1 closed, all stories assigned/estimated/completed

## Part D — Traceability [10 marks]

- [Design → Code Mapping](Part%20D%20%E2%80%94%20Traceability/Design-Code-Mapping.md) - Maps diagrams to code, Docker, Jira, and DB artifacts

## Sprint 1 Code Artifacts (Traceable)
Full implementations for Sprint 1 stories in `src/Services/` (Student, Academic, Course, Assessment, GoalPlanner, Habit): ASP.NET Core `net10.0`, EF Core 10 migrations per schema, cross-service HTTP clients, Docker images, and 74 passing xUnit tests.
