# Aspirra — Test Report

CSE454 Test Report assignment. Tools: xUnit + Moq (.NET equivalents of JUnit/Mockito) and GitHub Actions (`.github/workflows/ci.yml`).

| Deliverable | File |
|---|---|
| 1. Testing Requirements | [01-testing-requirements.md](01-testing-requirements.md) |
| 2. Test Cases (per owner) | [Ahmed](02-test-cases-01-ahmed.md) · [Yomna](02-test-cases-02-yomna.md) · [Arwa](02-test-cases-03-arwa.md) · [Mostafa](02-test-cases-04-mostafa.md) · [Muhammed](02-test-cases-05-muhammed.md) |
| 3. Testing Code | [03-testing-code.md](03-testing-code.md) → `tests/` |
| 4. Testing Results | [04-testing-results.md](04-testing-results.md) |

## Conventions
- Test case ID: `TC-<FR>-<nn>`, e.g. `TC-02.1-01`; test method: `TC_02_1_01_<Scenario>`.
- Each member owns the test-case file for their services and may refine Input/Expected wording.
- Services with no source yet (14 of 20) have **Planned** test cases; add real tests when the service lands, named with the same TC ID scheme.

## Regenerating
```bash
python3 docs/TestReport/tools/rename_tests.py   # prefixes new [Fact]/[Theory] methods with TC IDs (idempotent)
dotnet test Aspirra.slnx -c Release --settings coverlet.runsettings --collect:"XPlat Code Coverage" --results-directory artifacts/coverage --logger trx
python3 docs/TestReport/tools/generate_docs.py artifacts/coverage artifacts/coverage
```
