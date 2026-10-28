# Interfaces Document (Level 0)

This document lists every interface provided or required by Aspirra. For each interface it gives the **high-level** functional requirements and the **low-level** functional requirements grouped under each high-level FR. For every required interface operation, the low-level FR that uses it is written as `(used by: ...)`.

Sources of truth: `docs/proposal/04-B4-functional-requirements.md` (FR-01–FR-20), the Container Diagram, and the Jira backlog.

## Provided Interfaces (exposed by Aspirra service containers)

### IStudentProfile — Student Service — Owner: Ahmed Mohamed Ahmed
**IStudentProfile — high level:** - Manage Student Profile (FR-01)

**IStudentProfile — low level:** Manage Student Profile: - `createProfile(studentId, data)` (FR-01.1, FR-01.4) - `updateProfile(studentId, data)` (FR-01.2, FR-01.4) - `getProfile(studentId)` (FR-01.3) - `getAvailability(studentId)` (FR-01.3)

### IGoal — Academic Service — Owner: Ahmed Mohamed Ahmed
**IGoal — high level:** - Manage Academic Goals (FR-02)

**IGoal — low level:** Manage Academic Goals: - `createGoal(studentId, data)` (FR-02.1) - `updateGoal(goalId, data)` (FR-02.2, FR-02.3) - `completeGoal(goalId)` (FR-02.4) - `archiveGoal(goalId)` (FR-02.5) - `restoreGoal(goalId)` (FR-02.5) - `deleteGoal(goalId)` (FR-02.6) - `getGoal(goalId)` / `getGoals(studentId, status)` (FR-02.2, FR-02.4)

### ICourse — Course Service — Owner: Ahmed Mohamed Ahmed
**ICourse — high level:** - Manage Courses (FR-03)

**ICourse — low level:** Manage Courses: - `addCourse(studentId, data)` (FR-03.1, FR-03.2) - `updateCourse(courseId, data)` (FR-03.4) - `removeCourse(courseId)` (FR-03.5) - `getCourse(courseId)` / `getCourses(studentId)` (FR-03.4) | Track Course Progress: - `getCourseProgress(courseId)` (FR-03.3)

### IAssessment — Assessment Service — Owner: Ahmed Mohamed Ahmed
**IAssessment — high level:** - Manage Assignments and Exams (FR-04)

**IAssessment — low level:** Manage Assignments and Exams: - `addAssignment(data)` (FR-04.1) - `addExam(data)` (FR-04.2) | Track Assignment Status: - `setAssignmentStatus(assignmentId, status)` (FR-04.3) - `getAssessments(studentId)` / `getCourseAssignments(courseId)` (FR-04.3) | Manage Exam Reminders: - `setExamReminder(examId, daysBefore)` (FR-04.4) - `getUpcomingExams(studentId, days)` (FR-04.4)

### IGoalPlanner — Goal Planner Service — Owner: Yomna Mohamed Fekry
**IGoalPlanner — high level:** - Plan Goals with AI (FR-05)

**IGoalPlanner — low level:** Plan Goals with AI: - `generatePlan(studentId, goalText)` (FR-05.1, FR-05.2) - `updateDraft(draftId, edits)` (FR-05.3, FR-05.4, FR-05.5) - `approvePlan(draftId)` (FR-05.6) - `discardPlan(draftId)` (FR-05.6) - `getDraft(draftId)` (FR-05.3) - `getTasks(goalId)` (FR-05.2)

### IStudyPlanner — Study Planner Service — Owner: Yomna Mohamed Fekry
**IStudyPlanner — high level:** - Generate Study Plans (FR-06)

**IStudyPlanner — low level:** Generate Study Plans: - `generateStudyPlan(studentId, courses, availability)` (FR-06.1, FR-06.2) - `regenerateStudyPlan(studentId)` (FR-06.3) - `validateStudyPlan(plan)` (FR-06.4) - `retainPreviousPlan(studentId)` (FR-06.5)

### IScheduling — Scheduling Service — Owner: Yomna Mohamed Fekry
**IScheduling — high level:** - Enable Smart Scheduling (FR-07)

**IScheduling — low level:** Enable Smart Scheduling: - `scheduleTasks(studentId, tasks)` (FR-07.1) - `detectConflicts(schedule)` (FR-07.2) - `suggestAlternatives(conflict)` (FR-07.3) - `adjustScheduledTask(taskId, newSlot)` (FR-07.4) - `markTaskDone(taskId, completed)` (FR-07.5) - `getSchedule(studentId, range)` (FR-07.1)

### IAdaptation — Adaptation Service — Owner: Yomna Mohamed Fekry
**IAdaptation — high level:** - Enable Adaptive Planning (FR-08)

**IAdaptation — low level:** Enable Adaptive Planning: - `detectFallingBehind(studentId)` (FR-08.1) - `recommendRescheduling(studentId)` (FR-08.2) - `recommendReprioritization(studentId)` (FR-08.3) - `approveRecommendation(recommendationId)` (FR-08.4) - `acceptRecommendation(recommendationId)` (FR-08.5) - `rejectRecommendation(recommendationId)` (FR-08.6) - `modifyRecommendation(recommendationId, changes)` (FR-08.7)

### ICareer — Career Service — Owner: Arwa Mohey Hamdy
**ICareer — high level:** - Manage Career Roadmaps (FR-09)

**ICareer — low level:** Manage Career Roadmaps: - `selectTargetCareer(studentId, career)` (FR-09.1, FR-09.2) - `generateRoadmap(studentId, targetCareer)` (FR-09.3) - `trackRoadmapProgress(studentId)` (FR-09.4) - `updateTargetCareer(studentId, career)` (FR-09.5) - `regenerateRoadmap(studentId)` (FR-09.6)

### ISkill — Skill Service — Owner: Arwa Mohey Hamdy
**ISkill — high level:** - Track Skills (FR-10)

**ISkill — low level:** Track Skills: - `addSkill(studentId, skill)` (FR-10.1) - `compareSkills(studentId)` (FR-10.2) - `getSkillGaps(studentId)` (FR-10.3) - `updateSkillLevel(skillId, level)` (FR-10.4)

### IPortfolio — Portfolio Service — Owner: Arwa Mohey Hamdy
**IPortfolio — high level:** - Manage Projects and Portfolios (FR-11)

**IPortfolio — low level:** Manage Projects and Portfolios: - `addProject(studentId, project)` (FR-11.1) - `linkProjectToSkills(projectId, skillIds)` (FR-11.2) - `viewPortfolio(studentId)` (FR-11.3) - `addProjectArtifacts(projectId, links, documentation)` (FR-11.4)

### IJobPrep — Job Preparation Service — Owner: Arwa Mohey Hamdy
**IJobPrep — high level:** - Manage Internship and Job Preparation (FR-12)

**IJobPrep — low level:** Manage Internship and Job Preparation: - `trackApplication(studentId, application)` (FR-12.1) - `trackInterviewPrep(applicationId)` (FR-12.2) - `trackCvUpdates(applicationId)` (FR-12.3) - `trackPortfolioUpdates(applicationId)` (FR-12.4) - `recordOutcome(applicationId, outcome)` (FR-12.5) - `recordFeedback(applicationId, feedback)` (FR-12.6)

### IHabit — Habit Service — Owner: Mostafa Khamis Abozead
**IHabit — high level:** - Manage Habits (FR-13)

**IHabit — low level:** Manage Habits: - `createHabit(studentId, data)` (FR-13.1) - `completeHabit(habitId, date)` (FR-13.2) - `getStreaks(studentId)` (FR-13.3) - `linkHabitToGoal(habitId, goalId)` (FR-13.4) - `getHabits(studentId)` (FR-13.1)

### IAnalytics — Analytics Service — Owner: Mostafa Khamis Abozead
**IAnalytics — high level:** - Analyze Study Workload (FR-14)

**IAnalytics — low level:** Analyze Study Workload: - `calculateRemainingWorkload(studentId)` (FR-14.1) - `calculateAvailableStudyTime(studentId)` (FR-14.2) - `calculateCompletionPercentage(studentId)` (FR-14.3) - `getWorkloadSummary(studentId)` (FR-14.4) - `detectOverloadedGoal(studentId)` (FR-14.5)

### IPrediction — Prediction Service — Owner: Mostafa Khamis Abozead
**IPrediction — high level:** - Predict and Warn Early (FR-15)

**IPrediction — low level:** Predict and Warn Early: - `estimateCompletionLikelihood(studentId, goalId)` (FR-15.1) - `generateEarlyWarning(goalId)` (FR-15.2) - `identifyRiskTasks(goalId)` (FR-15.3) - `suggestRecoveryActions(goalId)` (FR-15.4)

### ICoach — AI Coach Service — Owner: Mostafa Khamis Abozead
**ICoach — high level:** - Coach Students with AI (FR-16)

**ICoach — low level:** Coach Students with AI: - `getPersonalizedRecommendations(studentId)` (FR-16.1) - `getStudyAdvice(studentId)` (FR-16.2) - `getRecoveryStrategy(studentId)` (FR-16.3) - `getMotivationalGuidance(studentId)` (FR-16.4)

### IDashboard — Dashboard Service — Owner: Muhammed Hany Hamdy
**IDashboard — high level:** - Display Student Dashboard (FR-17)

**IDashboard — low level:** Display Student Dashboard: - `getTodayAndDeadlines(studentId)` (FR-17.1) - `getGoalStatus(studentId)` (FR-17.2) - `getGoalCompletion(studentId)` (FR-17.3) - `getHabitStreaks(studentId)` (FR-17.4) - `getWorkloadSummary(studentId)` (FR-17.5) - `getCareerProgress(studentId)` (FR-17.6)

### INotification — Notification Service — Owner: Muhammed Hany Hamdy
**INotification — high level:** - Send Notifications (FR-18)

**INotification — low level:** Send Notifications: - `sendDeadlineReminder(studentId, deadline)` (FR-18.1, FR-18.5) - `sendTaskReminder(studentId, task)` (FR-18.2) - `sendHabitReminder(studentId, habit)` (FR-18.3) - `sendEarlyWarning(studentId, warning)` (FR-18.4) - `sendStreakMilestone(studentId, habit)` (FR-18.6)

### IStudyGroup — Study Group Service — Owner: Muhammed Hany Hamdy
**IStudyGroup — high level:** - Manage Study Groups and Accountability (FR-19)

**IStudyGroup — low level:** Manage Study Groups and Accountability: - `createStudyGroup(studentId, group)` (FR-19.1) - `joinStudyGroup(studentId, groupId)` (FR-19.2) - `shareGoal(groupId, goalId)` (FR-19.3) - `trackGroupProgress(groupId)` (FR-19.4) - `findAccountabilityPartner(studentId)` (FR-19.5) - `connectAccountabilityPartner(studentId, partnerId)` (FR-19.6) - `shareProgressUpdate(groupId)` (FR-19.7) - `sendSupportMessage(groupId, message)` (FR-19.8)

### IAdministration — Administration Service — Owner: Muhammed Hany Hamdy
**IAdministration — high level:** - Manage Administration (FR-20)

**IAdministration — low level:** Manage Administration: - `viewStudentAccount(studentId)` (FR-20.1) - `createStudentAccount(data)` (FR-20.2) - `updateStudentAccount(studentId, data)` (FR-20.3) - `suspendStudentAccount(studentId)` (FR-20.4) - `getUsageStatistics()` (FR-20.5, FR-20.6, FR-20.7) - `investigateAccountIssue(studentId)` (FR-20.8) - `resolveAccountIssue(studentId, resolution)` (FR-20.9) - `getRetentionMetrics()` (FR-20.10)

## Required Interfaces (consumed by Aspirra services or external systems)

### IAIProvider — External AI Provider
**IAIProvider — high level (required):** - Provide AI capabilities (supports FR-05, FR-06, FR-09, FR-16)

**IAIProvider — low level:** - `generate(prompt): AIResponse` (used by: FR-05.1, FR-05.2 — generate goal roadmap; FR-06.1 — generate study plan; FR-09.3 — generate career roadmap; FR-16.1, FR-16.2, FR-16.3 — personalized recommendations, study advice, recovery strategy)

### IEmailGateway — External Email Gateway
**IEmailGateway — high level (required):** - Send emails (supports FR-18)

**IEmailGateway — low level:** - `send(to, subject, body): void` (used by: FR-18.1 — assignment and exam deadline reminders; FR-18.2 — scheduled task reminders; FR-18.3 — scheduled habit reminders; FR-18.4 — early warning notifications; FR-18.5 — goal milestone reminders; FR-18.6 — habit streak milestone notifications)

### IStudentProfile — required by Scheduling, Career, Analytics, Study Group, and Administration services
**IStudentProfile — high level (required):** - Manage Student Profile (supports FR-07, FR-09, FR-14, FR-19, FR-20)

**IStudentProfile — low level:** - `getProfile(studentId)` (used by: FR-09.1, FR-09.2 — choose a target career; FR-19.3 — share goals in a study group; FR-20.1 — view student account) - `getAvailability(studentId)` (used by: FR-07.1 — schedule around availability; FR-14.2 — calculate remaining available study time) - `createProfile(studentId, data)` (used by: FR-20.2 — create a student account) - `updateProfile(studentId, data)` (used by: FR-20.3 — update student account details)

### IGoal — required by Goal Planner, AI Coach, Dashboard, and Study Group services
**IGoal — high level (required):** - Manage Academic Goals (supports FR-05, FR-16, FR-17, FR-19)

**IGoal — low level:** - `getGoal(goalId)` (used by: FR-05.2 — generate a plan for a goal; FR-05.6 — apply an approved plan) - `getGoals(studentId, status)` (used by: FR-16.1 — personalized recommendations; FR-17.2, FR-17.3 — dashboard goal status and completion; FR-19.3 — share goals in a study group)

### ICourse — required by Study Planner Service
**ICourse — high level (required):** - Manage Courses (supports FR-06)

**ICourse — low level:** - `getCourses(studentId)` (used by: FR-06.1 — generate a study plan from enrolled courses) - `getCourseProgress(courseId)` (used by: FR-06.1 — weigh remaining course work in the plan)

### IAssessment — required by Course, Scheduling, and Notification services
**IAssessment — high level (required):** - Manage Assignments and Exams (supports FR-03, FR-07, FR-18)

**IAssessment — low level:** - `getCourseAssignments(courseId)` (used by: FR-03.3 — view completed and outstanding assignments) - `getAssessments(studentId)` (used by: FR-07.1 — schedule around assessment deadlines; FR-18.1 — assignment deadline reminders) - `getUpcomingExams(studentId, days)` (used by: FR-18.1 — exam deadline reminders)

### IGoalPlanner — required by Scheduling Service
**IGoalPlanner — high level (required):** - Plan Goals with AI (supports FR-07)

**IGoalPlanner — low level:** - `getTasks(goalId)` (used by: FR-07.1 — schedule approved goal tasks) - `getDraft(draftId)` (used by: FR-07.1 — read task dependencies before scheduling)

### IScheduling — required by Study Planner, Adaptation, Analytics, Dashboard, and Notification services
**IScheduling — high level (required):** - Enable Smart Scheduling (supports FR-06, FR-08, FR-14, FR-17, FR-18)

**IScheduling — low level:** - `scheduleTasks(studentId, tasks)` (used by: FR-06.2 — distribute study tasks across available time slots) - `getSchedule(studentId, range)` (used by: FR-08.1 — detect falling behind; FR-14.1, FR-14.3 — calculate remaining workload and completed duration; FR-17.1 — display today's tasks; FR-18.2 — scheduled task reminders) - `markTaskDone(taskId, completed)` (used by: FR-08.1 — detect falling behind from unfinished tasks)

### IAdaptation — required by Dashboard Service
**IAdaptation — high level (required):** - Enable Adaptive Planning (supports FR-17)

**IAdaptation — low level:** - `getRecommendations(studentId)` (used by: FR-17.2 — show whether a goal needs attention) - `detectFallingBehind(studentId)` (used by: FR-17.2 — show goal risk status)

### ICareer — required by Skill and Dashboard services
**ICareer — high level (required):** - Manage Career Roadmaps (supports FR-10, FR-17)

**ICareer — low level:** - `getTargetCareer(studentId)` (used by: FR-10.2 — compare skills against the target career) - `getRequiredSkills(studentId)` (used by: FR-10.2, FR-10.3 — compare skills and identify gaps) - `getRoadmapProgress(studentId)` (used by: FR-17.6 — display career progress)

### ISkill — required by Portfolio Service
**ISkill — high level (required):** - Track Skills (supports FR-11)

**ISkill — low level:** - `getSkills(studentId)` (used by: FR-11.2 — link projects to demonstrated skills) - `updateSkillLevel(skillId, level)` (used by: FR-11.2 — reflect skills demonstrated by a linked project)

### IHabit — required by AI Coach, Dashboard, and Notification services
**IHabit — high level (required):** - Manage Habits (supports FR-16, FR-17, FR-18)

**IHabit — low level:** - `getHabits(studentId)` (used by: FR-16.1 — personalized recommendations; FR-18.3 — habit reminders; FR-18.6 — streak milestone notifications) - `getStreaks(studentId)` (used by: FR-16.1 — coaching context; FR-17.4 — display habit streaks) - `completeHabit(habitId, date)` (used by: FR-16.1 — update coaching context after a completion)

### IAnalytics — required by Prediction, AI Coach, Dashboard, and Study Group services
**IAnalytics — high level (required):** - Analyze Study Workload (supports FR-15, FR-16, FR-17, FR-19)

**IAnalytics — low level:** - `getGoalWorkload(studentId, goalId)` (used by: FR-15.1 — estimate completion likelihood; FR-15.2 — generate an early warning) - `getOverloadedGoals(studentId)` (used by: FR-15.2, FR-15.3 — raise warnings and identify risk tasks) - `getWorkloadSummary(studentId)` (used by: FR-16.2, FR-16.3 — deadline-aware advice and recovery strategy; FR-17.5 — dashboard workload summary; FR-19.4 — track group progress)

### IPrediction — required by Notification Service
**IPrediction — high level (required):** - Predict and Warn Early (supports FR-18)

**IPrediction — low level:** - `getEarlyWarnings(studentId)` (used by: FR-18.4 — send early warning notifications) - `getCompletionLikelihood(studentId, goalId)` (used by: FR-18.4 — include risk level in the early warning)

## Notes

- Every service container exposes one provided interface; other containers consume it only through the required-interface operations listed above.
- Each service owns its data and schema. Interfaces exchange identifiers and DTOs; no service reads another service's database directly.
- Implemented interfaces for Sprint 1 are traceable to code: `src/Services/StudentService/Interfaces/IStudentProfile.cs`, `src/Services/AcademicService/Interfaces/IGoal.cs`, `src/Services/CourseService/Interfaces/ICourse.cs`, `src/Services/AssessmentService/Interfaces/IAssessment.cs`, `src/Services/GoalPlannerService/Interfaces/IGoalPlanner.cs`, `src/Services/HabitService/Interfaces/IHabit.cs`.
- The external required interfaces map to configuration placeholders in `.env.example` (`AI_PROVIDER_API_KEY`) and the Notification Service's email settings (`EMAIL_GATEWAY_API_KEY`).
