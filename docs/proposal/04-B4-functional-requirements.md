# B4 — Functional requirements

> Issue: #4 · Part: B · Owner: Mostafa Khamis Abozead · Reviewer: TBD · Status: Draft

## Content

### High-level functional requirements

| ID | High-level FR (verb title) | Owner | Linked to |
|---|---|---|---|
| FR-01 | Manage Student Profile | Ahmed Mohamed Ahmed | CR-01 |
| FR-02 | Manage Academic Goals | Ahmed Mohamed Ahmed | CR-01, CR-02 |
| FR-03 | Manage Courses | Ahmed Mohamed Ahmed | CR-01, CR-02, CR-04 |
| FR-04 | Manage Assignments and Exams | Ahmed Mohamed Ahmed | CR-02, CR-04 |
| FR-05 | Plan Goals with AI | Yomna Mohamed Fekry | CR-01, CR-04 |
| FR-06 | Generate Study Plans | Yomna Mohamed Fekry | CR-01, CR-04, CR-06 |
| FR-07 | Enable Smart Scheduling | Yomna Mohamed Fekry | CR-04, CR-05 |
| FR-08 | Enable Adaptive Planning | Yomna Mohamed Fekry | CR-04, CR-05, CR-06, CR-11 |
| FR-09 | Manage Career Roadmaps | Arwa Mohey Hamdy | CR-07, CR-09 |
| FR-10 | Track Skills | Arwa Mohey Hamdy | CR-08, CR-09 |
| FR-11 | Manage Projects and Portfolios | Arwa Mohey Hamdy | CR-08 |
| FR-12 | Manage Internship and Job Preparation | Arwa Mohey Hamdy | CR-07, CR-08 |
| FR-13 | Manage Habits | Mostafa Khamis Abozead | CR-03 |
| FR-14 | Analyze Study Workload | Mostafa Khamis Abozead | CR-06 |
| FR-15 | Predict and Warn Early | Mostafa Khamis Abozead | CR-06, CR-10, CR-11 |
| FR-16 | Coach Students with AI | Mostafa Khamis Abozead | CR-06, CR-10, CR-11, CR-12 |
| FR-17 | Display Student Dashboard | Muhammed Hany Hamdy | CR-01, CR-02, CR-03, CR-06, CR-07 |
| FR-18 | Send Notifications | Muhammed Hany Hamdy | CR-02, CR-03, CR-04, CR-05, CR-10 |
| FR-19 | Manage Study Groups and Accountability | Muhammed Hany Hamdy | CR-12 |
| FR-20 | Manage Administration | Muhammed Hany Hamdy | CR-13, CR-14, CR-15, CR-16 |

### Low-level functional requirements

#### FR-01 — Manage Student Profile
**Owner:** Ahmed Mohamed Ahmed | **Linked to:** CR-01

| ID | Requirement | Owner | Linked to |
|---|---|---|---|
| FR-01.1 | The system shall allow a student to create a profile with academic level, major, interests, skills, career interests, and availability. | Ahmed Mohamed Ahmed | CR-01 |
| FR-01.2 | The system shall allow a student to update profile information including availability and career interests. | Ahmed Mohamed Ahmed | CR-01 |
| FR-01.3 | The system shall allow a student to view their complete profile details. | Ahmed Mohamed Ahmed | CR-01 |
| FR-01.4 | The system shall validate required profile fields before saving profile data. | Ahmed Mohamed Ahmed | CR-01 |

#### FR-02 — Manage Academic Goals
**Owner:** Ahmed Mohamed Ahmed | **Linked to:** CR-01, CR-02

| ID | Requirement | Owner | Linked to |
|---|---|---|---|
| FR-02.1 | The system shall allow a student to create an academic goal with a title, description, target date, and priority. | Ahmed Mohamed Ahmed | CR-01, CR-02 |
| FR-02.2 | The system shall allow a student to update the details of an existing academic goal. | Ahmed Mohamed Ahmed | CR-01 |
| FR-02.3 | The system shall allow a student to update the target date of an existing academic goal. | Ahmed Mohamed Ahmed | CR-02 |
| FR-02.4 | The system shall allow a student to mark an academic goal as completed. | Ahmed Mohamed Ahmed | CR-02 |
| FR-02.5 | The system shall allow a student to archive an academic goal. | Ahmed Mohamed Ahmed | CR-01 |
| FR-02.6 | The system shall allow a student to delete an academic goal they created. | Ahmed Mohamed Ahmed | CR-01 |

#### FR-03 — Manage Courses
**Owner:** Ahmed Mohamed Ahmed | **Linked to:** CR-01, CR-02, CR-04

| ID | Requirement | Owner | Linked to |
|---|---|---|---|
| FR-03.1 | The system shall allow a student to add a course with course name, course code, instructor, and semester details. | Ahmed Mohamed Ahmed | CR-01 |
| FR-03.2 | The system shall allow a student to record the credits and the weekly workload of each course. | Ahmed Mohamed Ahmed | CR-01, CR-04 |
| FR-03.3 | The system shall allow a student to view the progress of a course, including its completed and outstanding assignments. | Ahmed Mohamed Ahmed | CR-02, CR-04 |
| FR-03.4 | The system shall allow a student to update course information. | Ahmed Mohamed Ahmed | CR-01 |
| FR-03.5 | The system shall allow a student to remove a course. | Ahmed Mohamed Ahmed | CR-01 |

#### FR-04 — Manage Assignments and Exams
**Owner:** Ahmed Mohamed Ahmed | **Linked to:** CR-02, CR-04

| ID | Requirement | Owner | Linked to |
|---|---|---|---|
| FR-04.1 | The system shall allow a student to add an assignment with title, course, due date, priority, and description. | Ahmed Mohamed Ahmed | CR-02 |
| FR-04.2 | The system shall allow a student to add an exam with title, course, exam date, time, and location. | Ahmed Mohamed Ahmed | CR-02 |
| FR-04.3 | The system shall allow a student to track assignment status as not started, in progress, or completed. | Ahmed Mohamed Ahmed | CR-02, CR-04 |
| FR-04.4 | The system shall allow a student to set preparation reminders for upcoming exams. | Ahmed Mohamed Ahmed | CR-02 |

#### FR-05 — Plan Goals with AI
**Owner:** Yomna Mohamed Fekry | **Linked to:** CR-01, CR-04

| ID | Requirement | Owner | Linked to |
|---|---|---|---|
| FR-05.1 | The system shall allow a student to input a goal in natural language for AI processing. | Yomna Mohamed Fekry | CR-01 |
| FR-05.2 | The system shall generate a structured goal roadmap with milestones, tasks, and deadlines from natural language input. | Yomna Mohamed Fekry | CR-01 |
| FR-05.3 | The system shall allow a student to review AI-generated milestones and tasks before saving. | Yomna Mohamed Fekry | CR-04 |
| FR-05.4 | The system shall allow a student to edit AI-generated milestones before saving. | Yomna Mohamed Fekry | CR-04 |
| FR-05.5 | The system shall allow a student to edit AI-generated tasks before saving. | Yomna Mohamed Fekry | CR-04 |
| FR-05.6 | The system shall require student approval before applying AI-generated goals to their plan. | Yomna Mohamed Fekry | CR-04 |

#### FR-06 — Generate Study Plans
**Owner:** Yomna Mohamed Fekry | **Linked to:** CR-01, CR-04, CR-06

| ID | Requirement | Owner | Linked to |
|---|---|---|---|
| FR-06.1 | The system shall generate a personalized study plan based on courses, deadlines, and student availability. | Yomna Mohamed Fekry | CR-01, CR-04 |
| FR-06.2 | The system shall distribute study tasks across available time slots while respecting priorities. | Yomna Mohamed Fekry | CR-04 |
| FR-06.3 | The system shall allow a student to regenerate the study plan after an availability or deadline change. | Yomna Mohamed Fekry | CR-04, CR-06 |
| FR-06.4 | The system shall reject an AI-generated study plan that does not match the expected plan structure. | Yomna Mohamed Fekry | CR-04 |
| FR-06.5 | The system shall retain the previously saved study plan when an AI-generated plan is rejected. | Yomna Mohamed Fekry | CR-04 |

#### FR-07 — Enable Smart Scheduling
**Owner:** Yomna Mohamed Fekry | **Linked to:** CR-04, CR-05

| ID | Requirement | Owner | Linked to |
|---|---|---|---|
| FR-07.1 | The system shall schedule tasks based on deadlines, priorities, workload, availability, and dependencies. | Yomna Mohamed Fekry | CR-04 |
| FR-07.2 | The system shall detect scheduling conflicts between tasks and overlapping time slots. | Yomna Mohamed Fekry | CR-04 |
| FR-07.3 | The system shall suggest alternative time slots when scheduling conflicts occur. | Yomna Mohamed Fekry | CR-05 |
| FR-07.4 | The system shall allow a student to manually adjust scheduled tasks and times. | Yomna Mohamed Fekry | CR-04 |

#### FR-08 — Enable Adaptive Planning
**Owner:** Yomna Mohamed Fekry | **Linked to:** CR-04, CR-05, CR-06, CR-11

| ID | Requirement | Owner | Linked to |
|---|---|---|---|
| FR-08.1 | The system shall detect when a student is falling behind on tasks or deadlines. | Yomna Mohamed Fekry | CR-06 |
| FR-08.2 | The system shall recommend rescheduling tasks when the student is behind schedule. | Yomna Mohamed Fekry | CR-04, CR-05 |
| FR-08.3 | The system shall recommend reprioritizing tasks when the student is behind schedule. | Yomna Mohamed Fekry | CR-05 |
| FR-08.4 | The system shall require student approval before applying adaptive plan changes. | Yomna Mohamed Fekry | CR-11 |
| FR-08.5 | The system shall allow a student to accept adaptive planning recommendations. | Yomna Mohamed Fekry | CR-11 |
| FR-08.6 | The system shall allow a student to reject adaptive planning recommendations. | Yomna Mohamed Fekry | CR-11 |
| FR-08.7 | The system shall allow a student to modify adaptive planning recommendations. | Yomna Mohamed Fekry | CR-11 |

#### FR-09 — Manage Career Roadmaps
**Owner:** Arwa Mohey Hamdy | **Linked to:** CR-07, CR-09

| ID | Requirement | Owner | Linked to |
|---|---|---|---|
| FR-09.1 | The system shall allow a student to select a target career from a predefined list. | Arwa Mohey Hamdy | CR-07 |
| FR-09.2 | The system shall allow a student to enter a custom target career. | Arwa Mohey Hamdy | CR-07 |
| FR-09.3 | The system shall generate a career roadmap with required skills, learning steps, and milestones. | Arwa Mohey Hamdy | CR-07 |
| FR-09.4 | The system shall allow a student to track progress toward completing career roadmap milestones. | Arwa Mohey Hamdy | CR-09 |
| FR-09.5 | The system shall allow a student to update their target career. | Arwa Mohey Hamdy | CR-09 |
| FR-09.6 | The system shall regenerate the career roadmap after the target career is updated. | Arwa Mohey Hamdy | CR-09 |

#### FR-10 — Track Skills
**Owner:** Arwa Mohey Hamdy | **Linked to:** CR-08, CR-09

| ID | Requirement | Owner | Linked to |
|---|---|---|---|
| FR-10.1 | The system shall allow a student to add technical and soft skills with current skill level. | Arwa Mohey Hamdy | CR-08 |
| FR-10.2 | The system shall compare a student's skills against the required skills of their target career. | Arwa Mohey Hamdy | CR-09 |
| FR-10.3 | The system shall identify skill gaps based on career requirements. | Arwa Mohey Hamdy | CR-09 |
| FR-10.4 | The system shall allow a student to update skill levels as they progress. | Arwa Mohey Hamdy | CR-09 |

#### FR-11 — Manage Projects and Portfolios
**Owner:** Arwa Mohey Hamdy | **Linked to:** CR-08

| ID | Requirement | Owner | Linked to |
|---|---|---|---|
| FR-11.1 | The system shall allow a student to add a project with title, description, technologies, and completion status. | Arwa Mohey Hamdy | CR-08 |
| FR-11.2 | The system shall allow a student to link projects to the skills they demonstrate. | Arwa Mohey Hamdy | CR-08 |
| FR-11.3 | The system shall generate a portfolio view of the student's completed projects. | Arwa Mohey Hamdy | CR-08 |
| FR-11.4 | The system shall allow a student to add project links and documentation to their portfolio. | Arwa Mohey Hamdy | CR-08 |

#### FR-12 — Manage Internship and Job Preparation
**Owner:** Arwa Mohey Hamdy | **Linked to:** CR-07, CR-08

| ID | Requirement | Owner | Linked to |
|---|---|---|---|
| FR-12.1 | The system shall allow a student to track internship and job applications with company, position, status, and deadline. | Arwa Mohey Hamdy | CR-07 |
| FR-12.2 | The system shall allow a student to track interview preparation tasks for each application. | Arwa Mohey Hamdy | CR-07 |
| FR-12.3 | The system shall allow a student to track CV updates related to applications. | Arwa Mohey Hamdy | CR-08 |
| FR-12.4 | The system shall allow a student to track portfolio updates related to applications. | Arwa Mohey Hamdy | CR-08 |
| FR-12.5 | The system shall allow a student to record the outcome of an application. | Arwa Mohey Hamdy | CR-07 |
| FR-12.6 | The system shall allow a student to record feedback received for an application. | Arwa Mohey Hamdy | CR-07 |

#### FR-13 — Manage Habits
**Owner:** Mostafa Khamis Abozead | **Linked to:** CR-03

| ID | Requirement | Owner | Linked to |
|---|---|---|---|
| FR-13.1 | The system shall allow a student to create a habit with name, frequency, goal, and reminder settings. | Mostafa Khamis Abozead | CR-03 |
| FR-13.2 | The system shall allow a student to mark a habit as completed for a scheduled day. | Mostafa Khamis Abozead | CR-03 |
| FR-13.3 | The system shall track habit streaks based on consecutive completions. | Mostafa Khamis Abozead | CR-03 |
| FR-13.4 | The system shall allow a student to link habits to academic goals. | Mostafa Khamis Abozead | CR-03 |

#### FR-14 — Analyze Study Workload
**Owner:** Mostafa Khamis Abozead | **Linked to:** CR-06

| ID | Requirement | Owner | Linked to |
|---|---|---|---|
| FR-14.1 | The system shall calculate the remaining workload of each active goal from its unfinished scheduled tasks and their scheduled durations. | Mostafa Khamis Abozead | CR-06 |
| FR-14.2 | The system shall calculate the remaining available study time of each active goal up to its target date, using the student's availability. | Mostafa Khamis Abozead | CR-06 |
| FR-14.3 | The system shall calculate the percentage of planned task duration completed for each active goal. | Mostafa Khamis Abozead | CR-06 |
| FR-14.4 | The system shall display the remaining workload, remaining available study time, and workload completion ratio of each active goal. | Mostafa Khamis Abozead | CR-06 |
| FR-14.5 | The system shall mark an active goal as overloaded when its remaining workload exceeds its remaining available study time. | Mostafa Khamis Abozead | CR-06 |

#### FR-15 — Predict and Warn Early
**Owner:** Mostafa Khamis Abozead | **Linked to:** CR-06, CR-10, CR-11

| ID | Requirement | Owner | Linked to |
|---|---|---|---|
| FR-15.1 | The system shall estimate the likelihood of completing each active goal before its target date from its workload completion ratio and remaining available study time. | Mostafa Khamis Abozead | CR-06 |
| FR-15.2 | The system shall generate an early warning for an active goal before its target date when the goal is marked as overloaded. | Mostafa Khamis Abozead | CR-06, CR-10 |
| FR-15.3 | The system shall identify which scheduled tasks of an active goal push its workload over its available study time. | Mostafa Khamis Abozead | CR-06, CR-10 |
| FR-15.4 | The system shall suggest recovery actions for a goal with an active early warning so the student can recover before the target date. | Mostafa Khamis Abozead | CR-11 |

#### FR-16 — Coach Students with AI
**Owner:** Mostafa Khamis Abozead | **Linked to:** CR-06, CR-10, CR-11, CR-12

| ID | Requirement | Owner | Linked to |
|---|---|---|---|
| FR-16.1 | The system shall provide personalized recommendations based on the student's goals, scheduled tasks, habits, and workload progress. | Mostafa Khamis Abozead | CR-06 |
| FR-16.2 | The system shall offer study advice tailored to upcoming deadlines and workload. | Mostafa Khamis Abozead | CR-06, CR-10 |
| FR-16.3 | The system shall suggest recovery strategies when a student falls behind their plan. | Mostafa Khamis Abozead | CR-11 |
| FR-16.4 | The system shall provide motivational guidance to help students stay on track with their goals. | Mostafa Khamis Abozead | CR-12 |

#### FR-17 — Display Student Dashboard
**Owner:** Muhammed Hany Hamdy | **Linked to:** CR-01, CR-02, CR-03, CR-06, CR-07

| ID | Requirement | Owner | Linked to |
|---|---|---|---|
| FR-17.1 | The system shall display today's tasks and upcoming deadlines on the student dashboard. | Muhammed Hany Hamdy | CR-02 |
| FR-17.2 | The system shall display the current status of each goal on the dashboard. | Muhammed Hany Hamdy | CR-02 |
| FR-17.3 | The system shall display the completion percentage of each goal on the dashboard. | Muhammed Hany Hamdy | CR-01, CR-02 |
| FR-17.4 | The system shall display habit streaks and completion status on the dashboard. | Muhammed Hany Hamdy | CR-03 |
| FR-17.5 | The system shall display the study workload summary of each active goal on the dashboard. | Muhammed Hany Hamdy | CR-06 |
| FR-17.6 | The system shall display career progress on the dashboard. | Muhammed Hany Hamdy | CR-07 |

#### FR-18 — Send Notifications
**Owner:** Muhammed Hany Hamdy | **Linked to:** CR-02, CR-03, CR-04, CR-05, CR-10

| ID | Requirement | Owner | Linked to |
|---|---|---|---|
| FR-18.1 | The system shall send reminders for upcoming assignments and exam deadlines. | Muhammed Hany Hamdy | CR-02 |
| FR-18.2 | The system shall send reminders for scheduled study tasks. | Muhammed Hany Hamdy | CR-04, CR-05 |
| FR-18.3 | The system shall send reminders for scheduled habits. | Muhammed Hany Hamdy | CR-03 |
| FR-18.4 | The system shall send early warning notifications when a student is at risk of missing deadlines. | Muhammed Hany Hamdy | CR-10 |
| FR-18.5 | The system shall send goal milestone reminders. | Muhammed Hany Hamdy | CR-02 |
| FR-18.6 | The system shall notify a student when a habit streak reaches a new milestone. | Muhammed Hany Hamdy | CR-03 |

#### FR-19 — Manage Study Groups and Accountability
**Owner:** Muhammed Hany Hamdy | **Linked to:** CR-12

| ID | Requirement | Owner | Linked to |
|---|---|---|---|
| FR-19.1 | The system shall allow a student to create a study group. | Muhammed Hany Hamdy | CR-12 |
| FR-19.2 | The system shall allow a student to join a study group. | Muhammed Hany Hamdy | CR-12 |
| FR-19.3 | The system shall allow study group members to share goals. | Muhammed Hany Hamdy | CR-12 |
| FR-19.4 | The system shall allow study group members to track group progress. | Muhammed Hany Hamdy | CR-12 |
| FR-19.5 | The system shall allow a student to find an accountability partner. | Muhammed Hany Hamdy | CR-12 |
| FR-19.6 | The system shall allow a student to connect with an accountability partner. | Muhammed Hany Hamdy | CR-12 |
| FR-19.7 | The system shall allow study groups to share progress updates. | Muhammed Hany Hamdy | CR-12 |
| FR-19.8 | The system shall allow study groups to share support messages. | Muhammed Hany Hamdy | CR-12 |

#### FR-20 — Manage Administration
**Owner:** Muhammed Hany Hamdy | **Linked to:** CR-13, CR-14, CR-15, CR-16

| ID | Requirement | Owner | Linked to |
|---|---|---|---|
| FR-20.1 | The system shall allow an administrator to view student account details. | Muhammed Hany Hamdy | CR-13 |
| FR-20.2 | The system shall allow an administrator to create a student account. | Muhammed Hany Hamdy | CR-13 |
| FR-20.3 | The system shall allow an administrator to update student account details. | Muhammed Hany Hamdy | CR-13 |
| FR-20.4 | The system shall allow an administrator to suspend a student account. | Muhammed Hany Hamdy | CR-13 |
| FR-20.5 | The system shall provide usage statistics showing the number of registered students. | Muhammed Hany Hamdy | CR-14 |
| FR-20.6 | The system shall provide usage statistics showing the number of active students. | Muhammed Hany Hamdy | CR-14 |
| FR-20.7 | The system shall provide usage statistics showing platform feature usage. | Muhammed Hany Hamdy | CR-14 |
| FR-20.8 | The system shall allow an administrator to investigate student account issues. | Muhammed Hany Hamdy | CR-15 |
| FR-20.9 | The system shall allow an administrator to resolve student account issues. | Muhammed Hany Hamdy | CR-15 |
| FR-20.10 | The system shall display platform retention metrics showing student activity over time. | Muhammed Hany Hamdy | CR-16 |

## Traceability Notes

- **FR-01**: Profile management (including availability, used by scheduling) is mapped to CR-01, the only documented need covering planning courses and workload.
- **FR-02**: Goal lifecycle is mapped to CR-01 (planning) and CR-02 (knowing what is due and when). B3 has no CR dedicated to goal tracking, so these are the closest documented needs.
- **FR-03.2**: Narrowed to course credits and weekly workload, which are covered by CR-01 and CR-04. Grade recording was removed because no CR in B3 supports grades.
- **FR-03.3**: Course progress is mapped to CR-02 (outstanding assignments) and CR-04 (workload in one realistic schedule).
- **FR-04.3 / FR-04.4**: Assignment status tracking is mapped to CR-02 and CR-04; exam preparation reminders are mapped to CR-02, since CR-05 concerns prioritisation when deadlines collide.
- **FR-05**: Review, edit milestones, and edit tasks were split into FR-05.3, FR-05.4, and FR-05.5; the approval behaviour is FR-05.6. Review and edit are mapped to CR-04 because the student must be able to fit the AI proposal into a realistic schedule.
- **FR-06.1 / FR-06.3**: Plan generation is mapped to CR-01 and CR-04; regeneration after an availability or deadline change is also mapped to CR-06, since a changed workload may no longer be finishable.
- **FR-06.4 / FR-06.5**: Split into rejecting an invalid AI-generated plan and retaining the previously saved plan. Both are mapped to CR-04, because the student must keep a plan that still fits courses, projects, and work into one realistic schedule when a generation attempt fails.
- **FR-07**: Conflict resolution alternatives are mapped to CR-05 (knowing what to do first when deadlines collide); manual adjustment is mapped to CR-04 (fitting the work into one realistic schedule).
- **FR-08.2 / FR-08.3**: Rescheduling is mapped to CR-04 and CR-05; reprioritising is mapped to CR-05. Accept, reject, and modify are mapped to CR-11, the documented need for a clear way to recover after a plan breaks.
- **FR-09 / FR-10**: Career roadmap and skill work are mapped to CR-07 (balancing coursework with job preparation) and CR-09 (knowing which skills are missing for the target career). FR-10.1 is mapped to CR-08, since recorded skills are the content the student shows to employers in one place.
- **FR-11 / FR-12**: Projects, portfolio, CV, and portfolio updates are mapped to CR-08 (showing skills and projects to employers in one place); application tracking, interview preparation, outcomes, and feedback are mapped to CR-07 (job preparation).
- **FR-13.4**: Linking habits to academic goals is mapped to CR-03, the documented need to build good study habits from the start, so FR-13 is now linked to CR-03 only.
- **FR-14 (narrowed)**: Previously this FR covered grade trends, assignment completion rates, and GPA progress, which no CR in B3 supports. It is now **Analyze Study Workload**, defined only by the workload-planning need in CR-06 (knowing early when the workload is too much to finish): remaining workload, remaining available study time, the percentage of planned task duration completed, and the overloaded state.
- **FR-15**: Reworked around the same workload need. Early warnings are raised for goals already marked as overloaded, which is exactly the early warning CR-10 describes; recovery suggestions are mapped to CR-11.
- **FR-16.1 / FR-16.2**: Coaching recommendations and deadline-aware study advice are mapped to CR-06 and CR-10; recovery strategies to CR-11; motivational guidance to CR-12.
- **FR-17**: Renumbered after the splits. FR-17.2 shows the current status of each goal, mapped to CR-02 (knowing what is due and when). FR-17.3 shows the completion percentage of each goal, mapped to CR-01 (progress against the plan) and CR-02 (progress against the target date). Habit streaks are mapped to CR-03 and career progress to CR-07. FR-17.5 previously showed an academic performance summary, which had no supporting CR; it now shows the study workload summary under CR-06.
- **FR-18**: Reminders for scheduled study tasks are mapped to CR-04 and CR-05; habit reminders and habit streak milestones to CR-03; deadline reminders to CR-02; early warning notifications to CR-10.
- **FR-19**: Creating and joining a study group, and finding and connecting with an accountability partner, were split into separate behaviours, all mapped to CR-12.
- **FR-20**: Account management is mapped to CR-13; issue investigation and resolution to CR-15; registered students, active students, and platform feature usage statistics to CR-14; retention metrics to CR-16.

### Scope notes

- Grade trends, assignment completion rates, GPA progress, and grade recording are **not** part of B4, because no Customer Requirement in B3 supports them. If the team wants these capabilities, a customer requirement must be added to B3 first, and FR-14 plus FR-17.5 can then be extended accordingly.
- Career, job-preparation, and study-group requirements are kept because the assignment requires four high-level FRs per member and the README assigns these areas to Members 3 and 5. They remain later-phase items against the suggested MVP and must be prioritised in the backlog (C8).

## Checklist

- [x] List high-level FRs (verb titles) and assign 4 per member
- [x] Write low-level FRs (>= 2 per high-level)
- [x] Add "Linked to" column mapping each FR to its CR
- [x] Add owner next to each FR
- [x] All low-level requirements start with "The system shall ..."
- [x] Each requirement describes one specific behavior
- [x] Requirements are testable and traceable to CRs in B3
- [x] Team member names from A2 team table are used
- [x] All CR IDs from B3 are used exactly as written
- [x] Exactly 20 high-level FRs with 4 per team member
- [x] Low-level numbering is sequential and consistent after all splits
- [x] Compound requirements split into atomic behaviors (FR-02, FR-05, FR-06, FR-08, FR-09, FR-12, FR-18, FR-19, FR-20)
- [x] Every FR links to at least one existing CR that supports its behavior, with no unsupported or unlinked rows
- [x] High-level rows, section headers, and low-level rows agree on owner and CR mappings
- [x] Markdown tables and headings render correctly
- [ ] Teammate review of this deliverable — not yet performed, so Reviewer stays TBD and Status stays Draft
