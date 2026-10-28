# AGENTS.md

Guidance for AI assistants working in this repository.

## Project

**Aspirra — Student Success & Career Planner** is a group project for CSE454 — Advanced Software Engineering at Alamein International University. It is an AI-powered platform that turns student goals into editable plans, schedules, habits, progress tracking, and career preparation.

- Repository: https://github.com/AIU-SoftWave/Aspirra (public monorepo)
- Main documentation: `README.md`
- Contribution guide: `CONTRIBUTING.md`
- Proposal deliverables: `docs/proposal/` (one file per graded issue)
- Proposed stack: React/Next.js + TypeScript frontend, ASP.NET Core Web API + C# backend, EF Core, PostgreSQL or SQL Server
- Architecture: modular monolith; service names are logical ownership labels, not required microservices

The project is in the planning/requirements phase. Features and technologies in the README are proposed and may change.

## Group ownership

Five members each own four services/features. Stay within your owned services unless a change is coordinated with the owner. Shared interfaces, cross-service changes, and requirement changes should be agreed with the affected owner and reviewer.

| Member | GitHub | Owned services / labels |
|---|---|---|
| Ahmed Mohamed Ahmed (Team Leader) | `AbooSalh` | `student-service`, `academic-service`, `course-service`, `assessment-service` |
| Yomna Mohamed Fekry | `YomnaElmenawy` | `goal-planner-service`, `study-planner-service`, `scheduling-service`, `adaptation-service` |
| Arwa Mohey Hamdy | `ArwaMohey` | `career-service`, `skill-service`, `portfolio-service`, `job-preparation-service` |
| Mostafa Khamis Abozead | `Mostafa-abozead` | `habit-service`, `analytics-service`, `prediction-service`, `ai-coach-service` |
| Muhammed Hany Hamdy | `muhammedhanii` | `dashboard-service`, `notification-service`, `study-group-service`, `administration-service` |

Ownership areas map to the 20 high-level functional requirements (FR-01–FR-20), four per member.

## Development workflow

- `main` is protected and holds reviewed, stable work.
- `develop` is the shared integration branch. Branch from it and open PRs into it.
- Branch naming follows `CONTRIBUTING.md` and the actual remote branches: `feature/<feature-slug>`.
  - Examples: `feature/student-profile`, `feature/academic-goal-management`, `feature/course-management`, `feature/smart-scheduling`.
  - One feature branch per feature. A branch corresponds to one entry in the README "Proposed Features" table.
  - Note: README also mentions `feature/<service>` (e.g. `feature/academic-service`); that is inconsistent with the actual repository convention. Use the `CONTRIBUTING.md` feature-slug form.
- Pull request flow:
  1. Confirm the related Jira task.
  2. Create a feature branch from `develop`.
  3. Make a focused change with tests or documentation as needed.
  4. Open a PR into `develop` and link the Jira story.
  5. Request review from another team member and address feedback.
  6. Merge after agreed checks pass.
- Use stable traceability IDs: `BR-XX`, `CR-XX`, `FR-XX`, `FR-XX.Y`, `NFR-XX`, `US-XX.Y`.
- Do not commit secrets, build artifacts, or unrelated changes. Use environment variables and keep local env files untracked.

## Jira

Jira Scrum is required for the course; Trello is not accepted.

| Field | Value |
|---|---|
| Site | https://aspirra.atlassian.net |
| Project name | Aspirra Project |
| Project key | `SCRUM` |
| Board | SCRUM board, board ID `1` |
| Jira type | Team-managed |
| Epic convention | One epic per high-level FR (FR-01–FR-20); epics `SCRUM-10`–`SCRUM-27` |
| Story convention | User stories created as `Task` with label `story`; linked to parent epic |
| Story points field | `customfield_10016` |
| Labels | `story`, `epic`, `FR-XX`, and service labels such as `academic-service` |
| Sprint 1 | `Sprint 1 — Foundation`, closed 2026-10-28, 2026-10-22 to 2026-11-04, 16 stories / 46 points |
| Suggested workflow | `Backlog → Ready → In Progress → In Review → Done` |

The product backlog lives in `docs/proposal/08-C8-product-backlog.md` (73 stories, US-01.1–US-20.6, total 225 points). Estimation and sprint plan live in `docs/proposal/09-C9-estimation-and-sprint-plan.md`.

## CLI tools

Both tools are installed and authenticated locally.

### GitHub: `gh`

Use `gh` for GitHub operations instead of manual API calls or web UI instructions.

```bash
gh auth status
gh issue list --repo AIU-SoftWave/Aspirra
gh issue view <number> --repo AIU-SoftWave/Aspirra
gh pr list --repo AIU-SoftWave/Aspirra
gh pr create --base develop --fill
gh pr view <number> --repo AIU-SoftWave/Aspirra
gh pr checks <number> --repo AIU-SoftWave/Aspirra
```

Prefer `--repo AIU-SoftWave/Aspirra` when not running from the repository root. Use `gh repo view`, `gh pr diff`, `gh pr review`, and related commands as needed.

### Jira: `acli`

Use `acli jira` for Jira operations. Do not attempt to edit Jira through the GitHub CLI.

```bash
acli jira auth status
acli jira workitem search --jql "project = SCRUM" --limit 50 --json
acli jira workitem view SCRUM-101
acli jira workitem create --summary "..." --project SCRUM --type Task --parent SCRUM-10 --label story,academic-service
acli jira workitem edit --key SCRUM-101 --summary "..."
acli jira workitem transition --key SCRUM-101 --status "In Progress"
acli jira sprint view --id 1
acli jira sprint list-workitems --id 1
acli jira board --help
```

Notes:
- Use `acli jira workitem create --generate-json` / `--from-json` when fields such as story points (`customfield_10016`) or ADF descriptions are needed.
- Use `acli jira workitem search --fields "issuetype,key,parent,assignee,priority,status,summary"` for compact output.
- Use `--json` for machine-readable output.
- `acli` cannot add project members non-interactively; do not promise that capability.

## Guardrails

- Do not commit, push, amend, force-push, or open/merge PRs unless explicitly asked.
- Do not run destructive Jira actions (delete, bulk edit, archive, sprint changes) unless explicitly asked and confirmed.
- Avoid `--yes` on bulk or destructive commands without approval.
- Do not modify another member's owned service, branch, or Jira stories without coordination.
- Never expose or commit credentials, tokens, API keys, or `.env` files.
- Keep generated plans editable and require student approval before consequential changes, per the product requirements.

## Verification

After setup or tooling changes, verify authentication and read access:

```bash
gh auth status
acli jira auth status
acli jira workitem search --jql "project = SCRUM" --limit 3
```
