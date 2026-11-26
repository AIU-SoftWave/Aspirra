# Collaboration Diagrams

One collaboration (communication) diagram per PM2 scenario. Show the same objects and messages as the sequence diagram, but in a structural layout with numbered messages.

## Required files

- `01-ahmed-complete-goal-collaboration.puml` — Ahmed — `#57` — done
- `generate-goal-plan-collaboration.puml` — Yomna — `#59`
- `complete-habit-collaboration.puml` — Mostafa — `#60`
- `add-skill-collaboration.puml` — Arwa — `#61` — done
- `deadline-reminders-collaboration.puml` — Muhammed — `#62`

## Rules

- Number messages in the same order as the sequence diagram.
- Label each link with the message and interface used.
- Do not add objects that are absent from the sequence diagram.
- Render and review the PNG before committing.

## Message → verify → test (Complete Goal)

| Message | Moq `Verify` | Test |
| --- | --- | --- |
| `GetByIdAsync("goal-1001")` | `GetByIdAsync` setup returns the goal | `CompleteGoal_FromActive_ReturnsCompleted` |
| `Complete()` | `UpdateAsync(goal, ...)` called exactly once | `CompleteGoal_FromActive_PersistsExactlyOnce` |
| `UpdateAsync(goal)` | `Times.Once` | `CompleteGoal_FromActive_PersistsExactlyOnce` |
| Archived → `Complete()` | `UpdateAsync` `Times.Never` | `CompleteGoal_FromArchived_DoesNotPersist` |
| Missing goal → 404 | `UpdateAsync` `Times.Never` | `CompleteGoal_MissingGoal_ThrowsNotFound` |

## Message → verify → test (Add Skill)

| Message | Moq `Verify` | Test |
| --- | --- | --- |
| `1.2: GetByNameAsync("23101371", "SQL")` | `Times.Once` | `AddSkill_WithValidInput_SavesActiveSkillOnce` |
| `1.3 [no duplicate]: Activate()` | `AddAsync(It.Is<Skill>(s => s.Status == SkillStatus.Active))` | `AddSkill_WithValidInput_SavesActiveSkillOnce` |
| `1.4 [no duplicate]: AddAsync(sql)` | `Times.Once` | `AddSkill_WithValidInput_SavesActiveSkillOnce` |
| `1.5 [no duplicate]: SaveChangesAsync()` | `Times.Once` | `AddSkill_WithValidInput_SavesActiveSkillOnce` |
| Duplicate name → 409 | `AddAsync` / `SaveChangesAsync` `Times.Never` | `AddSkill_WhenDuplicateName_ReturnsDuplicateAndDoesNotPersist` |
| Missing / invalid field → 400 | `GetByNameAsync` `Times.Never` | `AddSkill_WithMissingOrInvalidField_IsRejectedBeforeRepository` |

