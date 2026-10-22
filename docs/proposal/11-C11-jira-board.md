# C11 — Jira board

> Issue: #11 · Part: C · Owner: Ahmed Mohamed Ahmed · Reviewer: TBD · Status: Completed

## Jira details

| Field | Value |
| --- | --- |
| **Jira site** | https://aspirra.atlassian.net |
| **Project name** | Aspirra Project |
| **Project key** | SCRUM |
| **Board** | SCRUM board (Board ID: 1) |
| **Issue types used** | Epic, Task (stories created as Task with `story` label; no native `Story` issue type available) |
| **Team-managed** | Yes |

## Epics

- Created **20 epics** (one per high-level functional requirement FR-01–FR-20), linked via parent relationship.
- Epic keys: **SCRUM-10** (FR-01) through **SCRUM-27** (FR-20).
- Each epic includes owner, service, linked customer requirements, and labels `epic`, `FR-XX`, and the corresponding service label.

## Product backlog (user stories)

- Created **73 user stories** as Jira Tasks (`Task` issue type) with label `story`.
- Stories are linked to their parent epics (e.g., US-01.1–US-01.3 under FR-01 / SCRUM-10).
- Each story includes the user story text, acceptance criteria, traceability (covered low-level FRs), priority, and service labels.
- **Total story points:** **225**.
- **Assignees:** None (stories remain unassigned as requested).
- Story keys range from **SCRUM-101** (US-02.1) to **SCRUM-173** (US-20.6).

## Sprint 1

| Field | Value |
| --- | --- |
| **Sprint ID** | 1 |
| **Sprint name** | Sprint 1 — Foundation |
| **State** | Active |
| **Start date** | 2026-10-22 |
| **End date** | 2026-11-04 |
| **Goal** | Deliver the core academic foundation, profile, goals, courses, assessments, habits, and the first AI goal planner. |
| **Stories committed** | 16 |
| **Points committed** | 46 |

**Sprint 1 stories (by key):** SCRUM-104 (US-01.1, 3), SCRUM-102 (US-01.3, 1), SCRUM-103 (US-01.2, 2), SCRUM-101 (US-02.1, 3), SCRUM-105 (US-02.2, 2), SCRUM-106 (US-02.3, 1), SCRUM-108 (US-03.1, 3), SCRUM-113 (US-04.1, 3), SCRUM-114 (US-04.2, 2), SCRUM-115 (US-04.3, 2), SCRUM-109 (US-03.2, 3), SCRUM-143 (US-13.1, 3), SCRUM-144 (US-13.2, 3), SCRUM-117 (US-05.1, 8), SCRUM-118 (US-05.2, 5), SCRUM-119 (US-05.3, 2).

## Reports

- **Sprint report / burndown:** [SCRUM Sprint 1 — burndown/report](https://aspirra.atlassian.net/jira/software/projects/SCRUM/boards/1/reports/burndown-chart?sprint=1)

## Cleanup performed

- Deleted sample issues **SCRUM-1**–**SCRUM-4**.
- Deleted **SCRUM Sprint 0**.
- Renamed/reconfigured the original future sprint to **Sprint 1 — Foundation** with dates 2026-10-22–2026-11-04, goal set, and state set to **Active**.

## Checklist

- [x] Create Jira Scrum project (Aspirra Project / SCRUM, board 1)
- [x] Create epic per high-level FR (20 epics, FR-01–FR-20, SCRUM-10–SCRUM-27)
- [x] Create stories linked to epics with points (73 Tasks, linked via parent, story points in `customfield_10016`, total 225)
- [x] Add acceptance criteria and service labels (each story includes AC, traceability, priority, FR/service/story labels)
- [x] Start Sprint 1 and capture burndown (active, 2026-10-22–2026-11-04, 16 stories / 46 points; burndown link recorded)
- [ ] Add the teaching assistant (skipped — no TA email provided; `acli` cannot add project members non-interactively)
