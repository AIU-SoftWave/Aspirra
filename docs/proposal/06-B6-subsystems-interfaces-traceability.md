# B6 — Subsystems, interfaces & traceability

> Issue: #6 · Part: B · Owner: Muhammed Hany Hamdy · Reviewer: TBD · Status: Draft

## Content

### System decomposition (system → containers → components)

| Level | Element | Description |
|---|---|---|
| System | Aspirra | AI-powered student planning and career platform |
| Container | Web Frontend | Student and administrator web application (React / Next.js) |
| Container | Backend API | Application logic (ASP.NET Core); its components are the 10 subsystems below |
| Container | Relational Database | Stores the data of every subsystem |
| External system | External: AI Provider | LLM API used for goal planning, study plans, career roadmaps and coaching |
| External system | External: Email Service | Delivers reminders and warnings |

### Subsystems with their FRs

Each subsystem groups related high-level FRs. Each FR is one service, with its own feature branch in the repository and its own Jira label.

| ID | Subsystem | High-level FRs | Services |
|---|---|---|---|
| SUB-01 | Student Profile | FR-01 Manage Student Profile | student-service |
| SUB-02 | Academic Management | FR-02 Manage Academic Goals<br>FR-03 Manage Courses<br>FR-04 Manage Assignments and Exams | academic-service<br>course-service<br>assessment-service |
| SUB-03 | AI Planning & Scheduling | FR-05 Plan Goals with AI<br>FR-06 Generate Study Plans<br>FR-07 Enable Smart Scheduling<br>FR-08 Enable Adaptive Planning | goal-planner-service<br>study-planner-service<br>scheduling-service<br>adaptation-service |
| SUB-04 | Career Development | FR-09 Manage Career Roadmaps<br>FR-10 Track Skills<br>FR-11 Manage Projects and Portfolios<br>FR-12 Manage Internship and Job Preparation | career-service<br>skill-service<br>portfolio-service<br>job-preparation-service |
| SUB-05 | Habit Tracking | FR-13 Manage Habits | habit-service |
| SUB-06 | Insights & Coaching | FR-14 Analyze Study Workload<br>FR-15 Predict and Warn Early<br>FR-16 Coach Students with AI | analytics-service<br>prediction-service<br>ai-coach-service |
| SUB-07 | Student Dashboard | FR-17 Display Student Dashboard | dashboard-service |
| SUB-08 | Notifications | FR-18 Send Notifications | notification-service |
| SUB-09 | Study Groups & Accountability | FR-19 Manage Study Groups and Accountability | study-group-service |
| SUB-10 | Administration | FR-20 Manage Administration | administration-service |

### Interfaces

| Interface | Provided by | What it offers |
|---|---|---|
| IStudentProfile | SUB-01 Student Profile | Create, update and view the profile (FR-01); the student's availability and interests for other subsystems |
| IAcademic | SUB-02 Academic Management | Goals (FR-02), courses (FR-03), assignments, exams and their deadlines (FR-04) |
| IPlanning | SUB-03 AI Planning & Scheduling | AI goal plans (FR-05), study plans (FR-06), scheduled tasks and conflicts (FR-07), adaptive recommendations (FR-08) |
| ICareer | SUB-04 Career Development | Career roadmap and its progress (FR-09), skills and skill gaps (FR-10), projects and portfolio (FR-11), job applications (FR-12) |
| IHabit | SUB-05 Habit Tracking | Habits, completions and streaks (FR-13) |
| IInsights | SUB-06 Insights & Coaching | Workload per goal and overload state (FR-14), completion likelihood and early warnings (FR-15), coaching advice (FR-16) |
| IDashboard | SUB-07 Student Dashboard | The student dashboard view (FR-17) |
| INotification | SUB-08 Notifications | Reminders and notifications (FR-18) |
| IStudyGroup | SUB-09 Study Groups & Accountability | Groups, shared goals and progress, partners and messages (FR-19) |
| IAdministration | SUB-10 Administration | Account management, statistics and retention metrics (FR-20) |
| IAIProvider | External: AI Provider | Generates goal plans, study plans, career roadmaps and coaching text |
| IEmailGateway | External: Email Service | Sends emails |

### Traceability matrix

| Subsystem | High-level FRs | Provided interface | Required interfaces |
|---|---|---|---|
| SUB-01 Student Profile | FR-01 Manage Student Profile | IStudentProfile | — |
| SUB-02 Academic Management | FR-02 Manage Academic Goals, FR-03 Manage Courses, FR-04 Manage Assignments and Exams | IAcademic | IStudentProfile |
| SUB-03 AI Planning & Scheduling | FR-05 Plan Goals with AI, FR-06 Generate Study Plans, FR-07 Enable Smart Scheduling, FR-08 Enable Adaptive Planning | IPlanning | IStudentProfile, IAcademic, IAIProvider (External) |
| SUB-04 Career Development | FR-09 Manage Career Roadmaps, FR-10 Track Skills, FR-11 Manage Projects and Portfolios, FR-12 Manage Internship and Job Preparation | ICareer | IStudentProfile, IAIProvider (External) |
| SUB-05 Habit Tracking | FR-13 Manage Habits | IHabit | IAcademic |
| SUB-06 Insights & Coaching | FR-14 Analyze Study Workload, FR-15 Predict and Warn Early, FR-16 Coach Students with AI | IInsights | IStudentProfile, IAcademic, IPlanning, IHabit, IAIProvider (External) |
| SUB-07 Student Dashboard | FR-17 Display Student Dashboard | IDashboard | IAcademic, IPlanning, ICareer, IHabit, IInsights |
| SUB-08 Notifications | FR-18 Send Notifications | INotification | IAcademic, IPlanning, IHabit, IInsights, IEmailGateway (External) |
| SUB-09 Study Groups & Accountability | FR-19 Manage Study Groups and Accountability | IStudyGroup | IStudentProfile, IAcademic, IInsights |
| SUB-10 Administration | FR-20 Manage Administration | IAdministration | IStudentProfile |

## Checklist

- [x] Define subsystems and map FRs to them
- [x] Specify provided interface per subsystem
- [x] Specify required interfaces per subsystem
- [x] Build the traceability matrix
- [ ] Team review