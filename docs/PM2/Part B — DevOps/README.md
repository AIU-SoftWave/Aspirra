# Part B — DevOps

PM2 DevOps evidence: Moq interaction tests, coverage, SonarQube Cloud quality gate, Jenkins pipeline, and a GitHub Actions comparison.

## Scope

| Task | Jira | GitHub | Deliverable |
| --- | --- | --- | --- |
| PM2-10 | `SCRUM-184` | `#63` | Moq tests and coverage in all test projects |
| PM2-11 | `SCRUM-185` | `#64` | SonarQube Cloud analysis and green quality gate |
| PM2-12 | `SCRUM-186` | `#65` | Jenkins pipeline and comparison with GitHub Actions |

## Files to add

```text
coverlet.runsettings             (added)
sonar-project.properties         (added)
Jenkinsfile                      (added)
devops/jenkins/Dockerfile        (added)
devops/jenkins/docker-compose.yml (added)
devops/jenkins/README.md         (added)
```

`.github/workflows/ci.yml` now collects coverage and runs SonarCloud when `SONAR_TOKEN` is configured.

## Test matrix

| Service | Test project | Interaction focus | Status |
| --- | --- | --- | --- |
| Academic | `tests/AcademicService.Tests/` | Goal transition guards, repository save, 400 mapping | Passing locally (37) |
| GoalPlanner | `tests/GoalPlannerService.Tests/` | `IAIProvider`, `IPlanDraftRepository`, `IGoalClient`, approve flow | Moq approve-flow tests added (16 passing) |
| Habit | `tests/HabitService.Tests/` | `IHabitRepository`, streak update, duplicate completion | Moq complete-flow tests added (13 passing) |
| Skill | `tests/SkillService.Tests/` | `ISkillRepository`, duplicate rejection | Pending |
| Notification | `tests/NotificationService.Tests/` | `IAssessment`, `IEmailGateway`, `INotificationRepository`, retry | Pending |

## Coverage

Use `coverlet.collector` and OpenCover output.

```bash
dotnet test Aspirra.slnx -c Release \
  --settings coverlet.runsettings \
  --collect:"XPlat Code Coverage" \
  --results-directory artifacts/coverage
```

Record:

- Test count and pass/fail.
- Line and branch coverage per service.
- Coverage artifact URL from GitHub Actions.

Do not claim a coverage percentage until the artifact exists.

Local result (2026-11-24): 101 tests passed; overall line coverage 37.1% across the six implemented services (AcademicService 51.8%, GoalPlannerService 39.1%, AssessmentService 37.3%, StudentService 35.4%, HabitService 33.8%, CourseService 28.5%). OpenCover output is under `artifacts/coverage/**/coverage.opencover.xml`.

## SonarQube Cloud

- Organization: `aboosalh`
- Project key: `AIU-SoftWave_Aspirra`
- Token: GitHub repository secret `SONAR_TOKEN` and Jenkins credential `sonar-token`

Local analysis:

```bash
dotnet tool install --global dotnet-sonarscanner
dotnet sonarscanner begin \
  /k:"AIU-SoftWave_Aspirra" \
  /o:"aboosalh" \
  /d:sonar.host.url="https://sonarcloud.io" \
  /d:sonar.token="$SONAR_TOKEN" \
  /d:sonar.cs.opencover.reportsPaths="artifacts/coverage/**/coverage.opencover.xml"
dotnet build Aspirra.slnx -c Release
dotnet test Aspirra.slnx -c Release --no-build
dotnet sonarscanner end /d:sonar.token="$SONAR_TOKEN"
```

Quality gate evidence (PR #69, 2026-11-25):

- [x] Quality gate status: **Passed**
- [x] Bugs / vulnerabilities / code smells: 0 bugs, 0 vulnerabilities, 2 code smells
- [x] Coverage on new code: 89.5% (threshold 80%)
- [x] Duplication: 0.0%
- [x] URL: https://sonarcloud.io/project/overview?id=AIU-SoftWave_Aspirra

> One new security issue (docker:S6506, non-HTTPS `curl` in `devops/jenkins/Dockerfile`) was found and fixed by adding `--proto '=https' --tlsv1.2`.

## Jenkins

Run Jenkins locally in Docker. The controller image needs .NET 10 SDK, JDK 21, Git, and `dotnet-sonarscanner`.

> Image build verified 2026-11-25: `aspirra-jenkins:latest` (1.63 GB) with .NET SDK 10.0.404 and
> `dotnet-sonarscanner` 12.1.0. See `devops/jenkins/README.md` for build notes (curl download + invariant globalization).

Pipeline stages:

1. Checkout.
2. `dotnet restore Aspirra.slnx`.
3. `dotnet build Aspirra.slnx -c Release --no-restore`.
4. `dotnet test Aspirra.slnx -c Release --no-build` with coverage.
5. SonarScanner begin/end when `sonar-token` exists.
6. Archive test results and coverage.
7. Publish the build result.

```bash
docker compose -f devops/jenkins/docker-compose.yml up -d --build
```

## GitHub Actions vs Jenkins

Fill this table from real runs, not from expectations.

| Aspect | GitHub Actions | Jenkins |
| --- | --- | --- |
| Trigger | Push/PR to `develop` | _pending_ |
| Runner | `ubuntu-latest` | Local Docker agent |
| SDK | .NET 10 | .NET 10 |
| Stages | Restore, build, test, coverage, Sonar, Docker/GHCR | _pending_ |
| Test result | _pending_ | _pending_ |
| Coverage | _pending_ | _pending_ |
| Sonar | _pending_ | _pending_ |
| Artifacts | _pending_ | _pending_ |
| Duration | _pending_ | _pending_ |
| Result | _pending_ | _pending_ |

## Evidence

| Evidence | Link | Status |
| --- | --- | --- |
| GitHub Actions run | _pending_ | Pending |
| Coverage artifact | _pending_ | Pending |
| SonarQube dashboard | _pending_ | Pending |
| Jenkins build | _pending_ | Pending |
| Jenkins console excerpt | _pending_ | Pending |

## Guardrails

- Never commit `SONAR_TOKEN`.
- Never present a local run as a CI run.
- If Sonar or Jenkins is unavailable, state the blocker and submit the verified fallback evidence.
