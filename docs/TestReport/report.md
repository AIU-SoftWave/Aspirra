# Aspirra — Test Report

**Course:** CSE454 — Advanced Software Engineering, Alamein International University  
**Repository:** https://github.com/AIU-SoftWave/Aspirra  
**Tools:** xUnit, Moq, GitHub Actions  
**Date:** 2026-11-26

**Team:** Ahmed Mohamed Ahmed (Leader), Yomna Mohamed Fekry, Arwa Mohey Hamdy, Mostafa Khamis Abozead, Muhammed Hany Hamdy

## Contents
1. Testing Requirements
2. Test Cases (one section per member)
3. Testing Code
4. Testing Results

\newpage

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


\newpage

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


\newpage

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


\newpage

# 2. Test Cases — Arwa Mohey Hamdy

Owner: `ArwaMohey`. Status/Actual Result are filled from the latest test run (`04-testing-results.md`).

## CareerService

| ID | Requirement | Description | Input | Expected Result | Actual Result | Status |
|---|---|---|---|---|---|---|
| TC-09.1-01 | FR-09.1 | Verify: The system shall allow a student to select a target career from a predefined list. | Valid request | Requirement satisfied | — | Planned |
| TC-09.2-01 | FR-09.2 | Verify: The system shall allow a student to enter a custom target career. | Valid request | Requirement satisfied | — | Planned |
| TC-09.3-01 | FR-09.3 | Verify: The system shall generate a career roadmap with required skills, learning steps, and miles | Valid request | Requirement satisfied | — | Planned |
| TC-09.4-01 | FR-09.4 | Verify: The system shall allow a student to track progress toward completing career roadmap milest | Valid request | Requirement satisfied | — | Planned |
| TC-09.5-01 | FR-09.5 | Verify: The system shall allow a student to update their target career. | Valid request | Requirement satisfied | — | Planned |
| TC-09.6-01 | FR-09.6 | Verify: The system shall regenerate the career roadmap after the target career is updated. | Valid request | Requirement satisfied | — | Planned |

## SkillService

| ID | Requirement | Description | Input | Expected Result | Actual Result | Status |
|---|---|---|---|---|---|---|
| TC-10.1-01 | FR-10.1 | Verify: The system shall allow a student to add technical and soft skills with current skill level | Valid request | Requirement satisfied | — | Planned |
| TC-10.2-01 | FR-10.2 | Verify: The system shall compare a student's skills against the required skills of their target ca | Valid request | Requirement satisfied | — | Planned |
| TC-10.3-01 | FR-10.3 | Verify: The system shall identify skill gaps based on career requirements. | Valid request | Requirement satisfied | — | Planned |
| TC-10.4-01 | FR-10.4 | Verify: The system shall allow a student to update skill levels as they progress. | Valid request | Requirement satisfied | — | Planned |

## PortfolioService

| ID | Requirement | Description | Input | Expected Result | Actual Result | Status |
|---|---|---|---|---|---|---|
| TC-11.1-01 | FR-11.1 | Verify: The system shall allow a student to add a project with title, description, technologies, a | Valid request | Requirement satisfied | — | Planned |
| TC-11.2-01 | FR-11.2 | Verify: The system shall allow a student to link projects to the skills they demonstrate. | Valid request | Requirement satisfied | — | Planned |
| TC-11.3-01 | FR-11.3 | Verify: The system shall generate a portfolio view of the student's completed projects. | Valid request | Requirement satisfied | — | Planned |
| TC-11.4-01 | FR-11.4 | Verify: The system shall allow a student to add project links and documentation to their portfolio | Valid request | Requirement satisfied | — | Planned |

## JobPreparationService

| ID | Requirement | Description | Input | Expected Result | Actual Result | Status |
|---|---|---|---|---|---|---|
| TC-12.1-01 | FR-12.1 | Verify: The system shall allow a student to track internship and job applications with company, po | Valid request | Requirement satisfied | — | Planned |
| TC-12.2-01 | FR-12.2 | Verify: The system shall allow a student to track interview preparation tasks for each application | Valid request | Requirement satisfied | — | Planned |
| TC-12.3-01 | FR-12.3 | Verify: The system shall allow a student to track CV updates related to applications. | Valid request | Requirement satisfied | — | Planned |
| TC-12.4-01 | FR-12.4 | Verify: The system shall allow a student to track portfolio updates related to applications. | Valid request | Requirement satisfied | — | Planned |
| TC-12.5-01 | FR-12.5 | Verify: The system shall allow a student to record the outcome of an application. | Valid request | Requirement satisfied | — | Planned |
| TC-12.6-01 | FR-12.6 | Verify: The system shall allow a student to record feedback received for an application. | Valid request | Requirement satisfied | — | Planned |


\newpage

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


\newpage

# 2. Test Cases — Muhammed Hany Hamdy

Owner: `muhammedhanii`. Status/Actual Result are filled from the latest test run (`04-testing-results.md`).

## DashboardService

| ID | Requirement | Description | Input | Expected Result | Actual Result | Status |
|---|---|---|---|---|---|---|
| TC-17.1-01 | FR-17.1 | Verify: The system shall display today's tasks and upcoming deadlines on the student dashboard. | Valid request | Requirement satisfied | — | Planned |
| TC-17.2-01 | FR-17.2 | Verify: The system shall display the current status of each goal on the dashboard. | Valid request | Requirement satisfied | — | Planned |
| TC-17.3-01 | FR-17.3 | Verify: The system shall display the completion percentage of each goal on the dashboard. | Valid request | Requirement satisfied | — | Planned |
| TC-17.4-01 | FR-17.4 | Verify: The system shall display habit streaks and completion status on the dashboard. | Valid request | Requirement satisfied | — | Planned |
| TC-17.5-01 | FR-17.5 | Verify: The system shall display the study workload summary of each active goal on the dashboard. | Valid request | Requirement satisfied | — | Planned |
| TC-17.6-01 | FR-17.6 | Verify: The system shall display career progress on the dashboard. | Valid request | Requirement satisfied | — | Planned |

## NotificationService

| ID | Requirement | Description | Input | Expected Result | Actual Result | Status |
|---|---|---|---|---|---|---|
| TC-18.1-01 | FR-18.1 | Verify: The system shall send reminders for upcoming assignments and exam deadlines. | Valid request | Requirement satisfied | — | Planned |
| TC-18.2-01 | FR-18.2 | Verify: The system shall send reminders for scheduled study tasks. | Valid request | Requirement satisfied | — | Planned |
| TC-18.3-01 | FR-18.3 | Verify: The system shall send reminders for scheduled habits. | Valid request | Requirement satisfied | — | Planned |
| TC-18.4-01 | FR-18.4 | Verify: The system shall send early warning notifications when a student is at risk of missing dea | Valid request | Requirement satisfied | — | Planned |
| TC-18.5-01 | FR-18.5 | Verify: The system shall send goal milestone reminders. | Valid request | Requirement satisfied | — | Planned |
| TC-18.6-01 | FR-18.6 | Verify: The system shall notify a student when a habit streak reaches a new milestone. | Valid request | Requirement satisfied | — | Planned |

## StudyGroupService

| ID | Requirement | Description | Input | Expected Result | Actual Result | Status |
|---|---|---|---|---|---|---|
| TC-19.1-01 | FR-19.1 | Verify: The system shall allow a student to create a study group. | Valid request | Requirement satisfied | — | Planned |
| TC-19.2-01 | FR-19.2 | Verify: The system shall allow a student to join a study group. | Valid request | Requirement satisfied | — | Planned |
| TC-19.3-01 | FR-19.3 | Verify: The system shall allow study group members to share goals. | Valid request | Requirement satisfied | — | Planned |
| TC-19.4-01 | FR-19.4 | Verify: The system shall allow study group members to track group progress. | Valid request | Requirement satisfied | — | Planned |
| TC-19.5-01 | FR-19.5 | Verify: The system shall allow a student to find an accountability partner. | Valid request | Requirement satisfied | — | Planned |
| TC-19.6-01 | FR-19.6 | Verify: The system shall allow a student to connect with an accountability partner. | Valid request | Requirement satisfied | — | Planned |
| TC-19.7-01 | FR-19.7 | Verify: The system shall allow study groups to share progress updates. | Valid request | Requirement satisfied | — | Planned |
| TC-19.8-01 | FR-19.8 | Verify: The system shall allow study groups to share support messages. | Valid request | Requirement satisfied | — | Planned |

## AdministrationService

| ID | Requirement | Description | Input | Expected Result | Actual Result | Status |
|---|---|---|---|---|---|---|
| TC-20.1-01 | FR-20.1 | Verify: The system shall allow an administrator to view student account details. | Valid request | Requirement satisfied | — | Planned |
| TC-20.2-01 | FR-20.2 | Verify: The system shall allow an administrator to create a student account. | Valid request | Requirement satisfied | — | Planned |
| TC-20.3-01 | FR-20.3 | Verify: The system shall allow an administrator to update student account details. | Valid request | Requirement satisfied | — | Planned |
| TC-20.4-01 | FR-20.4 | Verify: The system shall allow an administrator to suspend a student account. | Valid request | Requirement satisfied | — | Planned |
| TC-20.5-01 | FR-20.5 | Verify: The system shall provide usage statistics showing the number of registered students. | Valid request | Requirement satisfied | — | Planned |
| TC-20.6-01 | FR-20.6 | Verify: The system shall provide usage statistics showing the number of active students. | Valid request | Requirement satisfied | — | Planned |
| TC-20.7-01 | FR-20.7 | Verify: The system shall provide usage statistics showing platform feature usage. | Valid request | Requirement satisfied | — | Planned |
| TC-20.8-01 | FR-20.8 | Verify: The system shall allow an administrator to investigate student account issues. | Valid request | Requirement satisfied | — | Planned |
| TC-20.9-01 | FR-20.9 | Verify: The system shall allow an administrator to resolve student account issues. | Valid request | Requirement satisfied | — | Planned |
| TC-20.10-01 | FR-20.10 | Verify: The system shall display platform retention metrics showing student activity over time. | Valid request | Requirement satisfied | — | Planned |


\newpage

# 3. Testing Code

Test folder: https://github.com/AIU-SoftWave/Aspirra/tree/develop/tests

Framework: xUnit with Moq (the .NET equivalents of JUnit and Mockito). Every test method name carries its test case ID: `TC-02.1-01` becomes `TC_02_1_01_<Scenario>`.

| Service | Test project |
|---|---|
| AcademicService | [AcademicService.Tests](https://github.com/AIU-SoftWave/Aspirra/tree/develop/tests/AcademicService.Tests) |
| AssessmentService | [AssessmentService.Tests](https://github.com/AIU-SoftWave/Aspirra/tree/develop/tests/AssessmentService.Tests) |
| CourseService | [CourseService.Tests](https://github.com/AIU-SoftWave/Aspirra/tree/develop/tests/CourseService.Tests) |
| GoalPlannerService | [GoalPlannerService.Tests](https://github.com/AIU-SoftWave/Aspirra/tree/develop/tests/GoalPlannerService.Tests) |
| HabitService | [HabitService.Tests](https://github.com/AIU-SoftWave/Aspirra/tree/develop/tests/HabitService.Tests) |
| StudentService | [StudentService.Tests](https://github.com/AIU-SoftWave/Aspirra/tree/develop/tests/StudentService.Tests) |

## Test case → method map

| TC ID | FR | Method | Source |
|-----|-----|----------------------------------------|---------------|
| TC-02.1-01 | FR-02.1 | `TC_02_1_01_Health_ReturnsHealthy` | [AcademicApiTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/AcademicService.Tests/AcademicApiTests.cs) |
| TC-02.1-02 | FR-02.1 | `TC_02_1_02_CreateGoal_ReturnsCreatedGoalAndCanBeRetrieved` | [AcademicApiTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/AcademicService.Tests/AcademicApiTests.cs) |
| TC-02.1-03 | FR-02.1 | `TC_02_1_03_CreateGoal_MissingTitle_ReturnsRequiredFieldPayload` | [AcademicApiTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/AcademicService.Tests/AcademicApiTests.cs) |
| TC-02.1-04 | FR-02.1 | `TC_02_1_04_CreateGoal_WithoutStudentId_DoesNotPersist` | [GoalMoqTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/AcademicService.Tests/GoalMoqTests.cs) |
| TC-02.1-05 | FR-02.1 | `TC_02_1_05_CreateGoal_WithValidData_SetsStatusActive` | [GoalServiceTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/AcademicService.Tests/GoalServiceTests.cs) |
| TC-02.1-06 | FR-02.1 | `TC_02_1_06_CreateGoal_MissingTitle_ReturnsRequiredFieldError` | [GoalServiceTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/AcademicService.Tests/GoalServiceTests.cs) |
| TC-02.1-07 | FR-02.1 | `TC_02_1_07_CreateGoal_MissingPriority_ReturnsRequiredFieldError` | [GoalServiceTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/AcademicService.Tests/GoalServiceTests.cs) |
| TC-02.1-08 | FR-02.1 | `TC_02_1_08_CreateGoal_PastTargetDate_ReturnsInvalidDateError` | [GoalServiceTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/AcademicService.Tests/GoalServiceTests.cs) |
| TC-02.1-09 | FR-02.1 | `TC_02_1_09_GetGoals_FiltersByStudentAndStatus` | [GoalServiceTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/AcademicService.Tests/GoalServiceTests.cs) |
| TC-02.1-10 | FR-02.1 | `TC_02_1_10_GetGoal_NotFound_ReturnsNotFound` | [GoalServiceTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/AcademicService.Tests/GoalServiceTests.cs) |
| TC-02.2-01 | FR-02.2 | `TC_02_2_01_UpdateGoal_ChangesFields` | [GoalServiceTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/AcademicService.Tests/GoalServiceTests.cs) |
| TC-02.2-02 | FR-02.2 | `TC_02_2_02_UpdateGoal_MissingTitle_KeepsOldValues` | [GoalServiceTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/AcademicService.Tests/GoalServiceTests.cs) |
| TC-02.3-01 | FR-02.3 | `TC_02_3_01_UpdateGoal_PastTargetDate_KeepsOldValues` | [GoalServiceTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/AcademicService.Tests/GoalServiceTests.cs) |
| TC-02.4-01 | FR-02.4 | `TC_02_4_01_CompleteGoal_WhenArchived_ReturnsInvalidTransition` | [AcademicApiTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/AcademicService.Tests/AcademicApiTests.cs) |
| TC-02.4-02 | FR-02.4 | `TC_02_4_02_CompleteGoal_WhenMissing_ReturnsNotFound` | [AcademicApiTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/AcademicService.Tests/AcademicApiTests.cs) |
| TC-02.4-03 | FR-02.4 | `TC_02_4_03_CompleteGoal_FromActive_PersistsExactlyOnce` | [GoalMoqTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/AcademicService.Tests/GoalMoqTests.cs) |
| TC-02.4-04 | FR-02.4 | `TC_02_4_04_CompleteGoal_FromArchived_DoesNotPersist` | [GoalMoqTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/AcademicService.Tests/GoalMoqTests.cs) |
| TC-02.4-05 | FR-02.4 | `TC_02_4_05_CompleteGoal_SetsCompletedAndCompletionDate` | [GoalServiceTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/AcademicService.Tests/GoalServiceTests.cs) |
| TC-02.4-06 | FR-02.4 | `TC_02_4_06_CompleteGoal_Twice_KeepsFirstCompletionDate` | [GoalServiceTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/AcademicService.Tests/GoalServiceTests.cs) |
| TC-02.4-07 | FR-02.4 | `TC_02_4_07_CompleteGoal_FromActive_ReturnsCompleted` | [GoalStateTransitionTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/AcademicService.Tests/GoalStateTransitionTests.cs) |
| TC-02.4-08 | FR-02.4 | `TC_02_4_08_CompleteGoal_FromCompleted_IsIdempotent` | [GoalStateTransitionTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/AcademicService.Tests/GoalStateTransitionTests.cs) |
| TC-02.4-09 | FR-02.4 | `TC_02_4_09_CompleteGoal_FromArchived_ThrowsInvalidTransitionAndDoesNotUpdate` | [GoalStateTransitionTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/AcademicService.Tests/GoalStateTransitionTests.cs) |
| TC-02.4-10 | FR-02.4 | `TC_02_4_10_CompleteGoal_MissingGoal_ThrowsNotFound` | [GoalStateTransitionTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/AcademicService.Tests/GoalStateTransitionTests.cs) |
| TC-02.4-11 | FR-02.4 | `TC_02_4_11_CompleteGoal_InvalidTransition_ReturnsBadRequestWithCode` | [GoalStateTransitionTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/AcademicService.Tests/GoalStateTransitionTests.cs) |
| TC-02.4-12 | FR-02.4 | `TC_02_4_12_CompleteGoal_MissingGoal_ReturnsNotFound` | [GoalStateTransitionTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/AcademicService.Tests/GoalStateTransitionTests.cs) |
| TC-02.5-01 | FR-02.5 | `TC_02_5_01_RestoreGoal_FromArchived_ClearsCompletionDateAndPersists` | [GoalMoqTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/AcademicService.Tests/GoalMoqTests.cs) |
| TC-02.5-02 | FR-02.5 | `TC_02_5_02_ArchiveAndRestoreGoal_ChangesStatus` | [GoalServiceTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/AcademicService.Tests/GoalServiceTests.cs) |
| TC-02.5-03 | FR-02.5 | `TC_02_5_03_ArchiveGoal_FromActive_ReturnsArchived` | [GoalStateTransitionTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/AcademicService.Tests/GoalStateTransitionTests.cs) |
| TC-02.5-04 | FR-02.5 | `TC_02_5_04_ArchiveGoal_FromCompleted_ReturnsArchived` | [GoalStateTransitionTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/AcademicService.Tests/GoalStateTransitionTests.cs) |
| TC-02.5-05 | FR-02.5 | `TC_02_5_05_RestoreGoal_FromArchived_ReturnsActive` | [GoalStateTransitionTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/AcademicService.Tests/GoalStateTransitionTests.cs) |
| TC-02.5-06 | FR-02.5 | `TC_02_5_06_RestoreGoal_AfterArchivedCompletedGoal_ClearsCompletionDate` | [GoalStateTransitionTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/AcademicService.Tests/GoalStateTransitionTests.cs) |
| TC-02.5-07 | FR-02.5 | `TC_02_5_07_RestoreGoal_FromActive_ThrowsInvalidTransitionAndDoesNotUpdate` | [GoalStateTransitionTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/AcademicService.Tests/GoalStateTransitionTests.cs) |
| TC-02.5-08 | FR-02.5 | `TC_02_5_08_RestoreGoal_FromCompleted_ThrowsInvalidTransitionAndDoesNotUpdate` | [GoalStateTransitionTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/AcademicService.Tests/GoalStateTransitionTests.cs) |
| TC-02.5-09 | FR-02.5 | `TC_02_5_09_ArchiveGoal_MissingGoal_ThrowsNotFound` | [GoalStateTransitionTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/AcademicService.Tests/GoalStateTransitionTests.cs) |
| TC-02.5-10 | FR-02.5 | `TC_02_5_10_RestoreGoal_MissingGoal_ThrowsNotFound` | [GoalStateTransitionTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/AcademicService.Tests/GoalStateTransitionTests.cs) |
| TC-02.5-11 | FR-02.5 | `TC_02_5_11_RestoreGoal_InvalidTransition_ReturnsBadRequestWithCode` | [GoalStateTransitionTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/AcademicService.Tests/GoalStateTransitionTests.cs) |
| TC-02.6-01 | FR-02.6 | `TC_02_6_01_DeleteGoal_RemovesGoal` | [GoalServiceTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/AcademicService.Tests/GoalServiceTests.cs) |
| TC-04.1-01 | FR-04.1 | `TC_04_1_01_AddAssignment_WithRequiredFields_SetsNotStarted` | [AssessmentServiceTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/AssessmentService.Tests/AssessmentServiceTests.cs) |
| TC-04.1-02 | FR-04.1 | `TC_04_1_02_AddAssignment_MissingTitle_ReturnsRequiredFieldError` | [AssessmentServiceTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/AssessmentService.Tests/AssessmentServiceTests.cs) |
| TC-04.1-03 | FR-04.1 | `TC_04_1_03_GetAssignmentsByCourse_ReturnsExpectedShape` | [AssessmentServiceTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/AssessmentService.Tests/AssessmentServiceTests.cs) |
| TC-04.2-01 | FR-04.2 | `TC_04_2_01_AddExam_WithValidData_IsAddedToUpcoming` | [AssessmentServiceTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/AssessmentService.Tests/AssessmentServiceTests.cs) |
| TC-04.2-02 | FR-04.2 | `TC_04_2_02_AddExam_PastDate_ReturnsInvalidDateError` | [AssessmentServiceTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/AssessmentService.Tests/AssessmentServiceTests.cs) |
| TC-04.2-03 | FR-04.2 | `TC_04_2_03_GetAssessments_FiltersByStudent` | [AssessmentServiceTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/AssessmentService.Tests/AssessmentServiceTests.cs) |
| TC-04.3-01 | FR-04.3 | `TC_04_3_01_SetAssignmentStatus_ToCompleted_UpdatesStatus` | [AssessmentServiceTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/AssessmentService.Tests/AssessmentServiceTests.cs) |
| TC-04.3-02 | FR-04.3 | `TC_04_3_02_SetAssignmentStatus_InvalidStatus_ReturnsError` | [AssessmentServiceTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/AssessmentService.Tests/AssessmentServiceTests.cs) |
| TC-04.3-03 | FR-04.3 | `TC_04_3_03_SetAssignmentStatus_AssignmentNotFound_ReturnsNotFound` | [AssessmentServiceTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/AssessmentService.Tests/AssessmentServiceTests.cs) |
| TC-04.4-01 | FR-04.4 | `TC_04_4_01_SetExamReminder_StoresDaysBefore` | [AssessmentServiceTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/AssessmentService.Tests/AssessmentServiceTests.cs) |
| TC-04.4-02 | FR-04.4 | `TC_04_4_02_SetExamReminder_NegativeDays_ReturnsInvalidReminder` | [AssessmentServiceTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/AssessmentService.Tests/AssessmentServiceTests.cs) |
| TC-03.1-01 | FR-03.1 | `TC_03_1_01_AddCourse_MissingRequiredField_ReturnsRequiredFieldError` | [AddCourseTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/CourseService.Tests/AddCourseTests.cs) |
| TC-03.1-02 | FR-03.1 | `TC_03_1_02_AddCourse_DuplicateCodeAndSemester_ReturnsDuplicateError` | [AddCourseTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/CourseService.Tests/AddCourseTests.cs) |
| TC-03.1-03 | FR-03.1 | `TC_03_1_03_AddCourse_SameCodeDifferentSemester_Allowed` | [AddCourseTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/CourseService.Tests/AddCourseTests.cs) |
| TC-03.1-04 | FR-03.1 | `TC_03_1_04_GetCourses_FiltersByStudent` | [GetCoursesTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/CourseService.Tests/GetCoursesTests.cs) |
| TC-03.2-01 | FR-03.2 | `TC_03_2_01_AddCourse_WithValidData_StoresCreditsAndWorkload` | [AddCourseTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/CourseService.Tests/AddCourseTests.cs) |
| TC-03.3-01 | FR-03.3 | `TC_03_3_01_ViewCourseProgress_SeparatesCompletedAndOutstanding` | [ViewCourseProgressTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/CourseService.Tests/ViewCourseProgressTests.cs) |
| TC-03.3-02 | FR-03.3 | `TC_03_3_02_ViewCourseProgress_NoAssignments_ReturnsNoAssignmentsMessage` | [ViewCourseProgressTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/CourseService.Tests/ViewCourseProgressTests.cs) |
| TC-03.3-03 | FR-03.3 | `TC_03_3_03_ViewCourseProgress_CourseNotFound_ReturnsNotFound` | [ViewCourseProgressTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/CourseService.Tests/ViewCourseProgressTests.cs) |
| TC-03.4-01 | FR-03.4 | `TC_03_4_01_UpdateCourse_ChangesFields` | [UpdateCourseTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/CourseService.Tests/UpdateCourseTests.cs) |
| TC-03.5-01 | FR-03.5 | `TC_03_5_01_RemoveCourse_DeletesCourse` | [RemoveCourseTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/CourseService.Tests/RemoveCourseTests.cs) |
| TC-05.1-01 | FR-05.1 | `TC_05_1_01_GeneratePlan_EmptyDescription_ReturnsRequiredFieldError` | [PlansControllerTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/GoalPlannerService.Tests/PlansControllerTests.cs) |
| TC-05.2-01 | FR-05.2 | `TC_05_2_01_GeneratePlan_WithDescription_CreatesDraftWithMilestonesAndTasks` | [PlansControllerTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/GoalPlannerService.Tests/PlansControllerTests.cs) |
| TC-05.2-02 | FR-05.2 | `TC_05_2_02_GeneratePlan_AiUnavailable_ReturnsAiUnavailableError` | [PlansControllerTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/GoalPlannerService.Tests/PlansControllerTests.cs) |
| TC-05.2-03 | FR-05.2 | `TC_05_2_03_GeneratePlan_InvalidStructure_ReturnsPlanInvalidError` | [PlansControllerTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/GoalPlannerService.Tests/PlansControllerTests.cs) |
| TC-05.3-01 | FR-05.3 | `TC_05_3_01_Discard_DeletesDraft` | [PlansControllerTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/GoalPlannerService.Tests/PlansControllerTests.cs) |
| TC-05.3-02 | FR-05.3 | `TC_05_3_02_GetTasks_ReturnsOnlyApprovedPlanTasks` | [PlansControllerTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/GoalPlannerService.Tests/PlansControllerTests.cs) |
| TC-05.3-03 | FR-05.3 | `TC_05_3_03_GetDraft_Missing_ReturnsNotFound` | [PlansControllerTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/GoalPlannerService.Tests/PlansControllerTests.cs) |
| TC-05.3-04 | FR-05.3 | `TC_05_3_04_Discard_Missing_ReturnsNotFound` | [PlansControllerTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/GoalPlannerService.Tests/PlansControllerTests.cs) |
| TC-05.4-01 | FR-05.4 | `TC_05_4_01_UpdateDraft_EditsMilestonesAndTasks` | [PlansControllerTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/GoalPlannerService.Tests/PlansControllerTests.cs) |
| TC-05.5-01 | FR-05.5 | `TC_05_5_01_UpdateDraft_ApprovedPlan_ReturnsNotEditable` | [PlansControllerTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/GoalPlannerService.Tests/PlansControllerTests.cs) |
| TC-05.6-01 | FR-05.6 | `TC_05_6_01_Approve_WithExistingGoal_ApprovesAndSavesOnce` | [GoalPlannerMoqTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/GoalPlannerService.Tests/GoalPlannerMoqTests.cs) |
| TC-05.6-02 | FR-05.6 | `TC_05_6_02_Approve_WhenGoalDoesNotExist_ReturnsGoalNotFoundAndDoesNotSave` | [GoalPlannerMoqTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/GoalPlannerService.Tests/GoalPlannerMoqTests.cs) |
| TC-05.6-03 | FR-05.6 | `TC_05_6_03_Approve_WhenDraftMissing_ReturnsNotFound` | [GoalPlannerMoqTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/GoalPlannerService.Tests/GoalPlannerMoqTests.cs) |
| TC-05.6-04 | FR-05.6 | `TC_05_6_04_Approve_ValidDraft_SetsApproved` | [PlansControllerTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/GoalPlannerService.Tests/PlansControllerTests.cs) |
| TC-05.6-05 | FR-05.6 | `TC_05_6_05_Approve_GoalMissing_ReturnsGoalNotFound` | [PlansControllerTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/GoalPlannerService.Tests/PlansControllerTests.cs) |
| TC-05.6-06 | FR-05.6 | `TC_05_6_06_Approve_AlreadyApproved_IsIdempotent` | [PlansControllerTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/GoalPlannerService.Tests/PlansControllerTests.cs) |
| TC-13.1-01 | FR-13.1 | `TC_13_1_01_CreateHabit_WithValidData_SetsZeroStreak` | [HabitServiceTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/HabitService.Tests/HabitServiceTests.cs) |
| TC-13.1-02 | FR-13.1 | `TC_13_1_02_CreateHabit_MissingName_ReturnsRequiredFieldError` | [HabitServiceTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/HabitService.Tests/HabitServiceTests.cs) |
| TC-13.1-03 | FR-13.1 | `TC_13_1_03_CreateHabit_InvalidFrequency_ReturnsInvalidFrequencyError` | [HabitServiceTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/HabitService.Tests/HabitServiceTests.cs) |
| TC-13.2-01 | FR-13.2 | `TC_13_2_01_CompleteHabit_WhenNoLogExists_AddsLogAndSaves` | [HabitMoqTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/HabitService.Tests/HabitMoqTests.cs) |
| TC-13.2-02 | FR-13.2 | `TC_13_2_02_CompleteHabit_WhenLogAlreadyCompleted_DoesNotAddLog` | [HabitMoqTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/HabitService.Tests/HabitMoqTests.cs) |
| TC-13.2-03 | FR-13.2 | `TC_13_2_03_CompleteHabit_WhenHabitMissing_ReturnsNotFoundAndDoesNotPersist` | [HabitMoqTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/HabitService.Tests/HabitMoqTests.cs) |
| TC-13.3-01 | FR-13.3 | `TC_13_3_01_CompleteHabit_FirstTime_SetsStreakToOne` | [HabitServiceTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/HabitService.Tests/HabitServiceTests.cs) |
| TC-13.3-02 | FR-13.3 | `TC_13_3_02_CompleteHabit_ConsecutiveDays_IncrementsStreak` | [HabitServiceTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/HabitService.Tests/HabitServiceTests.cs) |
| TC-13.3-03 | FR-13.3 | `TC_13_3_03_CompleteHabit_MissedScheduledDay_ResetsStreakToOne` | [HabitServiceTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/HabitService.Tests/HabitServiceTests.cs) |
| TC-13.3-04 | FR-13.3 | `TC_13_3_04_CompleteHabit_SameDayTwice_DoesNotIncrementStreak` | [HabitServiceTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/HabitService.Tests/HabitServiceTests.cs) |
| TC-13.3-05 | FR-13.3 | `TC_13_3_05_GetStreaks_ShowsDoneToday` | [HabitServiceTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/HabitService.Tests/HabitServiceTests.cs) |
| TC-13.4-01 | FR-13.4 | `TC_13_4_01_LinkHabitToGoal_StoresGoalId` | [HabitServiceTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/HabitService.Tests/HabitServiceTests.cs) |
| TC-13.4-02 | FR-13.4 | `TC_13_4_02_LinkHabitToGoal_HabitNotFound_ReturnsNotFound` | [HabitServiceTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/HabitService.Tests/HabitServiceTests.cs) |
| TC-01.1-01 | FR-01.1 | `TC_01_1_01_CreateProfile_WithRequiredFields_CreatesProfile` | [CreateProfileTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/StudentService.Tests/CreateProfileTests.cs) |
| TC-01.1-02 | FR-01.1 | `TC_01_1_02_CreateProfile_Duplicate_ReturnsDuplicateError` | [CreateProfileTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/StudentService.Tests/CreateProfileTests.cs) |
| TC-01.2-01 | FR-01.2 | `TC_01_2_01_UpdateProfile_ChangesFields` | [UpdateProfileTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/StudentService.Tests/UpdateProfileTests.cs) |
| TC-01.2-02 | FR-01.2 | `TC_01_2_02_UpdateProfile_ClearingRequiredField_KeepsOldValues` | [UpdateProfileTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/StudentService.Tests/UpdateProfileTests.cs) |
| TC-01.2-03 | FR-01.2 | `TC_01_2_03_UpdateProfile_ChangesAvailability` | [UpdateProfileTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/StudentService.Tests/UpdateProfileTests.cs) |
| TC-01.2-04 | FR-01.2 | `TC_01_2_04_UpdateProfile_NotFound_ReturnsNotFound` | [UpdateProfileTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/StudentService.Tests/UpdateProfileTests.cs) |
| TC-01.3-01 | FR-01.3 | `TC_01_3_01_ViewProfile_ReturnsAllFields` | [ViewProfileTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/StudentService.Tests/ViewProfileTests.cs) |
| TC-01.3-02 | FR-01.3 | `TC_01_3_02_ViewProfile_OptionalFieldMissing_ShowsNotSet` | [ViewProfileTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/StudentService.Tests/ViewProfileTests.cs) |
| TC-01.3-03 | FR-01.3 | `TC_01_3_03_GetProfile_NotFound_ReturnsNotFound` | [ViewProfileTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/StudentService.Tests/ViewProfileTests.cs) |
| TC-01.3-04 | FR-01.3 | `TC_01_3_04_GetAvailability_ReturnsSlots` | [ViewProfileTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/StudentService.Tests/ViewProfileTests.cs) |
| TC-01.3-05 | FR-01.3 | `TC_01_3_05_GetAvailability_NotFound_ReturnsNotFound` | [ViewProfileTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/StudentService.Tests/ViewProfileTests.cs) |
| TC-01.4-01 | FR-01.4 | `TC_01_4_01_CreateProfile_MissingAcademicLevel_ReturnsRequiredFieldError` | [CreateProfileTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/StudentService.Tests/CreateProfileTests.cs) |
| TC-01.4-02 | FR-01.4 | `TC_01_4_02_CreateProfile_EndBeforeStart_ReturnsInvalidTimeSlot` | [CreateProfileTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/StudentService.Tests/CreateProfileTests.cs) |
| TC-01.4-03 | FR-01.4 | `TC_01_4_03_CreateProfile_MissingMajor_ReturnsRequiredFieldError` | [CreateProfileTests.cs](https://github.com/AIU-SoftWave/Aspirra/blob/develop/tests/StudentService.Tests/CreateProfileTests.cs) |


\newpage

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

