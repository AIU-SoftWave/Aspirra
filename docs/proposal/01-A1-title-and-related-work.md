# A1 — Title, description, problem & related work

> Issue: #1 · Part: A · Owner: TBD · Reviewer: TBD · Status: Draft

## Content

### Title & description

**Aspirra: Your AI Planner for Academic Success and Career Growth**

Every student has big goals, but turning them into daily action is the hard part. Aspirra is an AI-powered platform that makes it simple.

Share a goal like “reach a 3.5 GPA” or “land a software internship”, and Aspirra builds your roadmap of milestones, tasks, habits, and deadlines. You stay in control by editing the plan, and Aspirra fits it around your real schedule.

Life gets busy and plans slip. When yours does, Aspirra warns you early and recommends a way back on track, and nothing changes until you approve it. Along the way it tracks your skills, projects, and portfolio, so every study session moves you closer to your career.

Study, habits, and career growth, all in one place.

### The problem

Students don’t fail from a lack of ability. They fail because nothing connects their goals to their daily actions.

Deadlines sit in a calendar, tasks in a notes app, grades on the university portal, and career plans only in the student’s head. So goals stay too big to act on, schedules ignore real available time, and plans break when a single task is missed. Nobody warns the student early, and study stays disconnected from the skills and projects a career requires. The result is missed deadlines, lower grades, stress, and abandoned goals.

**What Aspirra solves**

Aspirra turns a goal into a realistic, editable plan, adapts it when the student falls behind, warns about risks early, and links daily study to career skills, all in one platform.
### Who suffers from it

University students are the people most affected, especially:

- **First-year students**, who are still learning to plan and are adjusting to the university workload.
- **Students with heavy workloads**, who juggle many courses, projects, and part-time work or activities.
- **Senior students**, who must balance coursework and graduation projects with portfolios and job applications.
- **Students who fall behind**, who have no easy way to recover once a plan breaks.

Instructors and academic advisors are affected indirectly, because they deal with late submissions and struggling students. They are not the main users of the system.

### Main features
|   # | Feature                        | Description                                                                                                      |
| --: | ------------------------------ | ---------------------------------------------------------------------------------------------------------------- |
|   1 | Student Profile                | Academic level, major, interests, skills, career interests, and availability.                                    |
|   2 | Academic Goal Management       | Semester goals, course goals, GPA targets, assignment goals, and exam goals.                                     |
|   3 | AI Goal Planner                | Convert a natural-language goal into milestones, tasks, habits, deadlines, and priorities.                       |
|   4 | AI Study Plan Generator        | Create personalized study plans and recommend adjustments based on progress and available time.                  |
|   5 | Course Management              | Manage courses, grades, assignments, projects, quizzes, exams, and course progress.                              |
|   6 | Assignment & Exam Planner      | Track deadlines, priorities, preparation schedules, and exam countdowns.                                         |
|   7 | Career Roadmap                 | Select a target career and track required skills, learning steps, and career progress.                           |
|   8 | Skill Tracking                 | Track technical and soft skills, skill levels, learning progress, and career skill requirements.                 |
|   9 | Project & Portfolio Management | Track projects, connect them to skills, and maintain a portfolio.                                                |
|  10 | Habit Management               | Track study, exercise, reading, coding, and other habits, including streaks and goal connections.                |
|  11 | Smart Scheduling               | Organize tasks using deadlines, priorities, workload, availability, and dependencies.                            |
|  12 | Adaptive Planning              | Detect when a student falls behind and recommend rescheduling or reprioritizing work.                            |
|  13 | AI Student Coach               | Provide personalized recommendations using the student's goals, tasks, habits, and progress.                     |
|  14 | Academic Performance Analytics | Show grade trends, course performance, study consistency, assignment completion, and GPA progress.               |
|  15 | Prediction & Early Warning     | Estimate goal completion and identify potential academic or deadline risks.                                      |
|  16 | Dashboard                      | Show today's tasks, upcoming deadlines, goal progress, habit streaks, academic performance, and career progress. |
|  17 | Notifications                  | Provide assignment, exam, study, missed-task, goal, and deadline reminders.                                      |
|  18 | Study Groups / Accountability  | Support shared goals, study groups, accountability partners, and shared progress.                                |
|  19 | Internship & Job Preparation   | Track applications, interview preparation, CV/portfolio progress, and required skills.                           |
|  20 | Admin Dashboard                | Support student management, system statistics, usage analytics, and activity monitoring.                         |
### Related work

| System      | Description                                                                                                                                                      | Limitations                                                                                                                                                                                                                                 |
| ----------- | ---------------------------------------------------------------------------------------------------------------------------------------------------------------- | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| Shovel      | A student planner where AI turns natural language into tasks, imports timetables, syncs calendars, checks if the schedule is realistic, and tracks study streaks | Plans only short-term study tasks. No long-term goal roadmap, no career or skill tracking, no study groups, no early-warning prediction. Paid subscription                                                                                  |
| MyStudyLife | A free planner for classes, assignments, and exams, with deadline reminders and a Pomodoro timer.                                                                | Mostly manual entry. No AI planning, no goal breakdown, no adaptive rescheduling, no career or skill features.                                                                                                                              |
| StudntX     | An AI assistant that builds study plans around the student’s schedule and tracks habits and streaks.                                                             | Focuses only on study plans and habits. No career roadmap, no skill or portfolio tracking, no analytics or early warning, no study groups.                                                                                                  |
| Reclaim.ai  | A web-based smart calendar that automatically blocks time for tasks and habits, and suggests a time-blocked day that the user approves or adjusts.               | Built as a general calendar, not for students. No course, grade, or exam tracking, no goal-to-plan breakdown, no career or skill features, no study groups. Its scheduling follows set rules rather than understanding the student’s goals. |

### Comparison table

| Feature                                                  | Shovel | MyStudyLife | StudntX | Reclaim.ai | Aspirra |
| -------------------------------------------------------- | ------ | ----------- | ------- | ---------- | ------- |
| Natural-language input to tasks                          | Yes    | No          | Yes     | No         | Yes     |
| Full goal roadmap (milestones, tasks, habits, deadlines) | No     | No          | Partial | No         | Yes     |
| Course, assignment, and exam tracking                    | Yes    | Yes         | Yes     | No         | Yes     |
| AI study plan around availability                        | Yes    | No          | Yes     | Partial    | Yes     |
| Schedule feasibility check                               | Yes    | No          | No      | No         | Yes     |
| Adaptive replanning when behind (student approves)       | No     | No          | No      | Partial    | Yes     |
| Habit tracking with streaks                              | Yes    | No          | Yes     | Partial    | Yes     |
| Deadline reminders                                       | Yes    | Yes         | Yes     | Yes        | Yes     |
| Career roadmap and skill tracking                        | No     | No          | No      | No         | Yes     |
| Project and portfolio management                         | No     | No          | No      | No         | Yes     |
| Prediction and early warning                             | No     | No          | No      | No         | Yes     |
| Study groups and accountability                          | No     | No          | No      | No         | Yes     |
| Admin dashboard                                          | No     | No          | No      | No         | Yes     |

## Checklist

- [x] Write title, description, problem statement and affected users
- [x] List the main features of the system
- [x] Research at least 3 related systems/apps
- [x] Summarize each system's limitations
- [x] Build the features x systems comparison table (Aspirra last)
