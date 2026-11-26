# 1. Testing Requirements

Requirements (FRs) under test per service and what must be true for each one to pass. Source: `docs/proposal/04-B4-functional-requirements.md`. Coverage column shows the number of test cases mapped to the FR.

## Ahmed Mohamed Ahmed (`AbooSalh`)

### StudentService — FR-01 Manage Student Profile
Implementation: implemented, automated tests exist

| FR | Requirement | Must be true to pass | Test cases |
|---|---|---|---|
| FR-01.1 | The system shall allow a student to create a profile with academic level, major, interests, skills, career interests, and availability. | All mapped test cases pass (TC-01.1-01, TC-01.1-02); valid input succeeds and invalid input is rejected with the documented error. | 2 |
| FR-01.2 | The system shall allow a student to update profile information including availability and career interests. | All mapped test cases pass (TC-01.2-01, TC-01.2-02, TC-01.2-03, TC-01.2-04); valid input succeeds and invalid input is rejected with the documented error. | 4 |
| FR-01.3 | The system shall allow a student to view their complete profile details. | All mapped test cases pass (TC-01.3-01, TC-01.3-02, TC-01.3-03, TC-01.3-04, TC-01.3-05); valid input succeeds and invalid input is rejected with the documented error. | 5 |
| FR-01.4 | The system shall validate required profile fields before saving profile data. | All mapped test cases pass (TC-01.4-01, TC-01.4-02, TC-01.4-03); valid input succeeds and invalid input is rejected with the documented error. | 3 |

### AcademicService — FR-02 Manage Academic Goals
Implementation: implemented, automated tests exist

| FR | Requirement | Must be true to pass | Test cases |
|---|---|---|---|
| FR-02.1 | The system shall allow a student to create an academic goal with a title, description, target date, and priority. | All mapped test cases pass (TC-02.1-01, TC-02.1-02, TC-02.1-03, TC-02.1-04, TC-02.1-05, TC-02.1-06, TC-02.1-07, TC-02.1-08, TC-02.1-09, TC-02.1-10); valid input succeeds and invalid input is rejected with the documented error. | 10 |
| FR-02.2 | The system shall allow a student to update the details of an existing academic goal. | All mapped test cases pass (TC-02.2-01, TC-02.2-02); valid input succeeds and invalid input is rejected with the documented error. | 2 |
| FR-02.3 | The system shall allow a student to update the target date of an existing academic goal. | All mapped test cases pass (TC-02.3-01); valid input succeeds and invalid input is rejected with the documented error. | 1 |
| FR-02.4 | The system shall allow a student to mark an academic goal as completed. | All mapped test cases pass (TC-02.4-01, TC-02.4-02, TC-02.4-03, TC-02.4-04, TC-02.4-05, TC-02.4-06, TC-02.4-07, TC-02.4-08, TC-02.4-09, TC-02.4-10, TC-02.4-11, TC-02.4-12); valid input succeeds and invalid input is rejected with the documented error. | 12 |
| FR-02.5 | The system shall allow a student to archive an academic goal. | All mapped test cases pass (TC-02.5-01, TC-02.5-02, TC-02.5-03, TC-02.5-04, TC-02.5-05, TC-02.5-06, TC-02.5-07, TC-02.5-08, TC-02.5-09, TC-02.5-10, TC-02.5-11); valid input succeeds and invalid input is rejected with the documented error. | 11 |
| FR-02.6 | The system shall allow a student to delete an academic goal they created. | All mapped test cases pass (TC-02.6-01); valid input succeeds and invalid input is rejected with the documented error. | 1 |

### CourseService — FR-03 Manage Courses
Implementation: implemented, automated tests exist

| FR | Requirement | Must be true to pass | Test cases |
|---|---|---|---|
| FR-03.1 | The system shall allow a student to add a course with course name, course code, instructor, and semester details. | All mapped test cases pass (TC-03.1-01, TC-03.1-02, TC-03.1-03, TC-03.1-04); valid input succeeds and invalid input is rejected with the documented error. | 4 |
| FR-03.2 | The system shall allow a student to record the credits and the weekly workload of each course. | All mapped test cases pass (TC-03.2-01); valid input succeeds and invalid input is rejected with the documented error. | 1 |
| FR-03.3 | The system shall allow a student to view the progress of a course, including its completed and outstanding assignments. | All mapped test cases pass (TC-03.3-01, TC-03.3-02, TC-03.3-03); valid input succeeds and invalid input is rejected with the documented error. | 3 |
| FR-03.4 | The system shall allow a student to update course information. | All mapped test cases pass (TC-03.4-01); valid input succeeds and invalid input is rejected with the documented error. | 1 |
| FR-03.5 | The system shall allow a student to remove a course. | All mapped test cases pass (TC-03.5-01); valid input succeeds and invalid input is rejected with the documented error. | 1 |

### AssessmentService — FR-04 Manage Assignments and Exams
Implementation: implemented, automated tests exist

| FR | Requirement | Must be true to pass | Test cases |
|---|---|---|---|
| FR-04.1 | The system shall allow a student to add an assignment with title, course, due date, priority, and description. | All mapped test cases pass (TC-04.1-01, TC-04.1-02, TC-04.1-03); valid input succeeds and invalid input is rejected with the documented error. | 3 |
| FR-04.2 | The system shall allow a student to add an exam with title, course, exam date, time, and location. | All mapped test cases pass (TC-04.2-01, TC-04.2-02, TC-04.2-03); valid input succeeds and invalid input is rejected with the documented error. | 3 |
| FR-04.3 | The system shall allow a student to track assignment status as not started, in progress, or completed. | All mapped test cases pass (TC-04.3-01, TC-04.3-02, TC-04.3-03); valid input succeeds and invalid input is rejected with the documented error. | 3 |
| FR-04.4 | The system shall allow a student to set preparation reminders for upcoming exams. | All mapped test cases pass (TC-04.4-01, TC-04.4-02); valid input succeeds and invalid input is rejected with the documented error. | 2 |

## Yomna Mohamed Fekry (`YomnaElmenawy`)

### GoalPlannerService — FR-05 Plan Goals with AI
Implementation: implemented, automated tests exist

| FR | Requirement | Must be true to pass | Test cases |
|---|---|---|---|
| FR-05.1 | The system shall allow a student to input a goal in natural language for AI processing. | All mapped test cases pass (TC-05.1-01); valid input succeeds and invalid input is rejected with the documented error. | 1 |
| FR-05.2 | The system shall generate a structured goal roadmap with milestones, tasks, and deadlines from natural language input. | All mapped test cases pass (TC-05.2-01, TC-05.2-02, TC-05.2-03); valid input succeeds and invalid input is rejected with the documented error. | 3 |
| FR-05.3 | The system shall allow a student to review AI-generated milestones and tasks before saving. | All mapped test cases pass (TC-05.3-01, TC-05.3-02, TC-05.3-03, TC-05.3-04); valid input succeeds and invalid input is rejected with the documented error. | 4 |
| FR-05.4 | The system shall allow a student to edit AI-generated milestones before saving. | All mapped test cases pass (TC-05.4-01); valid input succeeds and invalid input is rejected with the documented error. | 1 |
| FR-05.5 | The system shall allow a student to edit AI-generated tasks before saving. | All mapped test cases pass (TC-05.5-01); valid input succeeds and invalid input is rejected with the documented error. | 1 |
| FR-05.6 | The system shall require student approval before applying AI-generated goals to their plan. | All mapped test cases pass (TC-05.6-01, TC-05.6-02, TC-05.6-03, TC-05.6-04, TC-05.6-05, TC-05.6-06); valid input succeeds and invalid input is rejected with the documented error. | 6 |

### StudyPlannerService — FR-06 Generate Study Plans
Implementation: **not implemented yet** — test cases are planned drafts

| FR | Requirement | Must be true to pass | Test cases |
|---|---|---|---|
| FR-06.1 | The system shall generate a personalized study plan based on courses, deadlines, and student availability. | Behaviour is verified by at least one passing test: valid input succeeds, invalid input is rejected with a clear error. (Planned: service not implemented yet.) | planned |
| FR-06.2 | The system shall distribute study tasks across available time slots while respecting priorities. | Behaviour is verified by at least one passing test: valid input succeeds, invalid input is rejected with a clear error. (Planned: service not implemented yet.) | planned |
| FR-06.3 | The system shall allow a student to regenerate the study plan after an availability or deadline change. | Behaviour is verified by at least one passing test: valid input succeeds, invalid input is rejected with a clear error. (Planned: service not implemented yet.) | planned |
| FR-06.4 | The system shall reject an AI-generated study plan that does not match the expected plan structure. | Behaviour is verified by at least one passing test: valid input succeeds, invalid input is rejected with a clear error. (Planned: service not implemented yet.) | planned |
| FR-06.5 | The system shall retain the previously saved study plan when an AI-generated plan is rejected. | Behaviour is verified by at least one passing test: valid input succeeds, invalid input is rejected with a clear error. (Planned: service not implemented yet.) | planned |

### SchedulingService — FR-07 Enable Smart Scheduling
Implementation: **not implemented yet** — test cases are planned drafts

| FR | Requirement | Must be true to pass | Test cases |
|---|---|---|---|
| FR-07.1 | The system shall schedule tasks based on deadlines, priorities, workload, availability, and dependencies. | Behaviour is verified by at least one passing test: valid input succeeds, invalid input is rejected with a clear error. (Planned: service not implemented yet.) | planned |
| FR-07.2 | The system shall detect scheduling conflicts between tasks and overlapping time slots. | Behaviour is verified by at least one passing test: valid input succeeds, invalid input is rejected with a clear error. (Planned: service not implemented yet.) | planned |
| FR-07.3 | The system shall suggest alternative time slots when scheduling conflicts occur. | Behaviour is verified by at least one passing test: valid input succeeds, invalid input is rejected with a clear error. (Planned: service not implemented yet.) | planned |
| FR-07.4 | The system shall allow a student to manually adjust scheduled tasks and times. | Behaviour is verified by at least one passing test: valid input succeeds, invalid input is rejected with a clear error. (Planned: service not implemented yet.) | planned |
| FR-07.5 | The system shall allow a student to mark a scheduled task as completed or not completed. | Behaviour is verified by at least one passing test: valid input succeeds, invalid input is rejected with a clear error. (Planned: service not implemented yet.) | planned |

### AdaptationService — FR-08 Enable Adaptive Planning
Implementation: **not implemented yet** — test cases are planned drafts

| FR | Requirement | Must be true to pass | Test cases |
|---|---|---|---|
| FR-08.1 | The system shall detect when a student is falling behind on tasks or deadlines. | Behaviour is verified by at least one passing test: valid input succeeds, invalid input is rejected with a clear error. (Planned: service not implemented yet.) | planned |
| FR-08.2 | The system shall recommend rescheduling tasks when the student is behind schedule. | Behaviour is verified by at least one passing test: valid input succeeds, invalid input is rejected with a clear error. (Planned: service not implemented yet.) | planned |
| FR-08.3 | The system shall recommend reprioritizing tasks when the student is behind schedule. | Behaviour is verified by at least one passing test: valid input succeeds, invalid input is rejected with a clear error. (Planned: service not implemented yet.) | planned |
| FR-08.4 | The system shall require student approval before applying adaptive plan changes. | Behaviour is verified by at least one passing test: valid input succeeds, invalid input is rejected with a clear error. (Planned: service not implemented yet.) | planned |
| FR-08.5 | The system shall allow a student to accept adaptive planning recommendations. | Behaviour is verified by at least one passing test: valid input succeeds, invalid input is rejected with a clear error. (Planned: service not implemented yet.) | planned |
| FR-08.6 | The system shall allow a student to reject adaptive planning recommendations. | Behaviour is verified by at least one passing test: valid input succeeds, invalid input is rejected with a clear error. (Planned: service not implemented yet.) | planned |
| FR-08.7 | The system shall allow a student to modify adaptive planning recommendations. | Behaviour is verified by at least one passing test: valid input succeeds, invalid input is rejected with a clear error. (Planned: service not implemented yet.) | planned |

## Arwa Mohey Hamdy (`ArwaMohey`)

### CareerService — FR-09 Manage Career Roadmaps
Implementation: **not implemented yet** — test cases are planned drafts

| FR | Requirement | Must be true to pass | Test cases |
|---|---|---|---|
| FR-09.1 | The system shall allow a student to select a target career from a predefined list. | Behaviour is verified by at least one passing test: valid input succeeds, invalid input is rejected with a clear error. (Planned: service not implemented yet.) | planned |
| FR-09.2 | The system shall allow a student to enter a custom target career. | Behaviour is verified by at least one passing test: valid input succeeds, invalid input is rejected with a clear error. (Planned: service not implemented yet.) | planned |
| FR-09.3 | The system shall generate a career roadmap with required skills, learning steps, and milestones. | Behaviour is verified by at least one passing test: valid input succeeds, invalid input is rejected with a clear error. (Planned: service not implemented yet.) | planned |
| FR-09.4 | The system shall allow a student to track progress toward completing career roadmap milestones. | Behaviour is verified by at least one passing test: valid input succeeds, invalid input is rejected with a clear error. (Planned: service not implemented yet.) | planned |
| FR-09.5 | The system shall allow a student to update their target career. | Behaviour is verified by at least one passing test: valid input succeeds, invalid input is rejected with a clear error. (Planned: service not implemented yet.) | planned |
| FR-09.6 | The system shall regenerate the career roadmap after the target career is updated. | Behaviour is verified by at least one passing test: valid input succeeds, invalid input is rejected with a clear error. (Planned: service not implemented yet.) | planned |

### SkillService — FR-10 Track Skills
Implementation: **not implemented yet** — test cases are planned drafts

| FR | Requirement | Must be true to pass | Test cases |
|---|---|---|---|
| FR-10.1 | The system shall allow a student to add technical and soft skills with current skill level. | Behaviour is verified by at least one passing test: valid input succeeds, invalid input is rejected with a clear error. (Planned: service not implemented yet.) | planned |
| FR-10.2 | The system shall compare a student's skills against the required skills of their target career. | Behaviour is verified by at least one passing test: valid input succeeds, invalid input is rejected with a clear error. (Planned: service not implemented yet.) | planned |
| FR-10.3 | The system shall identify skill gaps based on career requirements. | Behaviour is verified by at least one passing test: valid input succeeds, invalid input is rejected with a clear error. (Planned: service not implemented yet.) | planned |
| FR-10.4 | The system shall allow a student to update skill levels as they progress. | Behaviour is verified by at least one passing test: valid input succeeds, invalid input is rejected with a clear error. (Planned: service not implemented yet.) | planned |

### PortfolioService — FR-11 Manage Projects and Portfolios
Implementation: **not implemented yet** — test cases are planned drafts

| FR | Requirement | Must be true to pass | Test cases |
|---|---|---|---|
| FR-11.1 | The system shall allow a student to add a project with title, description, technologies, and completion status. | Behaviour is verified by at least one passing test: valid input succeeds, invalid input is rejected with a clear error. (Planned: service not implemented yet.) | planned |
| FR-11.2 | The system shall allow a student to link projects to the skills they demonstrate. | Behaviour is verified by at least one passing test: valid input succeeds, invalid input is rejected with a clear error. (Planned: service not implemented yet.) | planned |
| FR-11.3 | The system shall generate a portfolio view of the student's completed projects. | Behaviour is verified by at least one passing test: valid input succeeds, invalid input is rejected with a clear error. (Planned: service not implemented yet.) | planned |
| FR-11.4 | The system shall allow a student to add project links and documentation to their portfolio. | Behaviour is verified by at least one passing test: valid input succeeds, invalid input is rejected with a clear error. (Planned: service not implemented yet.) | planned |

### JobPreparationService — FR-12 Manage Internship and Job Preparation
Implementation: **not implemented yet** — test cases are planned drafts

| FR | Requirement | Must be true to pass | Test cases |
|---|---|---|---|
| FR-12.1 | The system shall allow a student to track internship and job applications with company, position, status, and deadline. | Behaviour is verified by at least one passing test: valid input succeeds, invalid input is rejected with a clear error. (Planned: service not implemented yet.) | planned |
| FR-12.2 | The system shall allow a student to track interview preparation tasks for each application. | Behaviour is verified by at least one passing test: valid input succeeds, invalid input is rejected with a clear error. (Planned: service not implemented yet.) | planned |
| FR-12.3 | The system shall allow a student to track CV updates related to applications. | Behaviour is verified by at least one passing test: valid input succeeds, invalid input is rejected with a clear error. (Planned: service not implemented yet.) | planned |
| FR-12.4 | The system shall allow a student to track portfolio updates related to applications. | Behaviour is verified by at least one passing test: valid input succeeds, invalid input is rejected with a clear error. (Planned: service not implemented yet.) | planned |
| FR-12.5 | The system shall allow a student to record the outcome of an application. | Behaviour is verified by at least one passing test: valid input succeeds, invalid input is rejected with a clear error. (Planned: service not implemented yet.) | planned |
| FR-12.6 | The system shall allow a student to record feedback received for an application. | Behaviour is verified by at least one passing test: valid input succeeds, invalid input is rejected with a clear error. (Planned: service not implemented yet.) | planned |

## Mostafa Khamis Abozead (`Mostafa-abozead`)

### HabitService — FR-13 Manage Habits
Implementation: implemented, automated tests exist

| FR | Requirement | Must be true to pass | Test cases |
|---|---|---|---|
| FR-13.1 | The system shall allow a student to create a habit with name, frequency, goal, and reminder settings. | All mapped test cases pass (TC-13.1-01, TC-13.1-02, TC-13.1-03); valid input succeeds and invalid input is rejected with the documented error. | 3 |
| FR-13.2 | The system shall allow a student to mark a habit as completed for a scheduled day. | All mapped test cases pass (TC-13.2-01, TC-13.2-02, TC-13.2-03); valid input succeeds and invalid input is rejected with the documented error. | 3 |
| FR-13.3 | The system shall track habit streaks based on consecutive completions. | All mapped test cases pass (TC-13.3-01, TC-13.3-02, TC-13.3-03, TC-13.3-04, TC-13.3-05); valid input succeeds and invalid input is rejected with the documented error. | 5 |
| FR-13.4 | The system shall allow a student to link habits to academic goals. | All mapped test cases pass (TC-13.4-01, TC-13.4-02); valid input succeeds and invalid input is rejected with the documented error. | 2 |

### AnalyticsService — FR-14 Analyze Study Workload
Implementation: **not implemented yet** — test cases are planned drafts

| FR | Requirement | Must be true to pass | Test cases |
|---|---|---|---|
| FR-14.1 | The system shall calculate the remaining workload of each active goal from its unfinished scheduled tasks and their scheduled durations. | Behaviour is verified by at least one passing test: valid input succeeds, invalid input is rejected with a clear error. (Planned: service not implemented yet.) | planned |
| FR-14.2 | The system shall calculate the remaining available study time of each active goal up to its target date, using the student's availability. | Behaviour is verified by at least one passing test: valid input succeeds, invalid input is rejected with a clear error. (Planned: service not implemented yet.) | planned |
| FR-14.3 | The system shall calculate the percentage of planned task duration completed for each active goal. | Behaviour is verified by at least one passing test: valid input succeeds, invalid input is rejected with a clear error. (Planned: service not implemented yet.) | planned |
| FR-14.4 | The system shall display the remaining workload, remaining available study time, and the percentage of planned task duration completed for each active goal. | Behaviour is verified by at least one passing test: valid input succeeds, invalid input is rejected with a clear error. (Planned: service not implemented yet.) | planned |
| FR-14.5 | The system shall mark an active goal as overloaded when its remaining workload exceeds its remaining available study time. | Behaviour is verified by at least one passing test: valid input succeeds, invalid input is rejected with a clear error. (Planned: service not implemented yet.) | planned |

### PredictionService — FR-15 Predict and Warn Early
Implementation: **not implemented yet** — test cases are planned drafts

| FR | Requirement | Must be true to pass | Test cases |
|---|---|---|---|
| FR-15.1 | The system shall estimate the likelihood of completing each active goal before its target date from its percentage of planned task duration completed and its remaining available study time. | Behaviour is verified by at least one passing test: valid input succeeds, invalid input is rejected with a clear error. (Planned: service not implemented yet.) | planned |
| FR-15.2 | The system shall generate an early warning for an active goal before its target date when the goal is marked as overloaded. | Behaviour is verified by at least one passing test: valid input succeeds, invalid input is rejected with a clear error. (Planned: service not implemented yet.) | planned |
| FR-15.3 | The system shall identify which scheduled tasks of an active goal push its workload over its available study time. | Behaviour is verified by at least one passing test: valid input succeeds, invalid input is rejected with a clear error. (Planned: service not implemented yet.) | planned |
| FR-15.4 | The system shall suggest recovery actions for a goal with an active early warning so the student can recover before the target date. | Behaviour is verified by at least one passing test: valid input succeeds, invalid input is rejected with a clear error. (Planned: service not implemented yet.) | planned |

### AICoachService — FR-16 Coach Students with AI
Implementation: **not implemented yet** — test cases are planned drafts

| FR | Requirement | Must be true to pass | Test cases |
|---|---|---|---|
| FR-16.1 | The system shall provide personalized recommendations based on the student's goals, scheduled tasks, habits, and workload progress. | Behaviour is verified by at least one passing test: valid input succeeds, invalid input is rejected with a clear error. (Planned: service not implemented yet.) | planned |
| FR-16.2 | The system shall offer study advice tailored to upcoming deadlines and workload. | Behaviour is verified by at least one passing test: valid input succeeds, invalid input is rejected with a clear error. (Planned: service not implemented yet.) | planned |
| FR-16.3 | The system shall suggest recovery strategies when a student falls behind their plan. | Behaviour is verified by at least one passing test: valid input succeeds, invalid input is rejected with a clear error. (Planned: service not implemented yet.) | planned |
| FR-16.4 | The system shall provide motivational guidance to help students stay on track with their goals. | Behaviour is verified by at least one passing test: valid input succeeds, invalid input is rejected with a clear error. (Planned: service not implemented yet.) | planned |

## Muhammed Hany Hamdy (`muhammedhanii`)

### DashboardService — FR-17 Display Student Dashboard
Implementation: **not implemented yet** — test cases are planned drafts

| FR | Requirement | Must be true to pass | Test cases |
|---|---|---|---|
| FR-17.1 | The system shall display today's tasks and upcoming deadlines on the student dashboard. | Behaviour is verified by at least one passing test: valid input succeeds, invalid input is rejected with a clear error. (Planned: service not implemented yet.) | planned |
| FR-17.2 | The system shall display the current status of each goal on the dashboard. | Behaviour is verified by at least one passing test: valid input succeeds, invalid input is rejected with a clear error. (Planned: service not implemented yet.) | planned |
| FR-17.3 | The system shall display the completion percentage of each goal on the dashboard. | Behaviour is verified by at least one passing test: valid input succeeds, invalid input is rejected with a clear error. (Planned: service not implemented yet.) | planned |
| FR-17.4 | The system shall display habit streaks and completion status on the dashboard. | Behaviour is verified by at least one passing test: valid input succeeds, invalid input is rejected with a clear error. (Planned: service not implemented yet.) | planned |
| FR-17.5 | The system shall display the study workload summary of each active goal on the dashboard. | Behaviour is verified by at least one passing test: valid input succeeds, invalid input is rejected with a clear error. (Planned: service not implemented yet.) | planned |
| FR-17.6 | The system shall display career progress on the dashboard. | Behaviour is verified by at least one passing test: valid input succeeds, invalid input is rejected with a clear error. (Planned: service not implemented yet.) | planned |

### NotificationService — FR-18 Send Notifications
Implementation: **not implemented yet** — test cases are planned drafts

| FR | Requirement | Must be true to pass | Test cases |
|---|---|---|---|
| FR-18.1 | The system shall send reminders for upcoming assignments and exam deadlines. | Behaviour is verified by at least one passing test: valid input succeeds, invalid input is rejected with a clear error. (Planned: service not implemented yet.) | planned |
| FR-18.2 | The system shall send reminders for scheduled study tasks. | Behaviour is verified by at least one passing test: valid input succeeds, invalid input is rejected with a clear error. (Planned: service not implemented yet.) | planned |
| FR-18.3 | The system shall send reminders for scheduled habits. | Behaviour is verified by at least one passing test: valid input succeeds, invalid input is rejected with a clear error. (Planned: service not implemented yet.) | planned |
| FR-18.4 | The system shall send early warning notifications when a student is at risk of missing deadlines. | Behaviour is verified by at least one passing test: valid input succeeds, invalid input is rejected with a clear error. (Planned: service not implemented yet.) | planned |
| FR-18.5 | The system shall send goal milestone reminders. | Behaviour is verified by at least one passing test: valid input succeeds, invalid input is rejected with a clear error. (Planned: service not implemented yet.) | planned |
| FR-18.6 | The system shall notify a student when a habit streak reaches a new milestone. | Behaviour is verified by at least one passing test: valid input succeeds, invalid input is rejected with a clear error. (Planned: service not implemented yet.) | planned |

### StudyGroupService — FR-19 Manage Study Groups and Accountability
Implementation: **not implemented yet** — test cases are planned drafts

| FR | Requirement | Must be true to pass | Test cases |
|---|---|---|---|
| FR-19.1 | The system shall allow a student to create a study group. | Behaviour is verified by at least one passing test: valid input succeeds, invalid input is rejected with a clear error. (Planned: service not implemented yet.) | planned |
| FR-19.2 | The system shall allow a student to join a study group. | Behaviour is verified by at least one passing test: valid input succeeds, invalid input is rejected with a clear error. (Planned: service not implemented yet.) | planned |
| FR-19.3 | The system shall allow study group members to share goals. | Behaviour is verified by at least one passing test: valid input succeeds, invalid input is rejected with a clear error. (Planned: service not implemented yet.) | planned |
| FR-19.4 | The system shall allow study group members to track group progress. | Behaviour is verified by at least one passing test: valid input succeeds, invalid input is rejected with a clear error. (Planned: service not implemented yet.) | planned |
| FR-19.5 | The system shall allow a student to find an accountability partner. | Behaviour is verified by at least one passing test: valid input succeeds, invalid input is rejected with a clear error. (Planned: service not implemented yet.) | planned |
| FR-19.6 | The system shall allow a student to connect with an accountability partner. | Behaviour is verified by at least one passing test: valid input succeeds, invalid input is rejected with a clear error. (Planned: service not implemented yet.) | planned |
| FR-19.7 | The system shall allow study groups to share progress updates. | Behaviour is verified by at least one passing test: valid input succeeds, invalid input is rejected with a clear error. (Planned: service not implemented yet.) | planned |
| FR-19.8 | The system shall allow study groups to share support messages. | Behaviour is verified by at least one passing test: valid input succeeds, invalid input is rejected with a clear error. (Planned: service not implemented yet.) | planned |

### AdministrationService — FR-20 Manage Administration
Implementation: **not implemented yet** — test cases are planned drafts

| FR | Requirement | Must be true to pass | Test cases |
|---|---|---|---|
| FR-20.1 | The system shall allow an administrator to view student account details. | Behaviour is verified by at least one passing test: valid input succeeds, invalid input is rejected with a clear error. (Planned: service not implemented yet.) | planned |
| FR-20.2 | The system shall allow an administrator to create a student account. | Behaviour is verified by at least one passing test: valid input succeeds, invalid input is rejected with a clear error. (Planned: service not implemented yet.) | planned |
| FR-20.3 | The system shall allow an administrator to update student account details. | Behaviour is verified by at least one passing test: valid input succeeds, invalid input is rejected with a clear error. (Planned: service not implemented yet.) | planned |
| FR-20.4 | The system shall allow an administrator to suspend a student account. | Behaviour is verified by at least one passing test: valid input succeeds, invalid input is rejected with a clear error. (Planned: service not implemented yet.) | planned |
| FR-20.5 | The system shall provide usage statistics showing the number of registered students. | Behaviour is verified by at least one passing test: valid input succeeds, invalid input is rejected with a clear error. (Planned: service not implemented yet.) | planned |
| FR-20.6 | The system shall provide usage statistics showing the number of active students. | Behaviour is verified by at least one passing test: valid input succeeds, invalid input is rejected with a clear error. (Planned: service not implemented yet.) | planned |
| FR-20.7 | The system shall provide usage statistics showing platform feature usage. | Behaviour is verified by at least one passing test: valid input succeeds, invalid input is rejected with a clear error. (Planned: service not implemented yet.) | planned |
| FR-20.8 | The system shall allow an administrator to investigate student account issues. | Behaviour is verified by at least one passing test: valid input succeeds, invalid input is rejected with a clear error. (Planned: service not implemented yet.) | planned |
| FR-20.9 | The system shall allow an administrator to resolve student account issues. | Behaviour is verified by at least one passing test: valid input succeeds, invalid input is rejected with a clear error. (Planned: service not implemented yet.) | planned |
| FR-20.10 | The system shall display platform retention metrics showing student activity over time. | Behaviour is verified by at least one passing test: valid input succeeds, invalid input is rejected with a clear error. (Planned: service not implemented yet.) | planned |
