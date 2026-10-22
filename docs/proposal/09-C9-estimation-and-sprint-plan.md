# C9 — Estimation & sprint plan

> Issue: #9 · Part: C · Owner: Arwa Mohey Hamdy · Reviewer: TBD · Status: Draft

## Content

**Given (team decisions):** 1 story point = 1 working day. 1 week = 5 working days. Sprint length = 2 weeks (10 working days). Deadline = 12 weeks. Team = 5 developers.

### Planning Poker results

| Feature | User story | Points | Why this size? |
|---|---|---|---|
| FR-01 Manage Student Profile | US-01.1 Create Profile | 3 | Profile form with availability slots + required-field and time-slot validation |
| FR-01 Manage Student Profile | US-01.2 Update Profile | 2 | Re-uses the create form; update + validation |
| FR-01 Manage Student Profile | US-01.3 View Profile | 1 | Read-only profile page |
| FR-02 Manage Academic Goals | US-02.1 Create Goal | 3 | Goal form + validation (required fields, future date) |
| FR-02 Manage Academic Goals | US-02.2 Edit Goal | 2 | Edit existing goal + date validation |
| FR-02 Manage Academic Goals | US-02.3 Complete Goal | 1 | Status change to Completed |
| FR-02 Manage Academic Goals | US-02.4 Archive Goal | 2 | Archive + restore (two status changes and a filtered list) |
| FR-02 Manage Academic Goals | US-02.5 Delete Goal | 1 | Delete with confirmation |
| FR-03 Manage Courses | US-03.1 Add Course | 3 | Course form + credits/workload + duplicate-code check |
| FR-03 Manage Courses | US-03.2 View Course Progress | 3 | Joins course with its assignments, completed vs outstanding |
| FR-03 Manage Courses | US-03.3 Update Course | 2 | Edit course + duplicate-code check |
| FR-03 Manage Courses | US-03.4 Remove Course | 2 | Remove course + cascade warning for its assignments |
| FR-04 Manage Assignments and Exams | US-04.1 Add Assignment | 3 | Assignment form + validation + default status |
| FR-04 Manage Assignments and Exams | US-04.2 Add Exam | 2 | Exam form re-uses the assignment form pattern |
| FR-04 Manage Assignments and Exams | US-04.3 Track Assignment Status | 2 | Status change that also updates course progress |
| FR-04 Manage Assignments and Exams | US-04.4 Exam Preparation Reminder | 2 | Reminder setting saved with the exam + date check |
| FR-05 Plan Goals with AI | US-05.1 Generate Goal Plan | 8 | First AI integration: prompt, call, parse milestones/tasks, AI-unavailable handling |
| FR-05 Plan Goals with AI | US-05.2 Review and Edit AI Plan | 5 | Editable review screen for both milestones and tasks, nothing saved before approval |
| FR-05 Plan Goals with AI | US-05.3 Approve AI Plan | 2 | Approve saves the reviewed plan; discard drops it |
| FR-06 Generate Study Plans | US-06.1 Generate Study Plan | 8 | AI plan within availability + priority placement + schema validation + keep previous plan (re-uses AI client from US-05.1) |
| FR-06 Generate Study Plans | US-06.2 Regenerate Study Plan | 3 | Re-runs generation with new data; keeps old plan on failure |
| FR-07 Enable Smart Scheduling | US-07.1 Auto-Schedule Tasks | 8 | Scheduling algorithm: deadlines, priorities, availability and task dependencies |
| FR-07 Enable Smart Scheduling | US-07.2 Resolve Scheduling Conflicts | 5 | Overlap detection + finding free alternative slots |
| FR-07 Enable Smart Scheduling | US-07.3 Adjust Schedule Manually | 3 | Move a task to a new slot + conflict warning |
| FR-07 Enable Smart Scheduling | US-07.4 Mark Task Done | 2 | Status change on a scheduled task that updates progress and remaining workload |
| FR-08 Enable Adaptive Planning | US-08.1 Detect Falling Behind | 3 | Rule: past, unfinished tasks → Behind schedule |
| FR-08 Enable Adaptive Planning | US-08.2 Get Re-planning Recommendations | 5 | Builds reschedule/reprioritize suggestions from the schedule |
| FR-08 Enable Adaptive Planning | US-08.3 Respond to Recommendations | 3 | Accept / reject / modify flow with approval before applying |
| FR-09 Manage Career Roadmaps | US-09.1 Choose Target Career | 2 | Select from a list or enter a custom career + validation |
| FR-09 Manage Career Roadmaps | US-09.2 Generate Career Roadmap | 5 | AI roadmap generation (re-uses the AI client) + error handling |
| FR-09 Manage Career Roadmaps | US-09.3 Track Roadmap Progress | 2 | Mark milestones done + progress percentage |
| FR-09 Manage Career Roadmaps | US-09.4 Change Target Career | 3 | Change career + trigger roadmap regeneration |
| FR-10 Track Skills | US-10.1 Add Skill | 2 | Skill form + duplicate check |
| FR-10 Track Skills | US-10.2 View Skill Gaps | 5 | Compare skills and levels with the roadmap's required skills |
| FR-10 Track Skills | US-10.3 Update Skill Level | 1 | Update one field; gaps re-use US-10.2 |
| FR-11 Manage Projects and Portfolios | US-11.1 Add Project | 5 | Project form + URL validation + document upload |
| FR-11 Manage Projects and Portfolios | US-11.2 Link Project to Skills | 3 | Many-to-many link between projects and skills, shown both ways |
| FR-11 Manage Projects and Portfolios | US-11.3 View Portfolio | 3 | Portfolio page: completed projects with links and skills |
| FR-12 Manage Internship and Job Preparation | US-12.1 Track Applications | 3 | Application form + status changes + deadline check |
| FR-12 Manage Internship and Job Preparation | US-12.2 Interview Preparation | 2 | Checklist of preparation tasks per application |
| FR-12 Manage Internship and Job Preparation | US-12.3 Track CV and Portfolio Updates | 3 | Two item lists (CV and portfolio updates) per application |
| FR-12 Manage Internship and Job Preparation | US-12.4 Record Outcome and Feedback | 2 | Outcome field + feedback text |
| FR-13 Manage Habits | US-13.1 Create Habit | 3 | Habit form with frequency and reminder + validation |
| FR-13 Manage Habits | US-13.2 Complete Habit and Keep Streak | 3 | Daily completion + streak calculation and reset |
| FR-13 Manage Habits | US-13.3 Link Habit to Goal | 2 | Link habit to goal + inactive state when the goal is archived |
| FR-14 Analyze Study Workload | US-14.1 View Goal Workload | 5 | Three calculations (remaining work, available time, % completed) + display |
| FR-14 Analyze Study Workload | US-14.2 Overloaded Goal | 2 | One comparison rule on top of US-14.1 |
| FR-15 Predict and Warn Early | US-15.1 Completion Likelihood | 5 | Likelihood rule from % completed and remaining time + no-data case |
| FR-15 Predict and Warn Early | US-15.2 Early Warning | 5 | Creates/closes warnings and finds the tasks that cause the overload |
| FR-15 Predict and Warn Early | US-15.3 Recovery Actions | 3 | Suggests actions and recalculates after one is applied |
| FR-16 Coach Students with AI | US-16.1 Personalized Study Advice | 5 | AI advice from goals, tasks, habits and workload (re-uses AI client) |
| FR-16 Coach Students with AI | US-16.2 Recovery Strategy | 3 | Coach response for goals that are behind |
| FR-16 Coach Students with AI | US-16.3 Motivational Guidance | 2 | Message chosen from the week's completion |
| FR-17 Display Student Dashboard | US-17.1 Today and Deadlines | 3 | Dashboard layout + today's tasks + 7-day deadlines |
| FR-17 Display Student Dashboard | US-17.2 Goal Status | 2 | Goal status and % widget (data from goals) |
| FR-17 Display Student Dashboard | US-17.3 Habit Streaks | 2 | Habit streak widget (data from habits) |
| FR-17 Display Student Dashboard | US-17.4 Workload Summary | 2 | Workload widget (data from US-14.1) + overload highlight |
| FR-17 Display Student Dashboard | US-17.5 Career Progress | 1 | One progress value from the career roadmap |
| FR-18 Send Notifications | US-18.1 Deadline and Milestone Reminders | 5 | Reminder scheduler + email gateway + skip completed items |
| FR-18 Send Notifications | US-18.2 Task and Habit Reminders | 3 | Two more reminder types on the scheduler from US-18.1 |
| FR-18 Send Notifications | US-18.3 Early Warning Notification | 2 | Notification on new warning + no duplicates per day |
| FR-18 Send Notifications | US-18.4 Streak Milestone Notification | 2 | Notification on streak milestones (7/30/100) |
| FR-19 Manage Study Groups and Accountability | US-19.1 Create Study Group | 2 | Create group + invite code |
| FR-19 Manage Study Groups and Accountability | US-19.2 Join Study Group | 2 | Join by invite code + invalid-code error |
| FR-19 Manage Study Groups and Accountability | US-19.3 Share and Track Group Progress | 5 | Share goals, post updates and group progress for all members |
| FR-19 Manage Study Groups and Accountability | US-19.4 Accountability Partner | 5 | Partner matching + request / accept / reject flow |
| FR-19 Manage Study Groups and Accountability | US-19.5 Support Messages | 3 | Group message list + send |
| FR-20 Manage Administration | US-20.1 View and Update Student Account | 3 | Search accounts + view and edit details |
| FR-20 Manage Administration | US-20.2 Create Student Account | 2 | Create account + duplicate-email check |
| FR-20 Manage Administration | US-20.3 Suspend Student Account | 2 | Suspend + refuse access to suspended accounts |
| FR-20 Manage Administration | US-20.4 Usage Statistics | 5 | Three statistics queries + statistics page |
| FR-20 Manage Administration | US-20.5 Resolve Account Issues | 3 | Issue view with account activity + resolve status |
| FR-20 Manage Administration | US-20.6 Retention Metrics | 3 | Monthly retention calculation + chart |
| **Total** | **73 stories** | **225** | |

### Computed values

**1. Total points**

```
Total points = Σ story points
             = 6 + 9 + 10 + 9 + 15 + 11 + 18 + 11 + 12 + 8 + 11 + 10 + 8 + 7 + 13 + 10 + 10 + 12 + 17 + 18   (FR-01 … FR-20)
             = 225 points
```

**2. Effort (person-days)**

```
Effort = total points × days per point
       = 225 × 1
       = 225 person-days
```

**3. Effort (person-weeks)**

```
Effort = person-days ÷ 5
       = 225 ÷ 5
       = 45 person-weeks
```

**4. Team size**

```
Team size = effort (person-weeks) ÷ deadline (weeks) → round up
          = 45 ÷ 12
          = 3.75 → 4 developers
```

**5. Capacity check**

```
Capacity = team size × weeks ≥ required effort
         = 4 × 12 = 48 person-weeks ≥ 45 ✔
Why not 3? 3 × 12 = 36 < 45 ✘ — the deadline would be missed.
Our team (5): 5 × 12 = 60 person-weeks ≥ 45 ✔
```

**6. Sprint capacity**

```
Sprint capacity = developers × working days per sprint ÷ days per point
                = 5 × 10 ÷ 1
                = 50 points per sprint
```

**7. Actual velocity**

```
Actual velocity = completed story points per sprint
No sprint is finished yet → planned velocity = sprint capacity
                = 50 points per sprint (actual velocity reported after every sprint, from PM1)
```

**8. Schedule (weeks)**

```
Schedule = effort (person-weeks) ÷ team size → round up
         = 45 ÷ 5
         = 9 → 9 weeks
```

**9. Number of sprints**

```
Number of sprints = total points ÷ sprint capacity → round up
                  = 225 ÷ 50
                  = 4.5 → 5 sprints = 10 weeks ≤ 12 weeks ✔
```

**10. Max scope (points)**

```
Max scope = team size × weeks × 5 ÷ days per point
          = 5 × 12 × 5 ÷ 1
          = 300 points ≥ 225 ✔ (no story has to be removed)
```

### Sprint plan (by priority)

| Sprint | Weeks | User stories | Points | Fits? (≤ 50) |
|---|---|---|---|---|
| Sprint 1 | 1–2 | US-01.1 Create Profile (3), US-01.3 View Profile (1), US-01.2 Update Profile (2), US-02.1 Create Goal (3), US-02.2 Edit Goal (2), US-02.3 Complete Goal (1), US-03.1 Add Course (3), US-04.1 Add Assignment (3), US-04.2 Add Exam (2), US-04.3 Track Assignment Status (2), US-03.2 View Course Progress (3), US-13.1 Create Habit (3), US-13.2 Complete Habit and Keep Streak (3), US-05.1 Generate Goal Plan (8), US-05.2 Review and Edit AI Plan (5), US-05.3 Approve AI Plan (2) | 46 | ✔ |
| Sprint 2 | 3–4 | US-06.1 Generate Study Plan (8), US-07.1 Auto-Schedule Tasks (8), US-07.4 Mark Task Done (2), US-07.2 Resolve Scheduling Conflicts (5), US-08.1 Detect Falling Behind (3), US-17.1 Today and Deadlines (3), US-17.2 Goal Status (2), US-17.3 Habit Streaks (2), US-18.1 Deadline and Milestone Reminders (5), US-18.2 Task and Habit Reminders (3), US-02.4 Archive Goal (2), US-02.5 Delete Goal (1), US-03.3 Update Course (2), US-03.4 Remove Course (2), US-04.4 Exam Preparation Reminder (2) | 50 | ✔ |
| Sprint 3 | 5–6 | US-06.2 Regenerate Study Plan (3), US-07.3 Adjust Schedule Manually (3), US-13.3 Link Habit to Goal (2), US-14.1 View Goal Workload (5), US-14.2 Overloaded Goal (2), US-08.2 Get Re-planning Recommendations (5), US-08.3 Respond to Recommendations (3), US-15.1 Completion Likelihood (5), US-15.2 Early Warning (5), US-15.3 Recovery Actions (3), US-17.4 Workload Summary (2), US-18.3 Early Warning Notification (2), US-18.4 Streak Milestone Notification (2), US-20.2 Create Student Account (2), US-20.1 View and Update Student Account (3), US-20.3 Suspend Student Account (2) | 49 | ✔ |
| Sprint 4 | 7–8 | US-20.4 Usage Statistics (5), US-20.5 Resolve Account Issues (3), US-09.1 Choose Target Career (2), US-09.2 Generate Career Roadmap (5), US-09.3 Track Roadmap Progress (2), US-09.4 Change Target Career (3), US-10.1 Add Skill (2), US-10.2 View Skill Gaps (5), US-10.3 Update Skill Level (1), US-11.1 Add Project (5), US-11.2 Link Project to Skills (3), US-11.3 View Portfolio (3), US-12.1 Track Applications (3), US-12.2 Interview Preparation (2), US-12.3 Track CV and Portfolio Updates (3), US-12.4 Record Outcome and Feedback (2) | 49 | ✔ |
| Sprint 5 | 9–10 | US-16.1 Personalized Study Advice (5), US-16.2 Recovery Strategy (3), US-16.3 Motivational Guidance (2), US-17.5 Career Progress (1), US-19.1 Create Study Group (2), US-19.2 Join Study Group (2), US-19.3 Share and Track Group Progress (5), US-19.4 Accountability Partner (5), US-19.5 Support Messages (3), US-20.6 Retention Metrics (3) | 31 | ✔ |
| Sprint 6 | 11–12 | Buffer for unfinished stories | 0 | ✔ |

- US-06.1 is next after Sprint 1, but 46 + 8 = 54 > 50 ✘, so it moves to the top of Sprint 2.
- US-06.2 is next after Sprint 2, but 50 + 3 = 53 > 50 ✘, so it moves to the top of Sprint 3.
- US-20.4 is next after Sprint 3, but 49 + 5 = 54 > 50 ✘, so it moves to the top of Sprint 4.
- US-16.1 is next after Sprint 4, but 49 + 5 = 54 > 50 ✘, so it moves to the top of Sprint 5.

### Burndown

| Point in time | Remaining (ideal) | Remaining (planned) | Remaining (actual) |
|---|---|---|---|
| Start | 225 | 225 | 225 |
| End of Sprint 1 | 180 | 179 | — |
| End of Sprint 2 | 135 | 129 | — |
| End of Sprint 3 | 90 | 80 | — |
| End of Sprint 4 | 45 | 31 | — |
| End of Sprint 5 | 0 | 0 | — |

The ideal line drops by the same amount every sprint (225 ÷ 5 = 45); the planned line follows the sprint plan. The actual column is filled in after each sprint.

## Checklist

- [x] Planning poker: story points per story
- [x] Compute the 10 values with full working
- [x] Build the sprint plan table by priority
