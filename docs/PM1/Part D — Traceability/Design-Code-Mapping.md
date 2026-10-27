# Design → Code Mapping (Part D)

Every diagram maps to a real artifact (code, configuration, Jira, or the database).

## Architecture Diagrams

| Diagram | Artifact(s) | Notes |
| --- | --- | --- |
| System Context (Level 0) - `docs/PM1/Part A — Design/Architecture Diagrams/System Context diagram.puml` / `.png` | `README.md`, Jira Project `SCRUM` | Actors (Student, Administrator), Aspirra system, external systems (AI Provider, Email Gateway), provided/required interfaces |
| Interfaces (Level 0) - `docs/PM1/Part A — Design/Architecture Diagrams/Interfaces/Interfaces.md` | `docs/proposal/08-C8-product-backlog.md` (FR-01–FR-20), Jira Epics/Stories | High-level/low-level FRs, required interfaces with `(used by)` mapping |
| Container (Level 1) - `docs/PM1/Part A — Design/Container Diagrams/Aspirra-Container-Diagram.puml` | `docker-compose.yml`, `src/Services/*`, `src/ApiGateway/` | One container per service, connected via provided/required interfaces, shared DB (`aspirra-db`) |
| Component (Level 2) - `docs/PM1/Part A — Design/Component Diagrams/*.puml` (20 files) | `src/Services/*/`, `src/ApiGateway/` | Per-service components, provided/required interfaces, internals by owner |

## Class Diagrams (Level 3)

| Diagram | Artifact(s) | Notes |
| --- | --- | --- |
| System Class Diagram - `docs/PM1/Part A — Design/Class Diagram/00-system-class-diagram.puml` | All services in `src/Services/`, interfaces per container | SOLID applied, cross-service dependencies via interfaces |
| Per-service Class Diagrams - `docs/PM1/Part A — Design/Class Diagram/0X-*-service.puml` | Corresponding `src/Services/<Service>/` | Prepared by owner, aligned with component diagrams |

## ERD / Data Structure

| Diagram | Artifact(s) | Notes |
| --- | --- | --- |
| ERDs - `docs/PM1/Part A — Design/ERD/*.puml` (20 files) | `docker-compose.yml` (shared PostgreSQL), future migrations/entities in each service | Shared DB instance, each service owns its own schema/tables, no cross-service DB reads directly (logical references only) |

## DevOps

| Artifact | Artifact(s) | Notes |
| --- | --- | --- |
| Multi-stage Dockerfiles - `src/Services/*/Dockerfile`, `src/ApiGateway/Dockerfile` | `docker-compose.yml` | One per service/container as required |
| Docker Compose - `docker-compose.yml` | All services + `aspirra-db` (shared) | Single `docker compose up` brings up entire system |
| Environment - `.env.example` | `docker-compose.yml` | Environment variables for DB and external services |
| GitHub Actions - `.github/workflows/ci.yml` | GHCR (`ghcr.io/aiu-softwave/aspirra/*`) | Build pipeline, pushes images to GHCR on push to develop/main |

## Project Management

| Artifact | Artifact(s) | Notes |
| --- | --- | --- |
| Jira | https://aspirra.atlassian.net (Project SCRUM, Board 1) | Epics per FR-01–FR-20, stories labeled `story`, Sprint 1 active, traceable to backlog |
| Backlog/Estimation | `docs/proposal/08-C8-product-backlog.md`, `docs/proposal/09-C9-estimation-and-sprint-plan.md` | User stories US-XX.Y, story points, sprint plan |
