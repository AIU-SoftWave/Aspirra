# 2. Test Cases — Yomna Mohamed Fekry

Owner: `YomnaElmenawy`. Status/Actual Result are filled from the latest test run (`04-testing-results.md`).

## GoalPlannerService

| ID | Requirement | Description | Input | Expected Result | Actual Result | Status |
|---|---|---|---|---|---|---|
| TC-05.1-01 | FR-05.1 | Generate plan | Empty description | Returns required field error | As expected | Passed |
| TC-05.2-01 | FR-05.2 | Generate plan | With description | Creates draft with milestones and tasks | As expected | Passed |
| TC-05.2-02 | FR-05.2 | Generate plan | Ai unavailable | Returns ai unavailable error | As expected | Passed |
| TC-05.2-03 | FR-05.2 | Generate plan | Invalid structure | Returns plan invalid error | As expected | Passed |
| TC-05.3-01 | FR-05.3 | Discard | Valid input | Deletes draft | As expected | Passed |
| TC-05.3-02 | FR-05.3 | Get tasks | Valid input | Returns only approved plan tasks | As expected | Passed |
| TC-05.3-03 | FR-05.3 | Get draft | Missing | Returns not found | As expected | Passed |
| TC-05.3-04 | FR-05.3 | Discard | Missing | Returns not found | As expected | Passed |
| TC-05.4-01 | FR-05.4 | Update draft | Valid input | Edits milestones and tasks | As expected | Passed |
| TC-05.5-01 | FR-05.5 | Update draft | Approved plan | Returns not editable | As expected | Passed |
| TC-05.6-01 | FR-05.6 | Approve | With existing goal | Approves and saves once | As expected | Passed |
| TC-05.6-02 | FR-05.6 | Approve | When goal does not exist | Returns goal not found and does not save | As expected | Passed |
| TC-05.6-03 | FR-05.6 | Approve | When draft missing | Returns not found | As expected | Passed |
| TC-05.6-04 | FR-05.6 | Approve | Valid draft | Sets approved | As expected | Passed |
| TC-05.6-05 | FR-05.6 | Approve | Goal missing | Returns goal not found | As expected | Passed |
| TC-05.6-06 | FR-05.6 | Approve | Already approved | Is idempotent | As expected | Passed |

## StudyPlannerService

| ID | Requirement | Description | Input | Expected Result | Actual Result | Status |
|---|---|---|---|---|---|---|
| TC-06.1-01 | FR-06.1 | Verify: The system shall generate a personalized study plan based on courses, deadlines, and stude | Valid request | Requirement satisfied | — | Planned |
| TC-06.2-01 | FR-06.2 | Verify: The system shall distribute study tasks across available time slots while respecting prior | Valid request | Requirement satisfied | — | Planned |
| TC-06.3-01 | FR-06.3 | Verify: The system shall allow a student to regenerate the study plan after an availability or dea | Valid request | Requirement satisfied | — | Planned |
| TC-06.4-01 | FR-06.4 | Verify: The system shall reject an AI-generated study plan that does not match the expected plan s | Valid request | Requirement satisfied | — | Planned |
| TC-06.5-01 | FR-06.5 | Verify: The system shall retain the previously saved study plan when an AI-generated plan is rejec | Valid request | Requirement satisfied | — | Planned |

## SchedulingService

| ID | Requirement | Description | Input | Expected Result | Actual Result | Status |
|---|---|---|---|---|---|---|
| TC-07.1-01 | FR-07.1 | Verify: The system shall schedule tasks based on deadlines, priorities, workload, availability, an | Valid request | Requirement satisfied | — | Planned |
| TC-07.2-01 | FR-07.2 | Verify: The system shall detect scheduling conflicts between tasks and overlapping time slots. | Valid request | Requirement satisfied | — | Planned |
| TC-07.3-01 | FR-07.3 | Verify: The system shall suggest alternative time slots when scheduling conflicts occur. | Valid request | Requirement satisfied | — | Planned |
| TC-07.4-01 | FR-07.4 | Verify: The system shall allow a student to manually adjust scheduled tasks and times. | Valid request | Requirement satisfied | — | Planned |
| TC-07.5-01 | FR-07.5 | Verify: The system shall allow a student to mark a scheduled task as completed or not completed. | Valid request | Requirement satisfied | — | Planned |

## AdaptationService

| ID | Requirement | Description | Input | Expected Result | Actual Result | Status |
|---|---|---|---|---|---|---|
| TC-08.1-01 | FR-08.1 | Verify: The system shall detect when a student is falling behind on tasks or deadlines. | Valid request | Requirement satisfied | — | Planned |
| TC-08.2-01 | FR-08.2 | Verify: The system shall recommend rescheduling tasks when the student is behind schedule. | Valid request | Requirement satisfied | — | Planned |
| TC-08.3-01 | FR-08.3 | Verify: The system shall recommend reprioritizing tasks when the student is behind schedule. | Valid request | Requirement satisfied | — | Planned |
| TC-08.4-01 | FR-08.4 | Verify: The system shall require student approval before applying adaptive plan changes. | Valid request | Requirement satisfied | — | Planned |
| TC-08.5-01 | FR-08.5 | Verify: The system shall allow a student to accept adaptive planning recommendations. | Valid request | Requirement satisfied | — | Planned |
| TC-08.6-01 | FR-08.6 | Verify: The system shall allow a student to reject adaptive planning recommendations. | Valid request | Requirement satisfied | — | Planned |
| TC-08.7-01 | FR-08.7 | Verify: The system shall allow a student to modify adaptive planning recommendations. | Valid request | Requirement satisfied | — | Planned |
