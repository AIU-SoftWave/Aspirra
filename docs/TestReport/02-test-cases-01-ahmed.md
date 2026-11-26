# 2. Test Cases — Ahmed Mohamed Ahmed

Owner: `AbooSalh`. Status/Actual Result are filled from the latest test run (`04-testing-results.md`).

## StudentService

| ID | Requirement | Description | Input | Expected Result | Actual Result | Status |
|---|---|---|---|---|---|---|
| TC-01.1-01 | FR-01.1 | Create profile | With required fields | Creates profile | As expected | Passed |
| TC-01.1-02 | FR-01.1 | Create profile | Duplicate | Returns duplicate error | As expected | Passed |
| TC-01.2-01 | FR-01.2 | Update profile | Valid input | Changes fields | As expected | Passed |
| TC-01.2-02 | FR-01.2 | Update profile | Clearing required field | Keeps old values | As expected | Passed |
| TC-01.2-03 | FR-01.2 | Update profile | Valid input | Changes availability | As expected | Passed |
| TC-01.2-04 | FR-01.2 | Update profile | Not found | Returns not found | As expected | Passed |
| TC-01.3-01 | FR-01.3 | View profile | Valid input | Returns all fields | As expected | Passed |
| TC-01.3-02 | FR-01.3 | View profile | Optional field missing | Shows not set | As expected | Passed |
| TC-01.3-03 | FR-01.3 | Get profile | Not found | Returns not found | As expected | Passed |
| TC-01.3-04 | FR-01.3 | Get availability | Valid input | Returns slots | As expected | Passed |
| TC-01.3-05 | FR-01.3 | Get availability | Not found | Returns not found | As expected | Passed |
| TC-01.4-01 | FR-01.4 | Create profile | Missing academic level | Returns required field error | As expected | Passed |
| TC-01.4-02 | FR-01.4 | Create profile | End before start | Returns invalid time slot | As expected | Passed |
| TC-01.4-03 | FR-01.4 | Create profile | Missing major | Returns required field error | As expected | Passed |

## AcademicService

| ID | Requirement | Description | Input | Expected Result | Actual Result | Status |
|---|---|---|---|---|---|---|
| TC-02.1-01 | FR-02.1 | Health | Valid input | Returns healthy | As expected | Passed |
| TC-02.1-02 | FR-02.1 | Create goal | Valid input | Returns created goal and can be retrieved | As expected | Passed |
| TC-02.1-03 | FR-02.1 | Create goal | Missing title | Returns required field payload | As expected | Passed |
| TC-02.1-04 | FR-02.1 | Create goal | Without student id | Does not persist | As expected | Passed |
| TC-02.1-05 | FR-02.1 | Create goal | With valid data | Sets status active | As expected | Passed |
| TC-02.1-06 | FR-02.1 | Create goal | Missing title | Returns required field error | As expected | Passed |
| TC-02.1-07 | FR-02.1 | Create goal | Missing priority | Returns required field error | As expected | Passed |
| TC-02.1-08 | FR-02.1 | Create goal | Past target date | Returns invalid date error | As expected | Passed |
| TC-02.1-09 | FR-02.1 | Get goals | Valid input | Filters by student and status | As expected | Passed |
| TC-02.1-10 | FR-02.1 | Get goal | Not found | Returns not found | As expected | Passed |
| TC-02.2-01 | FR-02.2 | Update goal | Valid input | Changes fields | As expected | Passed |
| TC-02.2-02 | FR-02.2 | Update goal | Missing title | Keeps old values | As expected | Passed |
| TC-02.3-01 | FR-02.3 | Update goal | Past target date | Keeps old values | As expected | Passed |
| TC-02.4-01 | FR-02.4 | Complete goal | When archived | Returns invalid transition | As expected | Passed |
| TC-02.4-02 | FR-02.4 | Complete goal | When missing | Returns not found | As expected | Passed |
| TC-02.4-03 | FR-02.4 | Complete goal | From active | Persists exactly once | As expected | Passed |
| TC-02.4-04 | FR-02.4 | Complete goal | From archived | Does not persist | As expected | Passed |
| TC-02.4-05 | FR-02.4 | Complete goal | Valid input | Sets completed and completion date | As expected | Passed |
| TC-02.4-06 | FR-02.4 | Complete goal | Twice | Keeps first completion date | As expected | Passed |
| TC-02.4-07 | FR-02.4 | Complete goal | From active | Returns completed | As expected | Passed |
| TC-02.4-08 | FR-02.4 | Complete goal | From completed | Is idempotent | As expected | Passed |
| TC-02.4-09 | FR-02.4 | Complete goal | From archived | Throws invalid transition and does not update | As expected | Passed |
| TC-02.4-10 | FR-02.4 | Complete goal | Missing goal | Throws not found | As expected | Passed |
| TC-02.4-11 | FR-02.4 | Complete goal | Invalid transition | Returns bad request with code | As expected | Passed |
| TC-02.4-12 | FR-02.4 | Complete goal | Missing goal | Returns not found | As expected | Passed |
| TC-02.5-01 | FR-02.5 | Restore goal | From archived | Clears completion date and persists | As expected | Passed |
| TC-02.5-02 | FR-02.5 | Archive and restore goal | Valid input | Changes status | As expected | Passed |
| TC-02.5-03 | FR-02.5 | Archive goal | From active | Returns archived | As expected | Passed |
| TC-02.5-04 | FR-02.5 | Archive goal | From completed | Returns archived | As expected | Passed |
| TC-02.5-05 | FR-02.5 | Restore goal | From archived | Returns active | As expected | Passed |
| TC-02.5-06 | FR-02.5 | Restore goal | After archived completed goal | Clears completion date | As expected | Passed |
| TC-02.5-07 | FR-02.5 | Restore goal | From active | Throws invalid transition and does not update | As expected | Passed |
| TC-02.5-08 | FR-02.5 | Restore goal | From completed | Throws invalid transition and does not update | As expected | Passed |
| TC-02.5-09 | FR-02.5 | Archive goal | Missing goal | Throws not found | As expected | Passed |
| TC-02.5-10 | FR-02.5 | Restore goal | Missing goal | Throws not found | As expected | Passed |
| TC-02.5-11 | FR-02.5 | Restore goal | Invalid transition | Returns bad request with code | As expected | Passed |
| TC-02.6-01 | FR-02.6 | Delete goal | Valid input | Removes goal | As expected | Passed |

## CourseService

| ID | Requirement | Description | Input | Expected Result | Actual Result | Status |
|---|---|---|---|---|---|---|
| TC-03.1-01 | FR-03.1 | Add course | Missing required field | Returns required field error | As expected | Passed |
| TC-03.1-02 | FR-03.1 | Add course | Duplicate code and semester | Returns duplicate error | As expected | Passed |
| TC-03.1-03 | FR-03.1 | Add course | Same code different semester | Allowed | As expected | Passed |
| TC-03.1-04 | FR-03.1 | Get courses | Valid input | Filters by student | As expected | Passed |
| TC-03.2-01 | FR-03.2 | Add course | With valid data | Stores credits and workload | As expected | Passed |
| TC-03.3-01 | FR-03.3 | View course progress | Valid input | Separates completed and outstanding | As expected | Passed |
| TC-03.3-02 | FR-03.3 | View course progress | No assignments | Returns no assignments message | As expected | Passed |
| TC-03.3-03 | FR-03.3 | View course progress | Course not found | Returns not found | As expected | Passed |
| TC-03.4-01 | FR-03.4 | Update course | Valid input | Changes fields | As expected | Passed |
| TC-03.5-01 | FR-03.5 | Remove course | Valid input | Deletes course | As expected | Passed |

## AssessmentService

| ID | Requirement | Description | Input | Expected Result | Actual Result | Status |
|---|---|---|---|---|---|---|
| TC-04.1-01 | FR-04.1 | Add assignment | With required fields | Sets not started | As expected | Passed |
| TC-04.1-02 | FR-04.1 | Add assignment | Missing title | Returns required field error | As expected | Passed |
| TC-04.1-03 | FR-04.1 | Get assignments by course | Valid input | Returns expected shape | As expected | Passed |
| TC-04.2-01 | FR-04.2 | Add exam | With valid data | Is added to upcoming | As expected | Passed |
| TC-04.2-02 | FR-04.2 | Add exam | Past date | Returns invalid date error | As expected | Passed |
| TC-04.2-03 | FR-04.2 | Get assessments | Valid input | Filters by student | As expected | Passed |
| TC-04.3-01 | FR-04.3 | Set assignment status | To completed | Updates status | As expected | Passed |
| TC-04.3-02 | FR-04.3 | Set assignment status | Invalid status | Returns error | As expected | Passed |
| TC-04.3-03 | FR-04.3 | Set assignment status | Assignment not found | Returns not found | As expected | Passed |
| TC-04.4-01 | FR-04.4 | Set exam reminder | Valid input | Stores days before | As expected | Passed |
| TC-04.4-02 | FR-04.4 | Set exam reminder | Negative days | Returns invalid reminder | As expected | Passed |
