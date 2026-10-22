# C8 — Product backlog

> Issue: #8 · Part: C · Owner: Muhammed Hany Hamdy · Reviewer: TBD · Status: Draft

## Content

Each high-level FR is one feature (one epic in Jira), broken into user stories numbered under it as in the lab notation (FR-02 → US-02.1, US-02.2, …). **Covers** lists the low-level FRs each story implements; a low-level FR that is a system rule (e.g. FR-01.4 "validate required fields") is written as an acceptance criterion of its story. **Priority** (High = MVP, then Medium, then Low) is the order used in the sprint plan (C9).

### FR-01 — Manage Student Profile
![FR-01 user stories](assets/c8-figures/FR-01-manage-student-profile.png)


| Story | User story | Acceptance criteria | Covers | Priority |
|---|---|---|---|---|
| US-01.1 Create Profile Story | As a student, I need to create my profile with my academic level, major, interests, skills, career interests and availability, so that the system can plan around my situation. | • Given all required fields are filled, when the student clicks "Save", then the profile is created and a confirmation message is shown.<br>• Given a required field is empty, when the student clicks "Save", then a required-field message is shown and nothing is saved.<br>• Given an availability slot whose end time is before its start time, when the student saves, then the error "Invalid time slot" is shown. | FR-01.1, FR-01.4 | High |
| US-01.2 Update Profile Story | As a student, I need to update my profile information, so that my plans use my current availability and career interests. | • Given an existing profile, when the student changes a field and clicks "Save", then the new value is stored and shown.<br>• Given a required field is cleared, when the student saves, then a required-field message is shown and the old value is kept.<br>• Given the availability is changed, when it is saved, then the new availability is used by the next generated plan. | FR-01.2, FR-01.4 | High |
| US-01.3 View Profile Story | As a student, I need to view my complete profile, so that I can check that my information is correct. | • Given a saved profile, when the student opens "My Profile", then all profile fields are shown.<br>• Given an optional field was never filled, when the profile is shown, then the field shows "Not set". | FR-01.3 | High |

### FR-02 — Manage Academic Goals
![FR-02 user stories](assets/c8-figures/FR-02-manage-academic-goals.png)


| Story | User story | Acceptance criteria | Covers | Priority |
|---|---|---|---|---|
| US-02.1 Create Goal Story | As a student, I need to create an academic goal with a title, description, target date and priority, so that I know what I am working towards and by when. | • Given a title, a target date and a priority, when the student clicks "Create", then the goal is saved with status "Active".<br>• Given the title or target date is empty, when the student clicks "Create", then a required-field message is shown.<br>• Given a target date in the past, when the student clicks "Create", then the error "Target date must be in the future" is shown. | FR-02.1 | High |
| US-02.2 Edit Goal Story | As a student, I need to edit the details and the target date of my goal, so that it stays correct when my plans change. | • Given an active goal, when the student changes its title, description or priority and saves, then the new details are shown.<br>• Given an active goal, when the student sets a new future target date, then the goal uses the new date.<br>• Given a new target date in the past, when the student saves, then an error is shown and the old date is kept. | FR-02.2, FR-02.3 | High |
| US-02.3 Complete Goal Story | As a student, I need to mark a goal as completed, so that I can see what I have achieved. | • Given an active goal, when the student clicks "Mark as completed", then its status changes to "Completed" with the completion date.<br>• Given a completed goal, when the goal list is shown, then it appears under "Completed". | FR-02.4 | High |
| US-02.4 Archive Goal Story | As a student, I need to archive a goal, so that old goals do not fill my active list. | • Given a goal, when the student clicks "Archive", then it is removed from the active list and shown under "Archived".<br>• Given an archived goal, when the student clicks "Restore", then it returns to the active list. | FR-02.5 | Medium |
| US-02.5 Delete Goal Story | As a student, I need to delete a goal I created, so that goals I added by mistake are removed. | • Given a goal, when the student clicks "Delete" and confirms, then the goal is permanently removed.<br>• Given the confirmation dialog, when the student clicks "Cancel", then the goal is kept. | FR-02.6 | Medium |

### FR-03 — Manage Courses
![FR-03 user stories](assets/c8-figures/FR-03-manage-courses.png)


| Story | User story | Acceptance criteria | Covers | Priority |
|---|---|---|---|---|
| US-03.1 Add Course Story | As a student, I need to add a course with its name, code, instructor, semester, credits and weekly workload, so that my courses are part of my plan. | • Given a name, code and semester, when the student clicks "Add", then the course appears in the course list.<br>• Given credits and weekly workload hours, when the course is saved, then they are stored with the course.<br>• Given a course code that already exists in the same semester, when the student clicks "Add", then the error "Course already exists" is shown. | FR-03.1, FR-03.2 | High |
| US-03.2 View Course Progress Story | As a student, I need to view the progress of a course, so that I know which of its assignments are done and which are still outstanding. | • Given a course with assignments, when the student opens it, then completed and outstanding assignments are listed separately.<br>• Given a course with no assignments, when it is opened, then the message "No assignments yet" is shown. | FR-03.3 | High |
| US-03.3 Update Course Story | As a student, I need to update a course's information, so that it stays correct. | • Given an existing course, when the student edits a field and saves, then the new value is shown.<br>• Given the code is changed to one that already exists, when the student saves, then an error is shown. | FR-03.4 | Medium |
| US-03.4 Remove Course Story | As a student, I need to remove a course, so that dropped courses are no longer in my plan. | • Given a course, when the student clicks "Remove" and confirms, then it is removed from the course list.<br>• Given the course has assignments, when it is removed, then the student is warned that its assignments will also be removed. | FR-03.5 | Medium |

### FR-04 — Manage Assignments and Exams
![FR-04 user stories](assets/c8-figures/FR-04-manage-assignments-and-exams.png)


| Story | User story | Acceptance criteria | Covers | Priority |
|---|---|---|---|---|
| US-04.1 Add Assignment Story | As a student, I need to add an assignment with its title, course, due date, priority and description, so that I know what is due and when. | • Given a title, course and due date, when the student clicks "Add", then the assignment is saved with status "Not started".<br>• Given the title, course or due date is empty, when the student clicks "Add", then a required-field message is shown. | FR-04.1 | High |
| US-04.2 Add Exam Story | As a student, I need to add an exam with its title, course, date, time and location, so that I can prepare for it in time. | • Given a title, course, date and time, when the student clicks "Add", then the exam appears in the upcoming exams list.<br>• Given an exam date in the past, when the student clicks "Add", then an error is shown. | FR-04.2 | High |
| US-04.3 Track Assignment Status Story | As a student, I need to set an assignment's status to not started, in progress or completed, so that I can follow my progress. | • Given an assignment, when the student changes its status, then the new status is saved and shown.<br>• Given an assignment is set to "Completed", when its course progress is shown, then it is counted as completed. | FR-04.3 | High |
| US-04.4 Exam Preparation Reminder Story | As a student, I need to set preparation reminders for my upcoming exams, so that I start studying early enough. | • Given an upcoming exam, when the student sets a reminder (e.g. 3 days before), then the reminder is saved with the exam.<br>• Given a reminder time after the exam date, when the student saves, then an error is shown. | FR-04.4 | Medium |

### FR-05 — Plan Goals with AI
![FR-05 user stories](assets/c8-figures/FR-05-plan-goals-with-ai.png)


| Story | User story | Acceptance criteria | Covers | Priority |
|---|---|---|---|---|
| US-05.1 Generate Goal Plan Story | As a student, I need to describe my goal in my own words and get a plan with milestones, tasks and deadlines, so that I do not have to plan everything myself. | • Given a goal description, when the student clicks "Generate plan", then milestones, tasks and deadlines are shown.<br>• Given an empty description, when the student clicks "Generate plan", then a required-field message is shown.<br>• Given the AI service is unavailable, when the student clicks "Generate plan", then the message "Planning is unavailable, try again later" is shown. | FR-05.1, FR-05.2 | High |
| US-05.2 Review and Edit AI Plan Story | As a student, I need to review and edit the milestones and tasks the AI generated, so that the plan fits my real schedule. | • Given a generated plan, when it is shown, then nothing is saved until the student approves it.<br>• Given a generated milestone, when the student edits or deletes it, then the change appears in the plan.<br>• Given a generated task, when the student edits its title, duration or deadline, then the change appears in the plan. | FR-05.3, FR-05.4, FR-05.5 | High |
| US-05.3 Approve AI Plan Story | As a student, I need to approve the AI plan before it is added to my goals, so that nothing changes without my consent. | • Given a reviewed plan, when the student clicks "Approve", then its milestones and tasks are added to the goal.<br>• Given a reviewed plan, when the student clicks "Discard", then nothing is saved. | FR-05.6 | High |

### FR-06 — Generate Study Plans
![FR-06 user stories](assets/c8-figures/FR-06-generate-study-plans.png)


| Story | User story | Acceptance criteria | Covers | Priority |
|---|---|---|---|---|
| US-06.1 Generate Study Plan Story | As a student, I need a personalized study plan built from my courses, deadlines and availability, so that I know what to study and when. | • Given courses, deadlines and availability, when the student clicks "Generate study plan", then study tasks are placed only inside the available time slots.<br>• Given two tasks compete for one slot, when the plan is generated, then the higher-priority task gets the slot.<br>• Given the AI returns a plan that does not match the expected structure, when it is received, then it is rejected and the message "Plan could not be generated" is shown.<br>• Given a generated plan is rejected, when the student opens the plan page, then the previously saved plan is still shown. | FR-06.1, FR-06.2, FR-06.4, FR-06.5 | High |
| US-06.2 Regenerate Study Plan Story | As a student, I need to regenerate my study plan after my availability or a deadline changes, so that the plan stays realistic. | • Given the availability or a deadline has changed, when the student clicks "Regenerate", then a new plan is created from the new data.<br>• Given the new plan is rejected as invalid, when regeneration ends, then the previous plan is kept. | FR-06.3 | Medium |

### FR-07 — Enable Smart Scheduling
![FR-07 user stories](assets/c8-figures/FR-07-enable-smart-scheduling.png)


| Story | User story | Acceptance criteria | Covers | Priority |
|---|---|---|---|---|
| US-07.1 Auto-Schedule Tasks Story | As a student, I need my tasks scheduled automatically by deadline, priority, workload, availability and dependencies, so that I get a realistic weekly schedule. | • Given tasks with deadlines and priorities, when scheduling runs, then each task is placed before its deadline inside the available slots.<br>• Given task B depends on task A, when scheduling runs, then B is placed after A.<br>• Given a task cannot fit before its deadline, when scheduling runs, then the task is marked "Unscheduled" and shown to the student. | FR-07.1 | High |
| US-07.2 Resolve Scheduling Conflicts Story | As a student, I need to see scheduling conflicts and get alternative time slots, so that I know what to do when tasks collide. | • Given two tasks in overlapping slots, when the schedule is shown, then both tasks are marked as a conflict.<br>• Given a conflict, when the student opens it, then at least one free alternative slot is suggested.<br>• Given the student picks a suggested slot, when it is confirmed, then the conflict is removed. | FR-07.2, FR-07.3 | High |
| US-07.3 Adjust Schedule Manually Story | As a student, I need to move scheduled tasks and change their times myself, so that I stay in control of my schedule. | • Given a scheduled task, when the student moves it to another free slot, then the new time is saved.<br>• Given the student moves a task to an occupied slot, when it is dropped, then a conflict warning is shown. | FR-07.4 | Medium |
| US-07.4 Mark Task Done Story | As a student, I need to mark a scheduled task as done, so that my progress and remaining workload stay up to date. | • Given a scheduled task, when the student marks it as done, then it is shown as completed.<br>• Given a task is marked as done, when the workload of its goal is calculated, then the task no longer counts as remaining workload.<br>• Given a completed task, when the student marks it as not done, then it counts as remaining workload again. | FR-07.5 | High |

### FR-08 — Enable Adaptive Planning
![FR-08 user stories](assets/c8-figures/FR-08-enable-adaptive-planning.png)


| Story | User story | Acceptance criteria | Covers | Priority |
|---|---|---|---|---|
| US-08.1 Detect Falling Behind Story | As a student, I need the system to detect when I am falling behind, so that I can react before I miss a deadline. | • Given a task whose scheduled time has passed and is not completed, when the plan is checked, then the goal is marked "Behind schedule".<br>• Given all past tasks are completed, when the plan is checked, then the goal is marked "On track". | FR-08.1 | High |
| US-08.2 Get Re-planning Recommendations Story | As a student who is falling behind, I need recommendations to reschedule and reprioritize my tasks, so that I can still meet my deadlines. | • Given a goal marked "Behind schedule", when the student opens it, then rescheduling recommendations are shown with the new times.<br>• Given several late tasks, when recommendations are made, then the tasks with the nearest deadlines are moved to the top of the list. | FR-08.2, FR-08.3 | Medium |
| US-08.3 Respond to Recommendations Story | As a student, I need to accept, reject or modify each recommendation, so that my plan only changes when I approve it. | • Given a recommendation, when the student clicks "Accept", then the plan is updated.<br>• Given a recommendation, when the student clicks "Reject", then the plan is not changed.<br>• Given a recommendation, when the student edits it and accepts, then the edited version is applied.<br>• Given the student has not responded, when the plan is shown, then no recommendation has been applied. | FR-08.4, FR-08.5, FR-08.6, FR-08.7 | Medium |

### FR-09 — Manage Career Roadmaps
![FR-09 user stories](assets/c8-figures/FR-09-manage-career-roadmaps.png)


| Story | User story | Acceptance criteria | Covers | Priority |
|---|---|---|---|---|
| US-09.1 Choose Target Career Story | As a senior student, I need to choose my target career from a list or enter my own, so that my preparation is aimed at the right job. | • Given the career list, when the student selects a career and saves, then it is set as the target career.<br>• Given the career is not in the list, when the student enters a custom career name and saves, then it is set as the target career.<br>• Given an empty custom career name, when the student saves, then a required-field message is shown. | FR-09.1, FR-09.2 | Low |
| US-09.2 Generate Career Roadmap Story | As a senior student, I need a roadmap with the required skills, learning steps and milestones for my target career, so that I know what to learn and in what order. | • Given a target career, when the student clicks "Generate roadmap", then required skills, learning steps and milestones are shown.<br>• Given the AI service is unavailable, when the student clicks "Generate roadmap", then an error message is shown and no roadmap is saved. | FR-09.3 | Low |
| US-09.3 Track Roadmap Progress Story | As a senior student, I need to mark roadmap milestones as completed, so that I can see how close I am to being job-ready. | • Given a roadmap milestone, when the student marks it as completed, then the roadmap progress percentage increases.<br>• Given all milestones are completed, when the roadmap is shown, then its progress is 100%. | FR-09.4 | Low |
| US-09.4 Change Target Career Story | As a senior student, I need to change my target career and get a new roadmap, so that my plan follows my new direction. | • Given a target career, when the student selects a different career and confirms, then the target career is updated.<br>• Given the target career was updated, when the change is saved, then a new roadmap is generated for the new career. | FR-09.5, FR-09.6 | Low |

### FR-10 — Track Skills
![FR-10 user stories](assets/c8-figures/FR-10-track-skills.png)


| Story | User story | Acceptance criteria | Covers | Priority |
|---|---|---|---|---|
| US-10.1 Add Skill Story | As a student, I need to add my technical and soft skills with my current level, so that I have a record of what I can do. | • Given a skill name, type (technical / soft) and level, when the student clicks "Add", then the skill appears in the skill list.<br>• Given the same skill already exists, when the student adds it again, then the error "Skill already added" is shown. | FR-10.1 | Low |
| US-10.2 View Skill Gaps Story | As a senior student, I need to compare my skills with the skills my target career requires, so that I know which skills I am missing. | • Given a target career and a skill list, when the student opens "Skill gaps", then each required skill is shown as "Have" or "Missing".<br>• Given a skill whose level is below the required level, when the gaps are shown, then it is marked as a gap.<br>• Given no target career is set, when the student opens "Skill gaps", then the message "Choose a target career first" is shown. | FR-10.2, FR-10.3 | Low |
| US-10.3 Update Skill Level Story | As a student, I need to update my skill levels as I improve, so that my skill gaps stay accurate. | • Given a skill, when the student changes its level and saves, then the new level is shown.<br>• Given a gap skill reaches the required level, when the gaps are shown again, then it is no longer marked as a gap. | FR-10.4 | Low |

### FR-11 — Manage Projects and Portfolios
![FR-11 user stories](assets/c8-figures/FR-11-manage-projects-and-portfolios.png)


| Story | User story | Acceptance criteria | Covers | Priority |
|---|---|---|---|---|
| US-11.1 Add Project Story | As a student, I need to add a project with its title, description, technologies, status, links and documents, so that my work is recorded in one place. | • Given a title and description, when the student clicks "Add", then the project is saved with its technologies and status.<br>• Given a link, when the student adds it, then it is saved only if it is a valid URL.<br>• Given a document, when the student uploads it, then it is attached to the project. | FR-11.1, FR-11.4 | Low |
| US-11.2 Link Project to Skills Story | As a student, I need to link a project to the skills it demonstrates, so that employers see proof of my skills. | • Given a project and the student's skill list, when the student selects skills and saves, then the skills are linked to the project.<br>• Given a linked skill, when the skill is opened, then the projects that demonstrate it are listed. | FR-11.2 | Low |
| US-11.3 View Portfolio Story | As a senior student, I need a portfolio view of my completed projects, so that I can show my work to employers. | • Given completed projects, when the student opens "Portfolio", then only completed projects are shown with their links and skills.<br>• Given no completed projects, when the portfolio is opened, then the message "No completed projects yet" is shown. | FR-11.3 | Low |

### FR-12 — Manage Internship and Job Preparation
![FR-12 user stories](assets/c8-figures/FR-12-manage-internship-and-job-preparation.png)


| Story | User story | Acceptance criteria | Covers | Priority |
|---|---|---|---|---|
| US-12.1 Track Applications Story | As a senior student, I need to track my internship and job applications with company, position, status and deadline, so that I do not miss any application. | • Given a company, position and deadline, when the student clicks "Add", then the application is saved with status "Planned".<br>• Given an application, when the student changes its status (e.g. "Applied", "Interview"), then the new status is shown.<br>• Given an application deadline in the past, when the student adds it, then an error is shown. | FR-12.1 | Low |
| US-12.2 Interview Preparation Story | As a senior student, I need to track interview preparation tasks for each application, so that I am ready for every interview. | • Given an application, when the student adds a preparation task, then it is listed under that application.<br>• Given a preparation task, when the student marks it as done, then it is shown as completed. | FR-12.2 | Low |
| US-12.3 Track CV and Portfolio Updates Story | As a senior student, I need to track the CV and portfolio updates needed for each application, so that I send the right version to each employer. | • Given an application, when the student adds a CV update item, then it is listed under that application.<br>• Given an application, when the student adds a portfolio update item, then it is listed under that application.<br>• Given an update item, when it is marked as done, then it is shown as completed. | FR-12.3, FR-12.4 | Low |
| US-12.4 Record Outcome and Feedback Story | As a senior student, I need to record the outcome and feedback of each application, so that I can improve my next applications. | • Given an application, when the student sets the outcome (accepted / rejected / no response), then it is saved.<br>• Given an application, when the student writes feedback and saves, then the feedback is shown with the application. | FR-12.5, FR-12.6 | Low |

### FR-13 — Manage Habits
![FR-13 user stories](assets/c8-figures/FR-13-manage-habits.png)


| Story | User story | Acceptance criteria | Covers | Priority |
|---|---|---|---|---|
| US-13.1 Create Habit Story | As a first-year student, I need to create a habit with its name, frequency, goal and reminder, so that I build good study habits from the start. | • Given a name and frequency, when the student clicks "Create", then the habit is saved and shown on its scheduled days.<br>• Given the name or frequency is empty, when the student clicks "Create", then a required-field message is shown. | FR-13.1 | High |
| US-13.2 Complete Habit and Keep Streak Story | As a student, I need to mark a habit as done for a day and see my streak, so that I stay motivated to keep it. | • Given a habit scheduled today, when the student marks it as done, then today is recorded as completed.<br>• Given the habit was completed on consecutive scheduled days, when the streak is shown, then it equals the number of consecutive completions.<br>• Given a scheduled day is missed, when the streak is shown, then it restarts from 0. | FR-13.2, FR-13.3 | High |
| US-13.3 Link Habit to Goal Story | As a student, I need to link a habit to an academic goal, so that I see how my habits support my goals. | • Given a habit and an active goal, when the student links them, then the habit is shown inside the goal.<br>• Given the goal is archived, when the habit is shown, then the link is shown as inactive. | FR-13.4 | Medium |

### FR-14 — Analyze Study Workload
![FR-14 user stories](assets/c8-figures/FR-14-analyze-study-workload.png)


| Story | User story | Acceptance criteria | Covers | Priority |
|---|---|---|---|---|
| US-14.1 View Goal Workload Story | As a student with a heavy workload, I need to see the remaining workload, remaining study time and percentage completed of each goal, so that I know if my plan is realistic. | • Given an active goal with unfinished tasks, when the workload is shown, then the remaining workload equals the sum of the durations of its unfinished tasks.<br>• Given the student's availability up to the target date, when the workload is shown, then the remaining available study time is shown.<br>• Given completed and unfinished tasks, when the workload is shown, then percentage completed = completed duration ÷ total planned duration × 100. | FR-14.1, FR-14.2, FR-14.3, FR-14.4 | Medium |
| US-14.2 Overloaded Goal Story | As a student with a heavy workload, I need a goal to be marked as overloaded when its work does not fit my time, so that I know early that I cannot finish it as planned. | • Given remaining workload > remaining available study time, when the workload is calculated, then the goal is marked "Overloaded".<br>• Given remaining workload ≤ remaining available study time, when the workload is calculated, then the goal is not marked "Overloaded". | FR-14.5 | Medium |

### FR-15 — Predict and Warn Early
![FR-15 user stories](assets/c8-figures/FR-15-predict-and-warn-early.png)


| Story | User story | Acceptance criteria | Covers | Priority |
|---|---|---|---|---|
| US-15.1 Completion Likelihood Story | As a student, I need to see how likely I am to finish each goal on time, so that I can focus on the goals at risk. | • Given an active goal, when the prediction runs, then a likelihood (High / Medium / Low) is shown from its percentage completed and its remaining study time.<br>• Given a goal with no tasks, when the prediction runs, then the message "Not enough data" is shown. | FR-15.1 | Medium |
| US-15.2 Early Warning Story | As a student who is falling behind, I need an early warning before the target date with the tasks that cause the overload, so that I can act before I miss the deadline. | • Given a goal marked "Overloaded" before its target date, when the check runs, then an early warning is created for that goal.<br>• Given an early warning, when the student opens it, then the scheduled tasks that push the workload over the available time are listed.<br>• Given the goal is no longer overloaded, when the check runs, then the warning is closed. | FR-15.2, FR-15.3 | Medium |
| US-15.3 Recovery Actions Story | As a student who is falling behind, I need suggested recovery actions for a warned goal, so that I can still recover before the target date. | • Given an active early warning, when the student opens it, then at least one recovery action is suggested (e.g. extend the target date, add study time, remove a task).<br>• Given the student applies an action, when the workload is recalculated, then the new result is shown. | FR-15.4 | Medium |

### FR-16 — Coach Students with AI
![FR-16 user stories](assets/c8-figures/FR-16-coach-students-with-ai.png)


| Story | User story | Acceptance criteria | Covers | Priority |
|---|---|---|---|---|
| US-16.1 Personalized Study Advice Story | As a student, I need personalized advice based on my goals, tasks, habits, workload and upcoming deadlines, so that I know how to study better this week. | • Given the student's goals, tasks, habits and workload, when the student opens "Coach", then recommendations that mention their own goals and deadlines are shown.<br>• Given a deadline within 7 days, when advice is generated, then the advice includes that deadline.<br>• Given the AI service is unavailable, when the student opens "Coach", then an error message is shown. | FR-16.1, FR-16.2 | Low |
| US-16.2 Recovery Strategy Story | As a student who is falling behind, I need recovery strategies from the coach, so that I can get back on track. | • Given a goal marked "Behind schedule", when the student asks the coach for help, then recovery strategies for that goal are shown.<br>• Given no goal is behind, when the student asks, then the message "You are on track" is shown. | FR-16.3 | Low |
| US-16.3 Motivational Guidance Story | As a student who is falling behind, I need motivational guidance, so that I stay on track with my goals. | • Given the student has missed tasks this week, when the coach page is opened, then a motivational message is shown.<br>• Given the student completed all tasks this week, when the coach page is opened, then a positive progress message is shown. | FR-16.4 | Low |

### FR-17 — Display Student Dashboard
![FR-17 user stories](assets/c8-figures/FR-17-display-student-dashboard.png)


| Story | User story | Acceptance criteria | Covers | Priority |
|---|---|---|---|---|
| US-17.1 Today and Deadlines Story | As a student, I need to see today's tasks and upcoming deadlines on my dashboard, so that I know what to do first when I log in. | • Given tasks scheduled today, when the student opens the dashboard, then they are listed by time.<br>• Given deadlines in the next 7 days, when the dashboard is opened, then they are listed from nearest to farthest.<br>• Given nothing is scheduled today, when the dashboard is opened, then the message "Nothing scheduled today" is shown. | FR-17.1 | High |
| US-17.2 Goal Status Story | As a student, I need to see each goal's status and completion percentage on my dashboard, so that I can follow my progress at a glance. | • Given active goals, when the dashboard is opened, then each goal shows its status (On track / Behind / Completed).<br>• Given active goals, when the dashboard is opened, then each goal shows its completion percentage. | FR-17.2, FR-17.3 | High |
| US-17.3 Habit Streaks Story | As a student, I need to see my habit streaks and today's habit status on my dashboard, so that I keep my habits going. | • Given habits, when the dashboard is opened, then each habit shows its current streak.<br>• Given a habit scheduled today, when the dashboard is opened, then it shows whether it is done today. | FR-17.4 | High |
| US-17.4 Workload Summary Story | As a student with a heavy workload, I need a workload summary of each active goal on my dashboard, so that I notice overloaded goals quickly. | • Given active goals, when the dashboard is opened, then each goal shows its remaining workload and remaining study time.<br>• Given an overloaded goal, when the dashboard is opened, then it is highlighted as "Overloaded". | FR-17.5 | Medium |
| US-17.5 Career Progress Story | As a senior student, I need to see my career roadmap progress on my dashboard, so that I keep job preparation in view. | • Given a career roadmap, when the dashboard is opened, then its progress percentage is shown.<br>• Given no target career, when the dashboard is opened, then a "Set your target career" link is shown. | FR-17.6 | Low |

### FR-18 — Send Notifications
![FR-18 user stories](assets/c8-figures/FR-18-send-notifications.png)


| Story | User story | Acceptance criteria | Covers | Priority |
|---|---|---|---|---|
| US-18.1 Deadline and Milestone Reminders Story | As a student, I need reminders for upcoming assignments, exams and goal milestones, so that I never miss a deadline. | • Given an assignment or exam due in 24 hours, when the reminder time arrives, then the student receives a reminder.<br>• Given a goal milestone due soon, when the reminder time arrives, then the student receives a milestone reminder.<br>• Given the item is already completed, when the reminder time arrives, then no reminder is sent. | FR-18.1, FR-18.5 | High |
| US-18.2 Task and Habit Reminders Story | As a student, I need reminders for my scheduled study tasks and habits, so that I follow my plan every day. | • Given a study task starting in 15 minutes, when the time arrives, then the student receives a reminder.<br>• Given a habit with a reminder time, when the time arrives and the habit is not done, then the student receives a reminder. | FR-18.2, FR-18.3 | High |
| US-18.3 Early Warning Notification Story | As a student who is falling behind, I need to be notified of early warnings, so that I act in time. | • Given a new early warning, when it is created, then the student receives a notification with the goal name.<br>• Given the same warning is still open, when the next check runs, then no duplicate notification is sent on the same day. | FR-18.4 | Medium |
| US-18.4 Streak Milestone Notification Story | As a student, I need to be notified when my habit streak reaches a milestone, so that I feel motivated to continue. | • Given a habit streak reaches 7, 30 or 100 days, when it is updated, then the student receives a congratulation notification.<br>• Given the streak is not at a milestone, when it is updated, then no notification is sent. | FR-18.6 | Medium |

### FR-19 — Manage Study Groups and Accountability
![FR-19 user stories](assets/c8-figures/FR-19-manage-study-groups-and-accountability.png)


| Story | User story | Acceptance criteria | Covers | Priority |
|---|---|---|---|---|
| US-19.1 Create Study Group Story | As a student, I need to create a study group, so that I can study with friends. | • Given a group name, when the student clicks "Create", then the group is created with the student as its admin and an invite code.<br>• Given an empty group name, when the student clicks "Create", then a required-field message is shown. | FR-19.1 | Low |
| US-19.2 Join Study Group Story | As a student, I need to join a study group, so that I get support from other students. | • Given a valid invite code, when the student clicks "Join", then the student becomes a member of the group.<br>• Given an invalid invite code, when the student clicks "Join", then the error "Invalid code" is shown. | FR-19.2 | Low |
| US-19.3 Share and Track Group Progress Story | As a study group member, I need to share my goals and progress updates and see the group's progress, so that we keep each other accountable. | • Given a goal, when a member shares it with the group, then all members can see it.<br>• Given a member posts a progress update, when the group page is opened, then the update is shown to all members.<br>• Given shared goals, when the group page is opened, then the group's overall progress is shown. | FR-19.3, FR-19.4, FR-19.7 | Low |
| US-19.4 Accountability Partner Story | As a student who is falling behind, I need to find and connect with an accountability partner, so that someone helps me stay motivated. | • Given students with similar courses or goals, when the student searches for a partner, then matching students are listed.<br>• Given a partner request, when the other student accepts it, then both are connected as partners.<br>• Given the other student rejects the request, when it is answered, then no connection is created. | FR-19.5, FR-19.6 | Low |
| US-19.5 Support Messages Story | As a study group member, I need to send support messages to my group, so that we encourage each other. | • Given a group, when a member sends a message, then all members can see it.<br>• Given an empty message, when the member clicks "Send", then nothing is sent. | FR-19.8 | Low |

### FR-20 — Manage Administration
![FR-20 user stories](assets/c8-figures/FR-20-manage-administration.png)


| Story | User story | Acceptance criteria | Covers | Priority |
|---|---|---|---|---|
| US-20.1 View and Update Student Account Story | As an administrator, I need to view and update student account details, so that I can manage accounts without asking the developers. | • Given a student account, when the admin searches by name or email, then the account details are shown.<br>• Given an account, when the admin edits a field and saves, then the new value is stored. | FR-20.1, FR-20.3 | Medium |
| US-20.2 Create Student Account Story | As an administrator, I need to create a student account, so that new students can start using the platform. | • Given a name and university email, when the admin clicks "Create", then the account is created.<br>• Given an email that already exists, when the admin clicks "Create", then the error "Email already exists" is shown. | FR-20.2 | Medium |
| US-20.3 Suspend Student Account Story | As an administrator, I need to suspend a student account, so that misused accounts are stopped. | • Given an active account, when the admin clicks "Suspend" and confirms, then the account status becomes "Suspended".<br>• Given a suspended account, when the student tries to use the platform, then access is refused. | FR-20.4 | Medium |
| US-20.4 Usage Statistics Story | As an administrator, I need usage statistics for registered students, active students and feature usage, so that I know how the platform is used. | • Given the statistics page, when the admin opens it, then the number of registered students is shown.<br>• Given the statistics page, when the admin opens it, then the number of students active in the last 30 days is shown.<br>• Given the statistics page, when the admin opens it, then the usage count of each feature is shown. | FR-20.5, FR-20.6, FR-20.7 | Medium |
| US-20.5 Resolve Account Issues Story | As an administrator, I need to investigate and resolve student account issues, so that problems are fixed quickly. | • Given a reported account issue, when the admin opens it, then the account details and recent activity are shown.<br>• Given an investigated issue, when the admin marks it as resolved, then its status becomes "Resolved". | FR-20.8, FR-20.9 | Medium |
| US-20.6 Retention Metrics Story | As an administrator, I need retention metrics that show student activity over time, so that I know how many students keep using the platform. | • Given the retention page, when the admin opens it, then the percentage of active students is shown for each month.<br>• Given a month with no data, when the chart is shown, then the month shows 0. | FR-20.10 | Low |

## Checklist

- [x] Decompose each feature into user stories
- [x] Write acceptance criteria per story (Given / When / Then)
- [x] Link each story to its high-level FR (epic) and to the low-level FRs it covers
- [x] Assign stable story IDs (US-01.1, ...)
- [x] Every low-level FR in B4 (109, including FR-07.5) is covered by a story (FR-01.4 validation applies to both create and update, so it is covered by US-01.1 and US-01.2)
- [ ] Team review
