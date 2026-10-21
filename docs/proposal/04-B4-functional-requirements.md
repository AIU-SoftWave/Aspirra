# B4 — Functional requirements

> Issue: #4 · Part: B · Owner: Mostafa Khamis Abozead · Reviewer: TBD · Status: Draft

## Content

### High-level functional requirements

| ID | High-level FR (verb title) | Owner | Linked to |
|---|---|---|---|
| FR-01 | Manage Student Profile | Ahmed Mohamed Ahmed | CR-01 |
| FR-02 | Manage Academic Goals | Ahmed Mohamed Ahmed | CR-02, CR-05 |
| FR-03 | Manage Courses | Ahmed Mohamed Ahmed | CR-01, CR-02, CR-04 |
| FR-04 | Manage Assignments and Exams | Ahmed Mohamed Ahmed | CR-02, CR-04, CR-05 |
| FR-05 | Plan Goals with AI | Yomna Mohamed Fekry | CR-01, CR-04 |
| FR-06 | Generate Study Plans | Yomna Mohamed Fekry | CR-01, CR-04, CR-06 |
| FR-07 | Enable Smart Scheduling | Yomna Mohamed Fekry | CR-04, CR-05 |
| FR-08 | Enable Adaptive Planning | Yomna Mohamed Fekry | CR-05, CR-06, CR-11 |
| FR-09 | Manage Career Roadmaps | Arwa Mohey Hamdy | CR-07, CR-09 |
| FR-10 | Track Skills | Arwa Mohey Hamdy | CR-07, CR-08, CR-09 |
| FR-11 | Manage Projects and Portfolios | Arwa Mohey Hamdy | CR-08 |
| FR-12 | Manage Internship and Job Preparation | Arwa Mohey Hamdy | CR-07, CR-08 |
| FR-13 | Manage Habits | Mostafa Khamis Abozead | CR-03, CR-12 |
| FR-14 | Analyze Academic Performance | Mostafa Khamis Abozead | CR-02 |
| FR-15 | Predict and Warn Early | Mostafa Khamis Abozead | CR-06, CR-10, CR-11 |
| FR-16 | Coach Students with AI | Mostafa Khamis Abozead | CR-06, CR-10, CR-11, CR-12 |
| FR-17 | Display Student Dashboard | Muhammed Hany Hamdy | CR-01, CR-02 |
| FR-18 | Send Notifications | Muhammed Hany Hamdy | CR-02, CR-05, CR-10 |
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
**Owner:** Ahmed Mohamed Ahmed | **Linked to:** CR-02, CR-05

| ID | Requirement | Owner | Linked to |
|---|---|---|---|
| FR-02.1 | The system shall allow a student to create an academic goal with a title, description, target date, and priority. | Ahmed Mohamed Ahmed | CR-02 |
| FR-02.2 | The system shall allow a student to update an existing academic goal's details. | Ahmed Mohamed Ahmed | CR-05 |
| FR-02.3 | The system shall allow a student to update the target date of an existing academic goal. | Ahmed Mohamed Ahmed | CR-05 |
| FR-02.4 | The system shall allow a student to mark an academic goal as completed. | Ahmed Mohamed Ahmed | CR-02 |
| FR-02.5 | The system shall allow a student to archive an academic goal. | Ahmed Mohamed Ahmed | CR-02 |
| FR-02.6 | The system shall allow a student to delete an academic goal they created. | Ahmed Mohamed Ahmed | CR-05 |

#### FR-03 — Manage Courses
**Owner:** Ahmed Mohamed Ahmed | **Linked to:** CR-01, CR-02, CR-04

| ID | Requirement | Owner | Linked to |
|---|---|---|---|
| FR-03.1 | The system shall allow a student to add a course with course name, course code, instructor, and semester details. | Ahmed Mohamed Ahmed | CR-01 |
| FR-03.2 | The system shall allow a student to record course grades and credits for each course. | Ahmed Mohamed Ahmed | CR-02 |
| FR-03.3 | The system shall allow a student to view course progress including completed assignments and current standing. | Ahmed Mohamed Ahmed | CR-04 |
| FR-03.4 | The system shall allow a student to update course information. | Ahmed Mohamed Ahmed | CR-01 |
| FR-03.5 | The system shall allow a student to remove a course. | Ahmed Mohamed Ahmed | CR-01 |

#### FR-04 — Manage Assignments and Exams
**Owner:** Ahmed Mohamed Ahmed | **Linked to:** CR-02, CR-04, CR-05

| ID | Requirement | Owner | Linked to |
|---|---|---|---|
| FR-04.1 | The system shall allow a student to add an assignment with title, course, due date, priority, and description. | Ahmed Mohamed Ahmed | CR-02 |
| FR-04.2 | The system shall allow a student to add an exam with title, course, exam date, time, and location. | Ahmed Mohamed Ahmed | CR-02 |
| FR-04.3 | The system shall allow a student to track assignment status as not started, in progress, or completed. | Ahmed Mohamed Ahmed | CR-04 |
| FR-04.4 | The system shall allow a student to set preparation reminders for upcoming exams. | Ahmed Mohamed Ahmed | CR-05 |

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
| FR-06.1 | The system shall generate a personalized study plan based on courses, deadlines, and student availability. | Yomna Mohamed Fekry | CR-01 |
| FR-06.2 | The system shall distribute study tasks across available time slots while respecting priorities. | Yomna Mohamed Fekry | CR-04 |
| FR-06.3 | The system shall allow a student to regenerate the study plan based on updated availability or deadlines. | Yomna Mohamed Fekry | CR-06 |
| FR-06.4 | The system shall validate AI-generated study plan output against an expected schema before saving. | Yomna Mohamed Fekry | CR-04 |

#### FR-07 — Enable Smart Scheduling
**Owner:** Yomna Mohamed Fekry | **Linked to:** CR-04, CR-05

| ID | Requirement | Owner | Linked to |
|---|---|---|---|
| FR-07.1 | The system shall schedule tasks based on deadlines, priorities, workload, availability, and dependencies. | Yomna Mohamed Fekry | CR-04 |
| FR-07.2 | The system shall detect scheduling conflicts between tasks and overlapping time slots. | Yomna Mohamed Fekry | CR-04 |
| FR-07.3 | The system shall suggest alternative time slots when scheduling conflicts occur. | Yomna Mohamed Fekry | CR-05 |
| FR-07.4 | The system shall allow a student to manually adjust scheduled tasks and times. | Yomna Mohamed Fekry | CR-05 |

#### FR-08 — Enable Adaptive Planning
**Owner:** Yomna Mohamed Fekry | **Linked to:** CR-05, CR-06, CR-11

| ID | Requirement | Owner | Linked to |
|---|---|---|---|
| FR-08.1 | The system shall detect when a student is falling behind on tasks or deadlines. | Yomna Mohamed Fekry | CR-06 |
| FR-08.2 | The system shall recommend rescheduling tasks when behind schedule. | Yomna Mohamed Fekry | CR-05 |
| FR-08.3 | The system shall recommend reprioritizing tasks when behind schedule. | Yomna Mohamed Fekry | CR-05 |
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
| FR-09.6 | The system shall regenerate the career roadmap after a target career is updated. | Arwa Mohey Hamdy | CR-09 |

#### FR-10 — Track Skills
**Owner:** Arwa Mohey Hamdy | **Linked to:** CR-07, CR-08, CR-09

| ID | Requirement | Owner | Linked to |
|---|---|---|---|
| FR-10.1 | The system shall allow a student to add technical and soft skills with current skill level. | Arwa Mohey Hamdy | CR-07 |
| FR-10.2 | The system shall compare a student's skills against required skills for their target career. | Arwa Mohey Hamdy | CR-09 |
| FR-10.3 | The system shall identify skill gaps based on career requirements. | Arwa Mohey Hamdy | CR-09 |
| FR-10.4 | The system shall allow a student to update skill levels as they progress. | Arwa Mohey Hamdy | CR-08 |

#### FR-11 — Manage Projects and Portfolios
**Owner:** Arwa Mohey Hamdy | **Linked to:** CR-08

| ID | Requirement | Owner | Linked to |
|---|---|---|---|
| FR-11.1 | The system shall allow a student to add a project with title, description, technologies, and completion status. | Arwa Mohey Hamdy | CR-08 |
| FR-11.2 | The system shall allow a student to link projects to skills they demonstrate. | Arwa Mohey Hamdy | CR-08 |
| FR-11.3 | The system shall generate a portfolio view of completed projects with details. | Arwa Mohey Hamdy | CR-08 |
| FR-11.4 | The system shall allow a student to add project links and documentation to their portfolio. | Arwa Mohey Hamdy | CR-08 |

#### FR-12 — Manage Internship and Job Preparation
**Owner:** Arwa Mohey Hamdy | **Linked to:** CR-07, CR-08

| ID | Requirement | Owner | Linked to |
|---|---|---|---|
| FR-12.1 | The system shall allow a student to track internship and job applications with company, position, status, and deadline. | Arwa Mohey Hamdy | CR-07 |
| FR-12.2 | The system shall allow a student to track interview preparation tasks for each application. | Arwa Mohey Hamdy | CR-07 |
| FR-12.3 | The system shall allow a student to track CV and portfolio updates related to applications. | Arwa Mohey Hamdy | CR-08 |
| FR-12.4 | The system shall allow a student to record application outcomes and feedback. | Arwa Mohey Hamdy | CR-08 |

#### FR-13 — Manage Habits
**Owner:** Mostafa Khamis Abozead | **Linked to:** CR-03, CR-12

| ID | Requirement | Owner | Linked to |
|---|---|---|---|
| FR-13.1 | The system shall allow a student to create a habit with name, frequency, goal, and reminder settings. | Mostafa Khamis Abozead | CR-03 |
| FR-13.2 | The system shall allow a student to mark habits as completed for each scheduled day. | Mostafa Khamis Abozead | CR-03 |
| FR-13.3 | The system shall track habit streaks based on consecutive completions. | Mostafa Khamis Abozead | CR-03 |
| FR-13.4 | The system shall allow a student to link habits to academic goals for motivation tracking. | Mostafa Khamis Abozead | CR-12 |

#### FR-14 — Analyze Academic Performance
**Owner:** Mostafa Khamis Abozead | **Linked to:** **UNRESOLVED** (no explicit CR in B3)

| ID | Requirement | Owner | Linked to |
|---|---|---|---|
| FR-14.1 | The system shall calculate grade trends across courses and semesters. | Mostafa Khamis Abozead | **UNRESOLVED** (no explicit CR in B3) |
| FR-14.2 | The system shall display grade trends across courses and semesters. | Mostafa Khamis Abozead | **UNRESOLVED** (no explicit CR in B3) |
| FR-14.3 | The system shall track assignment completion rates per course. | Mostafa Khamis Abozead | **UNRESOLVED** (no explicit CR in B3) |
| FR-14.4 | The system shall calculate GPA progress based on recorded course grades. | Mostafa Khamis Abozead | **UNRESOLVED** (no explicit CR in B3) |
| FR-14.5 | The system shall display GPA progress based on recorded course grades. | Mostafa Khamis Abozead | **UNRESOLVED** (no explicit CR in B3) |

#### FR-15 — Predict and Warn Early
**Owner:** Mostafa Khamis Abozead | **Linked to:** CR-06, CR-10, CR-11

| ID | Requirement | Owner | Linked to |
|---|---|---|---|
| FR-15.1 | The system shall estimate goal completion probability based on current progress and remaining time. | Mostafa Khamis Abozead | CR-06 |
| FR-15.2 | The system shall identify academic risks when assignment completion falls below expected rates. | Mostafa Khamis Abozead | CR-10 |
| FR-15.3 | The system shall generate early warnings for students at risk of missing deadlines. | Mostafa Khamis Abozead | CR-10 |
| FR-15.4 | The system shall suggest recovery actions for students who are at risk of falling behind. | Mostafa Khamis Abozead | CR-11 |

#### FR-16 — Coach Students with AI
**Owner:** Mostafa Khamis Abozead | **Linked to:** CR-06, CR-10, CR-11, CR-12

| ID | Requirement | Owner | Linked to |
|---|---|---|---|
| FR-16.1 | The system shall provide personalized recommendations based on goals, tasks, habits, and progress. | Mostafa Khamis Abozead | CR-06 |
| FR-16.2 | The system shall offer study advice tailored to upcoming deadlines and workload. | Mostafa Khamis Abozead | CR-10 |
| FR-16.3 | The system shall suggest recovery strategies when a student falls behind their plan. | Mostafa Khamis Abozead | CR-11 |
| FR-16.4 | The system shall provide motivational guidance to help students stay on track with their goals. | Mostafa Khamis Abozead | CR-12 |

#### FR-17 — Display Student Dashboard
**Owner:** Muhammed Hany Hamdy | **Linked to:** CR-01, CR-02

| ID | Requirement | Owner | Linked to |
|---|---|---|---|
| FR-17.1 | The system shall display today's tasks and upcoming deadlines on the student dashboard. | Muhammed Hany Hamdy | CR-02 |
| FR-17.2 | The system shall display goal progress on the dashboard. | Muhammed Hany Hamdy | CR-02 |
| FR-17.6 | The system shall display goal completion percentages on the dashboard. | Muhammed Hany Hamdy | CR-02 |
| FR-17.3 | The system shall display habit streaks and completion status on the dashboard. | Muhammed Hany Hamdy | CR-03 |
| FR-17.4 | The system shall display an academic performance summary on the dashboard. | Muhammed Hany Hamdy | CR-02 |
| FR-17.5 | The system shall display career progress on the dashboard. | Muhammed Hany Hamdy | CR-07 |

#### FR-18 — Send Notifications
**Owner:** Muhammed Hany Hamdy | **Linked to:** CR-02, CR-05, CR-10

| ID | Requirement | Owner | Linked to |
|---|---|---|---|
| FR-18.1 | The system shall send reminders for upcoming assignments and exam deadlines. | Muhammed Hany Hamdy | CR-02 |
| FR-18.2 | The system shall send reminders for scheduled study tasks. | Muhammed Hany Hamdy | CR-05 |
| FR-18.6 | The system shall send reminders for scheduled habits. | Muhammed Hany Hamdy | CR-03 |
| FR-18.3 | The system shall send early warning notifications when a student is at risk of missing deadlines. | Muhammed Hany Hamdy | CR-10 |
| FR-18.4 | The system shall send goal milestone reminders. | Muhammed Hany Hamdy | CR-02 |
| FR-18.5 | The system shall send achievement notifications. | Muhammed Hany Hamdy | CR-02 |

#### FR-19 — Manage Study Groups and Accountability
**Owner:** Muhammed Hany Hamdy | **Linked to:** CR-12

| ID | Requirement | Owner | Linked to |
|---|---|---|---|
| FR-19.1 | The system shall allow a student to create or join a study group. | Muhammed Hany Hamdy | CR-12 |
| FR-19.2 | The system shall allow study group members to share goals. | Muhammed Hany Hamdy | CR-12 |
| FR-19.3 | The system shall allow study group members to track group progress. | Muhammed Hany Hamdy | CR-12 |
| FR-19.4 | The system shall allow a student to find and connect with an accountability partner. | Muhammed Hany Hamdy | CR-12 |
| FR-19.5 | The system shall allow study groups to share progress updates. | Muhammed Hany Hamdy | CR-12 |
| FR-19.6 | The system shall allow study groups to share support messages. | Muhammed Hany Hamdy | CR-12 |

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
| FR-20.10 | The system shall provide usage statistics showing platform feature usage. | Muhammed Hany Hamdy | CR-14 |
| FR-20.7 | The system shall allow an administrator to investigate student account issues. | Muhammed Hany Hamdy | CR-15 |
| FR-20.8 | The system shall allow an administrator to resolve student account issues. | Muhammed Hany Hamdy | CR-15 |
| FR-20.9 | The system shall display platform retention metrics showing student activity over time. | Muhammed Hany Hamdy | CR-16 |

## Traceability Notes

- **FR-01.3**: Updated to link to CR-01 (profile management) to align with the student profile need; no longer linked to CR-02.
- **FR-02**: Low-level requirements reordered sequentially (FR-02.1 through FR-02.6).
- **FR-05.3**: Split into review, edit milestones, and edit tasks (FR-05.3, FR-05.4, FR-05.5); approval moved to FR-05.6.
- **FR-08.2**: Split into recommend rescheduling and recommend reprioritizing (FR-08.2, FR-08.3).
- **FR-09.1**: Split into select from predefined list and enter custom career (FR-09.1, FR-09.2).
- **FR-14**: No explicit Customer Requirement in B3 supports academic performance analytics (grade trends, GPA, assignment completion rates). All FR-14 requirements are marked **UNRESOLVED** with no CR mapping. No CR was invented or modified.
- **FR-17.1**: Linked to CR-02 (knowing what is due and when).
- **FR-17.2 / FR-17.6**: Goal progress split into display and completion percentages, both linked to CR-02.
- **FR-17.3**: Habit streaks linked to CR-03 (habit building).
- **FR-17.4**: Academic performance summary linked to CR-02.
- **FR-17.5**: Career progress linked to CR-07.
- **FR-18.2**: Split into reminders for scheduled study tasks (CR-05) and reminders for scheduled habits (CR-03).
- **FR-20**: Added FR-20.10 for platform feature usage statistics under CR-14 (registered, active, feature usage, retention covered).

## Checklist

- [x] List high-level FRs (verb titles) and assign 4 per member
- [x] Write low-level FRs (>= 2 per high-level)
- [x] Add "Linked to" column mapping each FR to its CR
- [x] Add owner next to each FR
- [x] All low-level requirements start with "The system shall ..."
- [x] Each requirement describes one specific behavior
- [x] Requirements are testable and traceable to CRs where supported
- [x] Team member names from A2 team table are used
- [x] All CR IDs from B3 are used exactly as written
- [x] Exactly 20 high-level FRs with 4 per team member
- [x] Low-level numbering is sequential and consistent after all splits
- [x] Compound requirements split into atomic behaviors (FR-02, FR-05, FR-08, FR-09, FR-18)
- [ ] All FRs have undisputed traceability to existing CRs (see Traceability Notes above — FR-14 remains unresolved)
