# Interfaces Document (Level 0)

This document lists all provided and required interfaces in Aspirra, with their high-level and low-level FR mappings as requested by the TA.

## Provided Interfaces (exposed by service containers)

### IStudentProfile (Student Service - FR-01 | Owner: Ahmed Mohamed Ahmed)
- **High level:** - Manage student profile - Manage availability
- **Low level:** Manage student profile: - Create profile (FR-01.1) - Update profile (FR-01.2) - View profile (FR-01.3) | Manage availability: - Get availability (FR-01.x)

### IGoal (Academic Service - FR-02 | Owner: Ahmed Mohamed Ahmed)
- **High level:** - Manage academic goals
- **Low level:** Manage academic goals: - Create goal (FR-02.1) - Edit goal (FR-02.2, FR-02.3) - Complete goal (FR-02.4) - Archive goal (FR-02.5) - Restore goal (FR-02.5) - Delete goal (FR-02.6) - Get goals by status (FR-02.x)

### ICourse (Course Service - FR-03 | Owner: Ahmed Mohamed Ahmed)
- **High level:** - Manage courses - Track course progress
- **Low level:** Manage courses: - Add course (FR-03.1, FR-03.2) - Update course (FR-03.4) - Remove course (FR-03.5) | Track course progress: - View course progress (FR-03.3)

### IAssessment (Assessment Service - FR-04 | Owner: Ahmed Mohamed Ahmed)
- **High level:** - Manage assignments - Manage exams - Track assignment status
- **Low level:** Manage assignments: - Add assignment (FR-04.1) | Manage exams: - Add exam (FR-04.2) | Track assignment status: - Set assignment status (FR-04.3) - Set preparation reminders for exams (FR-04.4)

### IGoalPlanner (Goal Planner Service - FR-05 | Owner: Yomna Mohamed Fekry)
- **High level:** - Plan goals with AI
- **Low level:** Plan goals with AI: - Generate goal plan (FR-05.1, FR-05.2) - Review and edit AI plan (FR-05.3, FR-05.4, FR-05.5) - Approve AI plan (FR-05.6) - Discard draft (FR-05.6) - Get tasks for goal (FR-05.x)

### IStudyPlanner (Study Planner Service - FR-06 | Owner: Yomna Mohamed Fekry)
- **High level:** - Generate study plans
- **Low level:** Generate study plans: - Generate study plan (FR-06.1, FR-06.2, FR-06.4, FR-06.5) - Regenerate study plan (FR-06.3)

### IScheduling (Scheduling Service - FR-07 | Owner: Yomna Mohamed Fekry)
- **High level:** - Enable smart scheduling - Resolve conflicts - Manual adjustments
- **Low level:** Enable smart scheduling: - Auto-schedule tasks (FR-07.1) | Resolve conflicts: - Detect conflicts (FR-07.2) - Suggest alternatives (FR-07.3) | Manual adjustments: - Adjust schedule manually (FR-07.4) - Mark task done (FR-07.5)

### IAdaptation (Adaptation Service - FR-08 | Owner: Yomna Mohamed Fekry)
- **High level:** - Enable adaptive planning
- **Low level:** Enable adaptive planning: - Detect falling behind (FR-08.1) - Get re-planning recommendations (FR-08.2, FR-08.3) - Respond to recommendations (FR-08.4, FR-08.5, FR-08.6, FR-08.7)

### ICareer (Career Service - FR-09 | Owner: Arwa Mohey Hamdy)
- **High level:** - Manage career roadmaps
- **Low level:** Manage career roadmaps: - Choose target career (FR-09.1, FR-09.2) - Generate career roadmap (FR-09.3) - Track roadmap progress (FR-09.4) - Change target career (FR-09.5, FR-09.6)

### ISkill (Skill Service - FR-10 | Owner: Arwa Mohey Hamdy)
- **High level:** - Track skills
- **Low level:** Track skills: - Add skill (FR-10.1) - View skill gaps (FR-10.2, FR-10.3) - Update skill level (FR-10.4)

### IPortfolio (Portfolio Service - FR-11 | Owner: Arwa Mohey Hamdy)
- **High level:** - Manage projects and portfolios
- **Low level:** Manage projects and portfolios: - Add project (FR-11.1, FR-11.4) - Link project to skills (FR-11.2) - View portfolio (FR-11.3)

### IJobPrep (Job Preparation Service - FR-12 | Owner: Arwa Mohey Hamdy)
- **High level:** - Manage internship and job preparation
- **Low level:** Manage internship and job preparation: - Track applications (FR-12.1) - Interview preparation (FR-12.2) - Track CV and portfolio updates (FR-12.3, FR-12.4) - Record outcome and feedback (FR-12.5, FR-12.6)

### IHabit (Habit Service - FR-13 | Owner: Mostafa Khamis Abozead)
- **High level:** - Manage habits
- **Low level:** Manage habits: - Create habit (FR-13.1) - Complete habit and keep streak (FR-13.2, FR-13.3) - Link habit to goal (FR-13.4)

### IAnalytics (Analytics Service - FR-14 | Owner: Mostafa Khamis Abozead)
- **High level:** - Analyze study workload
- **Low level:** Analyze study workload: - View goal workload (FR-14.1, FR-14.2, FR-14.3, FR-14.4) - Detect overloaded goal (FR-14.5)

### IPrediction (Prediction Service - FR-15 | Owner: Mostafa Khamis Abozead)
- **High level:** - Predict and warn early
- **Low level:** Predict and warn early: - Completion likelihood (FR-15.1) - Early warning (FR-15.2, FR-15.3) - Recovery actions (FR-15.4)

### ICoach (AI Coach Service - FR-16 | Owner: Mostafa Khamis Abozead)
- **High level:** - Coach students with AI
- **Low level:** Coach students with AI: - Personalized study advice (FR-16.1, FR-16.2) - Recovery strategy (FR-16.3) - Motivational guidance (FR-16.4)

### IDashboard (Dashboard Service - FR-17 | Owner: Muhammed Hany Hamdy)
- **High level:** - Display student dashboard
- **Low level:** Display student dashboard: - Show today and deadlines (FR-17.1) - Show goal status (FR-17.2, FR-17.3) - Show habit streaks (FR-17.4) - Show workload summary (FR-17.5) - Show career progress (FR-17.6)

### INotification (Notification Service - FR-18 | Owner: Muhammed Hany Hamdy)
- **High level:** - Send notifications
- **Low level:** Send notifications: - Deadline and milestone reminders (FR-18.1, FR-18.5) - Task and habit reminders (FR-18.2, FR-18.3) - Early warning notification (FR-18.4) - Streak milestone notification (FR-18.6)

### IStudyGroup (Study Group Service - FR-19 | Owner: Muhammed Hany Hamdy)
- **High level:** - Manage study groups and accountability
- **Low level:** Manage study groups and accountability: - Create study group (FR-19.1) - Join study group (FR-19.2) - Share and track group progress (FR-19.3, FR-19.4, FR-19.7) - Accountability partner (FR-19.5, FR-19.6) - Support messages (FR-19.8)

### IAdministration (Administration Service - FR-20 | Owner: Muhammed Hany Hamdy)
- **High level:** - Manage administration
- **Low level:** Manage administration: - View and update student account (FR-20.1, FR-20.3) - Create student account (FR-20.2) - Suspend student account (FR-20.4) - Usage statistics (FR-20.5, FR-20.6, FR-20.7) - Resolve account issues (FR-20.8, FR-20.9) - Retention metrics (FR-20.10)

## Required Interfaces (consumed by services / external)

### IAIProvider (External - AI Provider)
- **High level (required):** - Provide AI capabilities
- **Low level:**
  - generate(prompt): AIResponse (used by: Generate goal plan - FR-05.1, US-05.1; Generate study plan - FR-06.1, US-06.1; Generate career roadmap - FR-09.3, US-09.2; Personalized study advice - FR-16.1, US-16.1)

### IEmailGateway (External - Email Gateway)
- **High level (required):** - Send emails
- **Low level:**
  - send(to, subject, body): void (used by: Deadline and milestone reminders - FR-18.1, US-18.1; Task and habit reminders - FR-18.2, US-18.2; Early warning notification - FR-18.4, US-18.3; Streak milestone notification - FR-18.6, US-18.4)

### Cross-service required interfaces (consumed from other service containers)
- **IStudentProfile** (required by: Academic Service - FR-02; Scheduling Service - FR-07; Career Service - FR-09; Analytics Service - FR-14; Study Group Service - FR-19; Administration Service - FR-20)  
  - createProfile/updateProfile/getProfile/getAvailability (used by: Create Goal context - US-02.1; Smart scheduling - US-07.1; Choose target career context - US-09.1; Workload analysis - US-14.1; Study groups - US-19.1-19.3; Account management - US-20.1)
- **IGoal** (required by: Goal Planner Service - FR-05; Habit Service - FR-13; AI Coach Service - FR-16; Dashboard Service - FR-17; Study Group Service - FR-19)  
  - createGoal/updateGoal/completeGoal/archiveGoal/restoreGoal/deleteGoal/getGoals (used by: Generate goal plan approval - US-05.3; Link habit to goal - US-13.3; Coach advice - US-16.1; Dashboard goal status - US-17.2; Share goals in groups - US-19.3)
- **ICourse** (required by: Study Planner Service - FR-06)  
  - getCourses/enrollments/progress (used by: Generate study plan - US-06.1)
- **IAssessment** (required by: Course Service - FR-03; Scheduling Service - FR-07; Notification Service - FR-18)  
  - addAssignment/addExam/getAssessments/setStatus (used by: View course progress - US-03.2; Auto-schedule tasks - US-07.1; Deadline reminders - US-18.1)
- **IGoalPlanner** (required by: Scheduling Service - FR-07)  
  - getTasks/approve/discard (used by: Auto-schedule tasks - US-07.1)
- **IScheduling** (required by: Study Planner Service - FR-06; Adaptation Service - FR-08; Analytics Service - FR-14; Dashboard Service - FR-17; Notification Service - FR-18)  
  - schedule/autoSchedule/getSchedule/conflicts/markDone (used by: Generate study plan placement - US-06.1; Detect falling behind - US-08.1; Workload analysis - US-14.1; Dashboard today/deadlines - US-17.1; Task reminders - US-18.2)
- **IAdaptation** (required by: Dashboard Service - FR-17)  
  - detectBehind/getRecommendations/respond (used by: Dashboard goal status - US-17.2)
- **ICareer** (required by: Skill Service - FR-10; Dashboard Service - FR-17)  
  - getRoadmap/getTargetCareer/generateRoadmap (used by: View skill gaps - US-10.2; Dashboard career progress - US-17.5)
- **ISkill** (required by: Portfolio Service - FR-11)  
  - getSkills/addSkill/updateSkillLevel/getGaps (used by: Link project to skills - US-11.2)
- **IHabit** (required by: AI Coach Service - FR-16; Dashboard Service - FR-17; Notification Service - FR-18)  
  - createHabit/completeHabit/getStreaks/linkToGoal (used by: Coach advice - US-16.1; Dashboard habit streaks - US-17.3; Habit reminders - US-18.2)
- **IAnalytics** (required by: Prediction Service - FR-15; AI Coach Service - FR-16; Dashboard Service - FR-17; Study Group Service - FR-19)  
  - getGoalWorkload/getOverloadedGoals (used by: Completion likelihood - US-15.1; Coach advice - US-16.1; Dashboard workload - US-17.4; Group progress - US-19.3)
- **IPrediction** (required by: Notification Service - FR-18)  
  - getWarnings/getLikelihood (used by: Early warning notification - US-18.3)

## Notes
- Each service container exposes its provided interface(s). Other service containers consume them as required interfaces.
- Shared database: each service owns its own data/schema; no service reads another service's database directly.
- External systems: AI Provider (IAIProvider), Email Gateway (IEmailGateway).
