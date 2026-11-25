# PM2 Submission Checklist

**Deadline:** 2026-11-25, 23:59  
**Repository:** https://github.com/AIU-SoftWave/Aspirra  
**Jira epic:** https://aspirra.atlassian.net/browse/SCRUM-174  
**GitHub parent:** https://github.com/AIU-SoftWave/Aspirra/issues/53

Do not submit until every item below is checked or explicitly marked not applicable with a reason.

## 1. Freeze the repository

- [ ] All PM2 PRs are merged into `develop`.
- [ ] `develop` is green locally and in GitHub Actions.
- [ ] No uncommitted changes remain.
- [ ] No secrets, `.env` files, `bin/`, `obj/`, or coverage dumps are tracked.
- [ ] The report references the exact commit being submitted.
- [ ] Tag the commit:

```bash
git checkout develop
git pull origin develop
git tag -a pm2-submission -m "CSE454 PM2 submission"
git push origin pm2-submission
```

- [ ] Record the tag and commit SHA in the report and in each member appendix.

## 2. Build the PDF

The report source is `docs/PM2/report.md`. The PDF is generated from HTML so that images and tables render consistently.

```bash
cd "docs/PM2"
npx --yes marked -i report.md -o report.html
libreoffice --headless --convert-to pdf --outdir . report.html
```

Verify:

- [ ] `report.pdf` exists and opens.
- [ ] All embedded diagram images are visible.
- [ ] Tables are not clipped.
- [ ] Page numbers or a table of contents are present if required by the course.
- [ ] The PDF filename is `report.pdf` and the cover shows the final date, tag, and commit SHA.

Fallback if `npx` is unavailable:

```bash
pip install --user markdown
python3 -m markdown report.md -f report.html
libreoffice --headless --convert-to pdf --outdir . report.html
```

Do not hand-edit the PDF. If content changes, edit `report.md`, regenerate HTML/PDF, and re-verify.

## 3. Consolidated package

The repository must contain:

```text
docs/PM2/
  README.md
  report.md
  report.pdf
  WORKFLOW.md
  SUBMISSION.md
  Part A — Design/
    Class Diagram/
    Object Diagrams/
    Sequence Diagrams/
    Collaboration Diagrams/
    State Diagrams/
  Part B — DevOps/
  Part C — Project Management/
  Part D — Traceability/
  members/
    Ahmed.md
    Yomna.md
    Arwa.md
    Mostafa.md
    Muhammed.md
```

- [ ] Every diagram directory has `.puml` and `.png` files.
- [ ] `Part D — Traceability/Design-Code-Mapping.md` covers every PM2 task.
- [ ] `members/*.md` states contribution, issue keys, evidence links, and self-assessment.
- [ ] `README.md` evidence table contains only real links.

## 4. Per-member submission

Each member submits the consolidated PDF plus a short appendix.

Create a private working folder:

```text
PM2-Aspirra-<GitHub-handle>/
  report.pdf
  appendix.md
  appendix.pdf
  LINKS.md
```

`LINKS.md` must contain:

```text
Name:
GitHub handle:
Jira tasks:
GitHub issues:
Repository:
Commit / tag: pm2-submission @ <sha>
Jira board:
GitHub Actions run:
SonarQube project:
Jenkins build:
```

Appendix contents (one page is enough):

- [ ] Name, student ID, GitHub handle.
- [ ] Owned PM2 tasks and story points.
- [ ] What you personally designed, implemented, or tested.
- [ ] Evidence links for your artifacts.
- [ ] What you reviewed for another member.
- [ ] Any blocker or scope reduction, stated honestly.

## 5. Individual verification

Run these before submitting:

```bash
gh auth status
acli jira auth status
dotnet test Aspirra.slnx -c Release
gh issue list --repo AIU-SoftWave/Aspirra --state open --limit 30
gh run list --repo AIU-SoftWave/Aspirra --limit 5
```

- [ ] All 15 GitHub sub-issues are closed or explicitly deferred with a comment.
- [ ] All 15 Jira tasks are **Done** or explicitly deferred with a reason.
- [ ] Sprint 2 burndown screenshot is captured.
- [ ] Sprint 1 burndown screenshot is captured.
- [ ] SonarQube quality gate is green.
- [ ] Jenkins build is green.
- [ ] GitHub Actions run is green.
- [ ] The report's numbers match Jira and the test output.

## 6. Submission and final check

- [ ] Submit the PDF and appendix through the course submission channel.
- [ ] Submit before **2026-11-25 23:59**; do not rely on a last-minute upload.
- [ ] Keep the repository public and the tag pushed.
- [ ] Keep Jira and GitHub open for grader inspection.
- [ ] Save the submission confirmation and timestamp.
- [ ] Send the confirmation to the team leader.

## 7. Last-resort triage

If a required artifact is incomplete at the deadline:

1. State exactly what is incomplete in the report and appendix.
2. Link the issue and the blocker.
3. Do not fabricate test, coverage, Sonar, Jenkins, or Jira evidence.
4. Submit the best verified version and preserve the evidence trail.
