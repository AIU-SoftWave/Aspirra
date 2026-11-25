# Part C — Project Management

PM2 project-management evidence: Sprint 1 actuals, Sprint 2 execution, velocity, and the revised product plan.

## Sprint overview

| Sprint | Goal | Start | End | State | Committed | Completed |
| --- | --- | --- | --- | --- | --- | --- |
| Sprint 1 — Foundation | Core academic foundation and first AI plan | 2026-10-22 | 2026-11-04 | Closed | 46 SP | 46 SP |
| Sprint 2 — Design & DevOps | PM2 design and DevOps evidence | 2026-11-12 | 2026-11-28 | Active | 48 SP | _pending_ |
| Sprint 3 — Product increment 2 | C9 Sprint 2 product scope | _pending_ | _pending_ | Planned | 50 SP | _pending_ |
| Sprint 4 — Product increment 3 | C9 Sprint 3 product scope | _pending_ | _pending_ | Planned | 49 SP | _pending_ |
| Sprint 5 — Product increment 4 | C9 Sprint 4 product scope | _pending_ | _pending_ | Planned | 49 SP | _pending_ |
| Sprint 6 — Product increment 5 | C9 Sprint 5 product scope | _pending_ | _pending_ | Planned | 31 SP | _pending_ |

## Sprint 2 — committed tasks

| ID | Jira | GitHub | Owner | SP | Status |
| --- | --- | --- | --- | --- | --- |
| PM2-01 | `SCRUM-175` | `#54` | Ahmed | 3 | Idea |
| PM2-02 | `SCRUM-176` | `#55` | Ahmed | 1 | Idea |
| PM2-03 | `SCRUM-177` | `#56` | Ahmed | 2 | Idea |
| PM2-04 | `SCRUM-178` | `#57` | Ahmed | 2 | Idea |
| PM2-05 | `SCRUM-179` | `#58` | Ahmed | 3 | Idea |
| PM2-06 | `SCRUM-180` | `#59` | Yomna | 5 | Idea |
| PM2-07 | `SCRUM-181` | `#60` | Mostafa | 5 | Idea |
| PM2-08 | `SCRUM-182` | `#61` | Arwa | 3 | Idea |
| PM2-09 | `SCRUM-183` | `#62` | Muhammed | 3 | Idea |
| PM2-10 | `SCRUM-184` | `#63` | Ahmed | 5 | Idea |
| PM2-11 | `SCRUM-185` | `#64` | Ahmed | 3 | Idea |
| PM2-12 | `SCRUM-186` | `#65` | Ahmed | 5 | Idea |
| PM2-13 | `SCRUM-187` | `#66` | Ahmed | 2 | Idea |
| PM2-14 | `SCRUM-188` | `#67` | Ahmed | 3 | Idea |
| PM2-15 | `SCRUM-189` | `#68` | Ahmed | 3 | Idea |
| **Total** |  |  |  | **48** |  |

Update this table from Jira at close-out. Do not mark anything Done before the evidence exists.

## Velocity

| Sprint | Committed | Completed | Estimation error | Notes |
| --- | --- | --- | --- | --- |
| Sprint 1 | 46 | 46 | 0% | 16 stories, all Done |
| Sprint 2 | 48 | _pending_ | _pending_ | PM2 process sprint |

## Revised product plan

Sprint 2 is a process/design sprint, so the C9 product plan shifts by one sprint:

| Planned sprint | Scope | SP | Reason |
| --- | --- | --- | --- |
| Sprint 3 | C9 Sprint 2 | 50 | Deferred by PM2 |
| Sprint 4 | C9 Sprint 3 | 49 | Follows Sprint 3 |
| Sprint 5 | C9 Sprint 4 | 49 | Follows Sprint 4 |
| Sprint 6 | C9 Sprint 5 | 31 | Remaining product scope |

The product backlog remains 179 SP after Sprint 1. The plan fits, but the schedule buffer is consumed; any new scope requires an explicit trade-off.

## Burndown evidence

- [ ] Sprint 1 burndown screenshot: _pending_
- [ ] Sprint 2 burndown screenshot: _pending_
- [ ] Jira board URL: https://aspirra.atlassian.net (board `1`)
- [ ] Sprint 2 report URL: _pending_

## Risks

| Risk | Impact | Mitigation |
| --- | --- | --- |
| Design sprint reduces product delivery time | Schedule pressure | Defer C9 Sprint 2, keep scope fixed |
| Arwa/Muhammed code slices incomplete | Missing scenario evidence | Checkpoints Nov 26 12:00 (skeleton) and Nov 27 12:00 (implementation/tests); reduce to design-only if blocked |
| Sonar token unavailable | No quality gate | Configure secret early; document blocker honestly |
| Jenkins environment drift | Pipeline differs from Actions | Pin SDK/JDK versions in Docker |
| Last-day PDF failure | No submission | Build PDF on Nov 27 and again on Nov 28 |
