# PM2 Workflow

This is the working agreement for PM2. It keeps Jira, GitHub, the repository, and the report aligned.

## Sources of truth

| Question | Source |
| --- | --- |
| What is committed in Sprint 2? | Jira Sprint 2 (`39`) |
| What is the task status and story points? | Jira task `SCRUM-175`–`SCRUM-189` |
| What exactly must be delivered? | GitHub issue `#54`–`#68` |
| What is the actual artifact? | Files in this repository |
| What is proven? | Tests, CI runs, SonarQube, Jenkins, screenshots |
| What is submitted? | `docs/PM2/report.pdf` plus per-member appendices |

Jira and GitHub mirror each other, but **Jira wins** for sprint status and points. If they disagree, correct GitHub to match Jira and add a comment explaining the change.

## Setup already completed

- Jira Sprint 2 (`39`) is active: `2026-11-12` → `2026-11-28`.
- Jira epic `SCRUM-174` owns PM2; tasks `SCRUM-175`–`SCRUM-189` are parented to it.
- GitHub milestone `#2` and parent issue `#53` own the GitHub side.
- GitHub labels `pm2`, `type:test`, `type:devops`, `type:docs` exist.
- Every Jira task has a comment linking to its GitHub mirror.

Do not recreate these items. If a field is wrong, edit the existing item.

## Branch and commit rules

- Branch from `develop`.
- Preferred integration branch for the milestone: `feature/pm2-design-devops`.
- Focused work may use a sub-branch such as `feature/pm2-goal-state` created from the integration branch.
- Never commit directly to `develop` or `main`.
- Commit format:

```text
PM2-XX #<issue> <type>: <short imperative message>
```

Examples:

```text
PM2-05 #58 fix: reject invalid Goal state transitions
PM2-10 #63 test: add Moq interaction tests for Complete Goal
PM2-12 #65 ci: add Jenkins pipeline and comparison notes
```

Allowed `<type>` values: `feat`, `fix`, `test`, `docs`, `ci`, `chore`, `refactor`.

- One logical change per commit.
- Reference the Jira key and GitHub issue in the PR body.
- Do not commit `.env`, secrets, `bin/`, `obj/`, coverage dumps, or generated PDFs before PM2-15 freezes the report.

## Daily workflow

1. Open your assigned GitHub issue and read the deliverables and acceptance criteria.
2. Move the Jira task to **In Progress**.
3. Create or switch to your branch.
4. Implement the smallest complete slice: artifact + test + evidence.
5. Run the relevant tests locally.
6. Commit with the required format and push.
7. Open a PR into `feature/pm2-design-devops` (or `develop` if the integration branch is not needed).
8. Put the Jira key and GitHub issue number in the PR title/body.
9. Request review from the artifact owner or another member.
10. After merge: close the GitHub issue and move the Jira task to **Done** only when the evidence exists.

## Jira status mapping

| Jira status | GitHub state | Meaning |
| --- | --- | --- |
| Idea / Ready | Open, unassigned or assigned | Not started |
| In Progress | Open, assigned, branch exists | Work underway |
| In Review | Open, PR open | Waiting for review |
| Done | Closed | Merged and evidenced |

Do not close a GitHub issue merely because code was written. Close it when the PR is merged, tests pass, and the traceability row exists.

## Definition of done

A PM2 task is done only when all of the following are true:

- [ ] The deliverable exists at the path named in the issue.
- [ ] The artifact uses real code names, not invented names.
- [ ] Tests pass locally and in CI.
- [ ] Every mocked collaboration has a Moq `Verify`.
- [ ] The relevant `.puml` renders to `.png`.
- [ ] The artifact is linked from the report and Part D traceability.
- [ ] The Jira task is **Done**.
- [ ] The GitHub issue is closed.
- [ ] No secrets or unrelated files are included.

## Diagram rules

- Source and render live together: `name.puml` and `name.png`.
- Use PlantUML; render before committing.
- Object, sequence, and collaboration diagrams for one scenario must use the same participants and message order.
- State diagrams must show valid and invalid transitions; invalid transitions must map to tests.
- Class diagrams must contain every class/interface/method used by the messages.
- Use the exact implemented names, including `IGoalRepository`, `IPlanDraftRepository`, `IAIProvider`, `IGoalClient`, `IHabitRepository`, `ISkillRepository`, `IAssessment`, `IEmailGateway`, and `INotificationRepository` where applicable.

Render command:

```bash
plantuml -tpng "docs/PM2/Part A — Design/<folder>/<file>.puml"
```

If `plantuml` is unavailable, use the repository's PlantUML service or the VS Code PlantUML extension and commit both files.

## Test rules

- Use xUnit and Moq.
- Follow Arrange / Act / Assert.
- Name tests by behavior, not implementation: `CompleteGoal_WhenArchived_ReturnsInvalidTransition`.
- Mock interfaces, not concrete services.
- Verify every collaboration that the design claims:
  - `IAIProvider.GenerateAsync` called once for a successful plan.
  - `IPlanDraftRepository.AddAsync` and `SaveChangesAsync` called once.
  - `IGoalClient` called only when the scenario requires it.
  - `IHabitRepository.UpdateAsync` / `SaveChangesAsync` for habit completion.
  - `ISkillRepository.AddAsync` / `SaveChangesAsync` for Add Skill.
  - `IAssessment.GetDeadlinesAsync`, `IEmailGateway.SendAsync`, and `INotificationRepository.SaveChangesAsync` for reminders.
- Use `Times.Once`, `Times.Never`, and `It.Is<T>` where the argument matters.
- Add a negative test for every invalid transition or failure branch.
- Do not assert on log text or implementation details.

Commands:

```bash
dotnet restore Aspirra.slnx
dotnet build Aspirra.slnx -c Release --no-restore
dotnet test Aspirra.slnx -c Release --no-build
```

Focused runs:

```bash
dotnet test tests/AcademicService.Tests/AcademicService.Tests.csproj -c Release
dotnet test tests/GoalPlannerService.Tests/GoalPlannerService.Tests.csproj -c Release
dotnet test tests/HabitService.Tests/HabitService.Tests.csproj -c Release
```

## Evidence capture

Record the command, date, commit SHA, and result. Do not paste secrets or full environment dumps.

Minimum evidence for PM2:

| Evidence | How to capture |
| --- | --- |
| Test count and pass/fail | `dotnet test` output |
| Coverage | `coverlet` output and CI artifact URL |
| SonarQube | Quality gate URL and screenshot |
| GitHub Actions | Run URL |
| Jenkins | Build URL and console excerpt |
| Jira Sprint 1 | Burndown screenshot |
| Jira Sprint 2 | Burndown screenshot |
| Traceability | `docs/PM2/Part D — Traceability/Design-Code-Mapping.md` |

## Pull request checklist

- [ ] Base branch is `feature/pm2-design-devops` or `develop`.
- [ ] PR title starts with `PM2-XX`.
- [ ] PR body links the Jira key and GitHub issue.
- [ ] Diff contains only the intended files.
- [ ] Tests pass locally.
- [ ] Diagram source and PNG are both present.
- [ ] Report/traceability placeholders updated if applicable.
- [ ] No secrets or build artifacts.

## Checkpoints

| Time | Checkpoint |
| --- | --- |
| Nov 26 12:00 | Diagrams + code skeleton |
| Nov 27 12:00 | Implementation and tests complete |
| Nov 27 20:00 | PRs merged into `develop`; Sonar + Jenkins green |
| Nov 28 12:00 | Report draft; Sprint 2 close-out |
| Nov 28 18:00 | Code freeze; final verification |
| Nov 28 20:00 | PDF + tag ready; each member submits before 23:59 |

If a checkpoint slips, reduce scope in the report and say so. Do not invent results.

## Escalation

- Blocked on a service owner: comment on the GitHub issue and tag the owner.
- Blocked on Jira fields or sprint state: ask Ahmed; do not recreate tasks.
- Blocked on Sonar or Jenkins credentials: ask Ahmed; never commit a token.
- Conflict between issues: Jira wins for scope/points; GitHub wins for artifact detail; ask before changing either.
