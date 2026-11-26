# 2. Test Cases — Mostafa Khamis Abozead

Owner: `Mostafa-abozead`. Status/Actual Result are filled from the latest test run (`04-testing-results.md`).

## HabitService

| ID | Requirement | Description | Input | Expected Result | Actual Result | Status |
|---|---|---|---|---|---|---|
| TC-13.1-01 | FR-13.1 | Create habit | With valid data | Sets zero streak | As expected | Passed |
| TC-13.1-02 | FR-13.1 | Create habit | Missing name | Returns required field error | As expected | Passed |
| TC-13.1-03 | FR-13.1 | Create habit | Invalid frequency | Returns invalid frequency error | As expected | Passed |
| TC-13.2-01 | FR-13.2 | Complete habit | When no log exists | Adds log and saves | As expected | Passed |
| TC-13.2-02 | FR-13.2 | Complete habit | When log already completed | Does not add log | As expected | Passed |
| TC-13.2-03 | FR-13.2 | Complete habit | When habit missing | Returns not found and does not persist | As expected | Passed |
| TC-13.3-01 | FR-13.3 | Complete habit | First time | Sets streak to one | As expected | Passed |
| TC-13.3-02 | FR-13.3 | Complete habit | Consecutive days | Increments streak | As expected | Passed |
| TC-13.3-03 | FR-13.3 | Complete habit | Missed scheduled day | Resets streak to one | As expected | Passed |
| TC-13.3-04 | FR-13.3 | Complete habit | Same day twice | Does not increment streak | As expected | Passed |
| TC-13.3-05 | FR-13.3 | Get streaks | Valid input | Shows done today | As expected | Passed |
| TC-13.4-01 | FR-13.4 | Link habit to goal | Valid input | Stores goal id | As expected | Passed |
| TC-13.4-02 | FR-13.4 | Link habit to goal | Habit not found | Returns not found | As expected | Passed |

## AnalyticsService

| ID | Requirement | Description | Input | Expected Result | Actual Result | Status |
|---|---|---|---|---|---|---|
| TC-14.1-01 | FR-14.1 | Verify: The system shall calculate the remaining workload of each active goal from its unfinished  | Valid request | Requirement satisfied | — | Planned |
| TC-14.2-01 | FR-14.2 | Verify: The system shall calculate the remaining available study time of each active goal up to it | Valid request | Requirement satisfied | — | Planned |
| TC-14.3-01 | FR-14.3 | Verify: The system shall calculate the percentage of planned task duration completed for each acti | Valid request | Requirement satisfied | — | Planned |
| TC-14.4-01 | FR-14.4 | Verify: The system shall display the remaining workload, remaining available study time, and the p | Valid request | Requirement satisfied | — | Planned |
| TC-14.5-01 | FR-14.5 | Verify: The system shall mark an active goal as overloaded when its remaining workload exceeds its | Valid request | Requirement satisfied | — | Planned |

## PredictionService

| ID | Requirement | Description | Input | Expected Result | Actual Result | Status |
|---|---|---|---|---|---|---|
| TC-15.1-01 | FR-15.1 | Verify: The system shall estimate the likelihood of completing each active goal before its target  | Valid request | Requirement satisfied | — | Planned |
| TC-15.2-01 | FR-15.2 | Verify: The system shall generate an early warning for an active goal before its target date when  | Valid request | Requirement satisfied | — | Planned |
| TC-15.3-01 | FR-15.3 | Verify: The system shall identify which scheduled tasks of an active goal push its workload over i | Valid request | Requirement satisfied | — | Planned |
| TC-15.4-01 | FR-15.4 | Verify: The system shall suggest recovery actions for a goal with an active early warning so the s | Valid request | Requirement satisfied | — | Planned |

## AICoachService

| ID | Requirement | Description | Input | Expected Result | Actual Result | Status |
|---|---|---|---|---|---|---|
| TC-16.1-01 | FR-16.1 | Verify: The system shall provide personalized recommendations based on the student's goals, schedu | Valid request | Requirement satisfied | — | Planned |
| TC-16.2-01 | FR-16.2 | Verify: The system shall offer study advice tailored to upcoming deadlines and workload. | Valid request | Requirement satisfied | — | Planned |
| TC-16.3-01 | FR-16.3 | Verify: The system shall suggest recovery strategies when a student falls behind their plan. | Valid request | Requirement satisfied | — | Planned |
| TC-16.4-01 | FR-16.4 | Verify: The system shall provide motivational guidance to help students stay on track with their g | Valid request | Requirement satisfied | — | Planned |
