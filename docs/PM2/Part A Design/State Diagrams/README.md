# State Diagrams

One state diagram per PM2 scenario with meaningful lifecycle transitions. Invalid transitions must be explicit and must map to tests.

## Required files

- `01-goal-state.puml` — Ahmed — `#58`
- `plan-draft-state.puml` — Yomna — `#59`
- `habit-state.puml` — Mostafa — `#60`
- `skill-state.puml` — Arwa — `#61`
- `notification-state.puml` — Muhammed — `#62` — done

## Goal rules

Valid:

- `Active → Completed`
- `Completed → Completed` (idempotent)
- `Active → Archived`
- `Completed → Archived`
- `Archived → Active`

Invalid and rejected:

- `Archived → Completed`
- `Active → Restore`
- `Completed → Restore`
- missing goal (`404`)

## Rules

- Label transitions with events and guards.
- Mark invalid transitions and name the expected result.
- Every invalid transition has a test in `tests/AcademicService.Tests/` or the owning service's test project.
- Render and review the PNG before committing.
