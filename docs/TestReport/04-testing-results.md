# 4. Testing Results

Generated: 2026-11-26 on branch `develop`.

## Pass / fail summary

| Test project | Passed | Failed | Not run | Total |
|---|---|---|---|---|
| AcademicService.Tests | 37 | 0 | 0 | 37 |
| AssessmentService.Tests | 11 | 0 | 0 | 11 |
| CourseService.Tests | 10 | 0 | 0 | 10 |
| GoalPlannerService.Tests | 16 | 0 | 0 | 16 |
| HabitService.Tests | 13 | 0 | 0 | 13 |
| StudentService.Tests | 14 | 0 | 0 | 14 |
| **Total** | **101** | **0** | **0** | **101** |

## Coverage (line coverage per service, from coverlet/opencover)

| Service | Lines covered | Lines total | Coverage |
|---|---|---|---|
| AcademicService | 197 | 380 | 51.8% |
| AssessmentService | 216 | 579 | 37.3% |
| CourseService | 143 | 501 | 28.5% |
| GoalPlannerService | 289 | 740 | 39.1% |
| HabitService | 182 | 538 | 33.8% |
| StudentService | 219 | 618 | 35.4% |

Quality-gate coverage (authoritative) is reported by SonarCloud: https://sonarcloud.io/project/overview?id=AIU-SoftWave_Aspirra

## Pipeline screenshot

![GitHub Actions run](images/pipeline-green.png)

> Add `images/pipeline-green.png` — a screenshot of the green `CI - Build, Test and Push to GHCR` run on GitHub Actions after the PR is opened.

## Reproduce

```bash
dotnet test Aspirra.slnx -c Release --settings coverlet.runsettings --collect:"XPlat Code Coverage" --results-directory artifacts/coverage
```

Services without source code yet (14 of 20) have no executable tests; their test cases are marked **Planned**.
