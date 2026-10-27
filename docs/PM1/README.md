# PM1 Submission Index - Aspirra

This directory contains all deliverables for PM1 (Part A–D).

## Part A — Design [50 marks]

### Architecture Diagrams
- [System Context Diagram (Level 0) - PNG](Part%20A%20%E2%80%94%20Design/Architecture%20Diagrams/System%20Context%20diagram.png)
- [System Context Diagram (Level 0) - C4 PlantUML](Part%20A%20%E2%80%94%20Design/Architecture%20Diagrams/System%20Context%20diagram.puml)
- [Interfaces Document (Level 0)](Part%20A%20%E2%80%94%20Design/Architecture%20Diagrams/Interfaces/Interfaces.md)

### Container Diagrams (Level 1)
- [Aspirra Container Diagram](Part%20A%20%E2%80%94%20Design/Container%20Diagrams/Aspirra-Container-Diagram.puml) - One container per service, connected via provided/required interfaces, shared DB

### Component Diagrams (Level 2)
- [Component Diagram Template](Part%20A%20%E2%80%94%20Design/Component%20Diagrams/00-COMPONENT-DIAGRAM-TEMPLATE.puml)
- [All 20 Service Component Diagrams](Part%20A%20%E2%80%94%20Design/Component%20Diagrams/) (by owner per AGENTS.md)

### Class Diagrams (Level 3)
- [System Class Diagram (after SOLID)](Part%20A%20%E2%80%94%20Design/Class%20Diagram/00-system-class-diagram.puml)
- [Per-Service Class Diagrams](Part%20A%20%E2%80%94%20Design/Class%20Diagram/) (01-20)

### ERD / Data Structure
- [ERD Template](Part%20A%20%E2%80%94%20Design/ERD/00-ERD-Template.puml)
- [All 20 Service ERDs](Part%20A%20%E2%80%94%20Design/ERD/) - Shared DB, each service owns its own data/schema, logical references only

## Part B — DevOps [20 marks]

- [Dockerfiles (all services)](../src/Services/) - Multi-stage Dockerfiles for all 20 services
- [API Gateway Dockerfile](../src/ApiGateway/Dockerfile)
- [docker-compose.yml](../docker-compose.yml) - Single `docker compose up` command, all services + shared Postgres
- [.env.example](../.env.example)
- [GitHub Actions CI (GHCR)](../.github/workflows/ci.yml) - Builds and pushes images to ghcr.io/aiu-softwave/aspirra

## Part C — Project Management [20 marks]

- [Sprint Plan Table](Part%20C%20%E2%80%94%20Project%20Management/Sprint-Plan-Table.md) - Sprint 1 completed (46 SP, all done, 0% estimation error)
- [Jira Board & Burndown](Part%20C%20%E2%80%94%20Project%20Management/Jira-Board-and-Burndown.md) - Board 1, Sprint 1 closed, all stories assigned/estimated/completed

## Part D — Traceability [10 marks]

- [Design → Code Mapping](Part%20D%20%E2%80%94%20Traceability/Design-Code-Mapping.md) - Maps diagrams to code, Docker, Jira, and DB artifacts

## Sprint 1 Code Artifacts (Traceable)
Minimal implementations for Sprint 1 stories in `src/Services/` (Student, Academic, Course, Assessment, GoalPlanner, Habit).
