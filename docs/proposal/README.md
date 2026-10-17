# Project Proposal — Per-Issue Documents

Each graded deliverable of the proposal is written as **one Markdown file per GitHub issue** in this folder. This keeps work parallel and reviewable, and makes it easy to combine them into a single proposal document when needed.

## Naming rule

```
NN-CODE-slug.md
```

- `NN` — two-digit order for assembling the combined proposal (`01`–`13`).
- `CODE` — the issue code (`A1`, `B3`, `C8`, ...).
- `slug` — short kebab-case description.

## Files

| Order | File | Issue | Part |
|---|---|---|---|
| 01 | `01-A1-title-and-related-work.md` | [#1](https://github.com/AIU-SoftWave/Aspirra/issues/1) | A |
| 02 | `02-A2-team-table.md` | [#2](https://github.com/AIU-SoftWave/Aspirra/issues/2) | A |
| 03 | `03-B3-business-and-customer-requirements.md` | [#3](https://github.com/AIU-SoftWave/Aspirra/issues/3) | B |
| 04 | `04-B4-functional-requirements.md` | [#4](https://github.com/AIU-SoftWave/Aspirra/issues/4) | B |
| 05 | `05-B5-non-functional-requirements.md` | [#5](https://github.com/AIU-SoftWave/Aspirra/issues/5) | B |
| 06 | `06-B6-subsystems-interfaces-traceability.md` | [#6](https://github.com/AIU-SoftWave/Aspirra/issues/6) | B |
| 07 | `07-B7-use-case-diagram.md` | [#7](https://github.com/AIU-SoftWave/Aspirra/issues/7) | B |
| 08 | `08-C8-product-backlog.md` | [#8](https://github.com/AIU-SoftWave/Aspirra/issues/8) | C |
| 09 | `09-C9-estimation-and-sprint-plan.md` | [#9](https://github.com/AIU-SoftWave/Aspirra/issues/9) | C |
| 10 | `10-C10-pert-estimation-error.md` | [#10](https://github.com/AIU-SoftWave/Aspirra/issues/10) | C |
| 11 | `11-C11-jira-board.md` | [#11](https://github.com/AIU-SoftWave/Aspirra/issues/11) | C |
| 12 | `12-C12-github-repo.md` | [#12](https://github.com/AIU-SoftWave/Aspirra/issues/12) | C |
| 13 | `13-D13-figma-gui.md` | [#13](https://github.com/AIU-SoftWave/Aspirra/issues/13) | D |

## File format

Start every file from `_template.md`. Required header:

```markdown
# <CODE> — <Title>
> Issue: #<n> · Part: <A-D> · Owner: <name> · Reviewer: <name> · Status: Draft
```

Then a `## Content` section with your deliverable, followed by the `## Checklist`.

## How to contribute

1. Pick an issue from the table above.
2. Create a branch: `feature/proposal-<code>` (e.g. `feature/proposal-a1`).
3. Fill in your file (start from `_template.md`).
4. Open a Pull Request into `develop` and link the issue.
5. Update the issue's "Status" line and tick the checklist items.

## Combining the files

To assemble the final proposal, concatenate the numbered files in order:

```bash
cat docs/proposal/[0-9][0-9]-*.md > proposal.md
```
