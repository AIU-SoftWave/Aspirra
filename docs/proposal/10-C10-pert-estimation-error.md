# C10 — PERT estimation error

> Issue: #10 · Part: C · Owner: Arwa Mohey Hamdy · Reviewer: TBD · Status: Draft

## Content

For every story the team gave three estimates in days: **O** (optimistic), **M** (most likely = points × 1 day, from C9) and **P** (pessimistic). O and P depend on how uncertain the story is: simple screens and forms have P close to M, while stories that use the AI provider, the email service or the scheduling algorithm have a much larger P. P is always farther from M than O, because things go wrong more often than they go better.

```
E  = (O + 4M + P) ÷ 6
SD = (P − O) ÷ 6
```

| User story | O (days) | M (days) | P (days) | E = (O+4M+P)/6 | SD = (P−O)/6 |
|---|---|---|---|---|---|
| US-01.1 Create Profile | 2 | 3 | 5 | 19 ÷ 6 = 3.17 | 3 ÷ 6 = 0.50 |
| US-01.2 Update Profile | 1.5 | 2 | 3 | 12.5 ÷ 6 = 2.08 | 1.5 ÷ 6 = 0.25 |
| US-01.3 View Profile | 0.5 | 1 | 2 | 6.5 ÷ 6 = 1.08 | 1.5 ÷ 6 = 0.25 |
| US-02.1 Create Goal | 2 | 3 | 5 | 19 ÷ 6 = 3.17 | 3 ÷ 6 = 0.50 |
| US-02.2 Edit Goal | 1.5 | 2 | 3 | 12.5 ÷ 6 = 2.08 | 1.5 ÷ 6 = 0.25 |
| US-02.3 Complete Goal | 0.5 | 1 | 2 | 6.5 ÷ 6 = 1.08 | 1.5 ÷ 6 = 0.25 |
| US-02.4 Archive Goal | 1.5 | 2 | 3 | 12.5 ÷ 6 = 2.08 | 1.5 ÷ 6 = 0.25 |
| US-02.5 Delete Goal | 0.5 | 1 | 2 | 6.5 ÷ 6 = 1.08 | 1.5 ÷ 6 = 0.25 |
| US-03.1 Add Course | 2 | 3 | 5 | 19 ÷ 6 = 3.17 | 3 ÷ 6 = 0.50 |
| US-03.2 View Course Progress | 2 | 3 | 6 | 20 ÷ 6 = 3.33 | 4 ÷ 6 = 0.67 |
| US-03.3 Update Course | 1.5 | 2 | 3 | 12.5 ÷ 6 = 2.08 | 1.5 ÷ 6 = 0.25 |
| US-03.4 Remove Course | 1.5 | 2 | 3 | 12.5 ÷ 6 = 2.08 | 1.5 ÷ 6 = 0.25 |
| US-04.1 Add Assignment | 2 | 3 | 5 | 19 ÷ 6 = 3.17 | 3 ÷ 6 = 0.50 |
| US-04.2 Add Exam | 1.5 | 2 | 3 | 12.5 ÷ 6 = 2.08 | 1.5 ÷ 6 = 0.25 |
| US-04.3 Track Assignment Status | 1.5 | 2 | 3 | 12.5 ÷ 6 = 2.08 | 1.5 ÷ 6 = 0.25 |
| US-04.4 Exam Preparation Reminder | 1.5 | 2 | 3 | 12.5 ÷ 6 = 2.08 | 1.5 ÷ 6 = 0.25 |
| US-05.1 Generate Goal Plan | 6 | 8 | 18 | 56 ÷ 6 = 9.33 | 12 ÷ 6 = 2.00 |
| US-05.2 Review and Edit AI Plan | 4 | 5 | 10 | 34 ÷ 6 = 5.67 | 6 ÷ 6 = 1.00 |
| US-05.3 Approve AI Plan | 1.5 | 2 | 3 | 12.5 ÷ 6 = 2.08 | 1.5 ÷ 6 = 0.25 |
| US-06.1 Generate Study Plan | 6 | 8 | 18 | 56 ÷ 6 = 9.33 | 12 ÷ 6 = 2.00 |
| US-06.2 Regenerate Study Plan | 2 | 3 | 8 | 22 ÷ 6 = 3.67 | 6 ÷ 6 = 1.00 |
| US-07.1 Auto-Schedule Tasks | 6 | 8 | 18 | 56 ÷ 6 = 9.33 | 12 ÷ 6 = 2.00 |
| US-07.2 Resolve Scheduling Conflicts | 4 | 5 | 10 | 34 ÷ 6 = 5.67 | 6 ÷ 6 = 1.00 |
| US-07.3 Adjust Schedule Manually | 2 | 3 | 6 | 20 ÷ 6 = 3.33 | 4 ÷ 6 = 0.67 |
| US-07.4 Mark Task Done | 1.5 | 2 | 3 | 12.5 ÷ 6 = 2.08 | 1.5 ÷ 6 = 0.25 |
| US-08.1 Detect Falling Behind | 2 | 3 | 6 | 20 ÷ 6 = 3.33 | 4 ÷ 6 = 0.67 |
| US-08.2 Get Re-planning Recommendations | 4 | 5 | 10 | 34 ÷ 6 = 5.67 | 6 ÷ 6 = 1.00 |
| US-08.3 Respond to Recommendations | 2 | 3 | 6 | 20 ÷ 6 = 3.33 | 4 ÷ 6 = 0.67 |
| US-09.1 Choose Target Career | 1.5 | 2 | 3 | 12.5 ÷ 6 = 2.08 | 1.5 ÷ 6 = 0.25 |
| US-09.2 Generate Career Roadmap | 4 | 5 | 12 | 36 ÷ 6 = 6.00 | 8 ÷ 6 = 1.33 |
| US-09.3 Track Roadmap Progress | 1.5 | 2 | 3 | 12.5 ÷ 6 = 2.08 | 1.5 ÷ 6 = 0.25 |
| US-09.4 Change Target Career | 2 | 3 | 8 | 22 ÷ 6 = 3.67 | 6 ÷ 6 = 1.00 |
| US-10.1 Add Skill | 1.5 | 2 | 3 | 12.5 ÷ 6 = 2.08 | 1.5 ÷ 6 = 0.25 |
| US-10.2 View Skill Gaps | 4 | 5 | 10 | 34 ÷ 6 = 5.67 | 6 ÷ 6 = 1.00 |
| US-10.3 Update Skill Level | 0.5 | 1 | 2 | 6.5 ÷ 6 = 1.08 | 1.5 ÷ 6 = 0.25 |
| US-11.1 Add Project | 4 | 5 | 10 | 34 ÷ 6 = 5.67 | 6 ÷ 6 = 1.00 |
| US-11.2 Link Project to Skills | 2 | 3 | 6 | 20 ÷ 6 = 3.33 | 4 ÷ 6 = 0.67 |
| US-11.3 View Portfolio | 2 | 3 | 5 | 19 ÷ 6 = 3.17 | 3 ÷ 6 = 0.50 |
| US-12.1 Track Applications | 2 | 3 | 5 | 19 ÷ 6 = 3.17 | 3 ÷ 6 = 0.50 |
| US-12.2 Interview Preparation | 1.5 | 2 | 3 | 12.5 ÷ 6 = 2.08 | 1.5 ÷ 6 = 0.25 |
| US-12.3 Track CV and Portfolio Updates | 2 | 3 | 5 | 19 ÷ 6 = 3.17 | 3 ÷ 6 = 0.50 |
| US-12.4 Record Outcome and Feedback | 1.5 | 2 | 3 | 12.5 ÷ 6 = 2.08 | 1.5 ÷ 6 = 0.25 |
| US-13.1 Create Habit | 2 | 3 | 5 | 19 ÷ 6 = 3.17 | 3 ÷ 6 = 0.50 |
| US-13.2 Complete Habit and Keep Streak | 2 | 3 | 6 | 20 ÷ 6 = 3.33 | 4 ÷ 6 = 0.67 |
| US-13.3 Link Habit to Goal | 1.5 | 2 | 3 | 12.5 ÷ 6 = 2.08 | 1.5 ÷ 6 = 0.25 |
| US-14.1 View Goal Workload | 4 | 5 | 10 | 34 ÷ 6 = 5.67 | 6 ÷ 6 = 1.00 |
| US-14.2 Overloaded Goal | 1.5 | 2 | 3 | 12.5 ÷ 6 = 2.08 | 1.5 ÷ 6 = 0.25 |
| US-15.1 Completion Likelihood | 4 | 5 | 10 | 34 ÷ 6 = 5.67 | 6 ÷ 6 = 1.00 |
| US-15.2 Early Warning | 4 | 5 | 10 | 34 ÷ 6 = 5.67 | 6 ÷ 6 = 1.00 |
| US-15.3 Recovery Actions | 2 | 3 | 6 | 20 ÷ 6 = 3.33 | 4 ÷ 6 = 0.67 |
| US-16.1 Personalized Study Advice | 4 | 5 | 12 | 36 ÷ 6 = 6.00 | 8 ÷ 6 = 1.33 |
| US-16.2 Recovery Strategy | 2 | 3 | 8 | 22 ÷ 6 = 3.67 | 6 ÷ 6 = 1.00 |
| US-16.3 Motivational Guidance | 1.5 | 2 | 3 | 12.5 ÷ 6 = 2.08 | 1.5 ÷ 6 = 0.25 |
| US-17.1 Today and Deadlines | 2 | 3 | 5 | 19 ÷ 6 = 3.17 | 3 ÷ 6 = 0.50 |
| US-17.2 Goal Status | 1.5 | 2 | 3 | 12.5 ÷ 6 = 2.08 | 1.5 ÷ 6 = 0.25 |
| US-17.3 Habit Streaks | 1.5 | 2 | 3 | 12.5 ÷ 6 = 2.08 | 1.5 ÷ 6 = 0.25 |
| US-17.4 Workload Summary | 1.5 | 2 | 3 | 12.5 ÷ 6 = 2.08 | 1.5 ÷ 6 = 0.25 |
| US-17.5 Career Progress | 0.5 | 1 | 2 | 6.5 ÷ 6 = 1.08 | 1.5 ÷ 6 = 0.25 |
| US-18.1 Deadline and Milestone Reminders | 4 | 5 | 12 | 36 ÷ 6 = 6.00 | 8 ÷ 6 = 1.33 |
| US-18.2 Task and Habit Reminders | 2 | 3 | 6 | 20 ÷ 6 = 3.33 | 4 ÷ 6 = 0.67 |
| US-18.3 Early Warning Notification | 1.5 | 2 | 3 | 12.5 ÷ 6 = 2.08 | 1.5 ÷ 6 = 0.25 |
| US-18.4 Streak Milestone Notification | 1.5 | 2 | 3 | 12.5 ÷ 6 = 2.08 | 1.5 ÷ 6 = 0.25 |
| US-19.1 Create Study Group | 1.5 | 2 | 3 | 12.5 ÷ 6 = 2.08 | 1.5 ÷ 6 = 0.25 |
| US-19.2 Join Study Group | 1.5 | 2 | 3 | 12.5 ÷ 6 = 2.08 | 1.5 ÷ 6 = 0.25 |
| US-19.3 Share and Track Group Progress | 4 | 5 | 10 | 34 ÷ 6 = 5.67 | 6 ÷ 6 = 1.00 |
| US-19.4 Accountability Partner | 4 | 5 | 10 | 34 ÷ 6 = 5.67 | 6 ÷ 6 = 1.00 |
| US-19.5 Support Messages | 2 | 3 | 5 | 19 ÷ 6 = 3.17 | 3 ÷ 6 = 0.50 |
| US-20.1 View and Update Student Account | 2 | 3 | 5 | 19 ÷ 6 = 3.17 | 3 ÷ 6 = 0.50 |
| US-20.2 Create Student Account | 1.5 | 2 | 3 | 12.5 ÷ 6 = 2.08 | 1.5 ÷ 6 = 0.25 |
| US-20.3 Suspend Student Account | 1.5 | 2 | 3 | 12.5 ÷ 6 = 2.08 | 1.5 ÷ 6 = 0.25 |
| US-20.4 Usage Statistics | 4 | 5 | 10 | 34 ÷ 6 = 5.67 | 6 ÷ 6 = 1.00 |
| US-20.5 Resolve Account Issues | 2 | 3 | 5 | 19 ÷ 6 = 3.17 | 3 ÷ 6 = 0.50 |
| US-20.6 Retention Metrics | 2 | 3 | 6 | 20 ÷ 6 = 3.33 | 4 ÷ 6 = 0.67 |
| **Total** | | **225** | | **249.00** | |

### Step 1 — Estimation error of one story

```
US-05.1 Generate Goal Plan: E = 9.33 days, SD = 2.00
Range (68%) = 9.33 ± 2.00 → from about 7.33 to 11.33 days
```

US-05.1 Generate Goal Plan, US-06.1 Generate Study Plan, US-07.1 Auto-Schedule Tasks have the largest SD (2.00) → they are the riskiest stories; the team should watch them closely.

### Step 2 — Total expected effort

```
E(total) = Σ (O + 4M + P) ÷ 6 = 1494 ÷ 6 = 249.00 person-days
Single-point estimate (ΣM) = 225 person-days
Difference = 249.00 − 225 = 24.00 days
Percentage = 24.00 ÷ 225 × 100 = 10.7%
```

The single-point estimate was **24.00 days (10.7%) optimistic**: it planned for less work than the team will most probably need.

The total is computed from unrounded per-story values. The per-story rows are rounded to two decimals, so summing the displayed rows gives 248.94 rather than 249.00.

### Step 3 — Is the team still enough?

```
Effort (person-weeks) = 249.00 ÷ 5 = 49.80 person-weeks
Team size = 49.80 ÷ 12 = 4.15 → round up → 5 developers
Capacity = 5 × 12 = 60 person-weeks ≥ 49.80 ✔
Number of sprints = 249.00 ÷ 50 = 4.98 → 5 sprints ≤ 6 ✔
```

**Yes, the team is still enough.** Our 5 developers can still finish before the deadline, but with a smaller safety margin (60 vs. 49.80 instead of 60 vs. 45), and the team needed is now exactly our 5 developers. Sprint 6 is kept free as a buffer for overruns.

## Checklist

- [x] Fill O, M, P per story
- [x] Compute E per story
- [x] Compute E(total) and compare with sum(M)
- [x] Re-check capacity with E(total)
