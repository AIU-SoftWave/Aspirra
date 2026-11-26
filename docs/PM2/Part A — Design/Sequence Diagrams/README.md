# Sequence Diagrams

One sequence diagram per PM2 scenario. Show the ordered messages that implement the scenario, including failure branches that the tests cover.

## Required files

- `01-ahmed-complete-goal-sequence.puml` — Ahmed — `#56` — done
- `generate-goal-plan-sequence.puml` — Yomna — `#59`
- `complete-habit-sequence.puml` — Mostafa — `#60`
- `add-skill-sequence.puml` — Arwa — `#61` — done
- `deadline-reminders-sequence.puml` — Muhammed — `#62`

## Rules

- Participants must match the object and collaboration diagrams.
- Show the controller, provided interface, service, repository/client, and domain object.
- Show return messages that affect control flow.
- Use `alt`/`else` for valid and invalid branches.
- Name the guard with the corresponding test name when possible.
- Render and review the PNG before committing.
