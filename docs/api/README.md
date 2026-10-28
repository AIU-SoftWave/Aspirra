# Aspirra API Documentation

The six implemented services expose interactive OpenAPI documentation in the Development environment. Start the stack, then open a service's UI from the table below.

```bash
docker compose up -d --build
```

| Service | Interactive UI | OpenAPI JSON | Health |
| --- | --- | --- | --- |
| Student | http://localhost:5011/scalar/v1 | http://localhost:5011/openapi/v1.json | http://localhost:5011/health |
| Academic | http://localhost:5012/scalar/v1 | http://localhost:5012/openapi/v1.json | http://localhost:5012/health |
| Course | http://localhost:5013/scalar/v1 | http://localhost:5013/openapi/v1.json | http://localhost:5013/health |
| Assessment | http://localhost:5014/scalar/v1 | http://localhost:5014/openapi/v1.json | http://localhost:5014/health |
| Goal Planner | http://localhost:5015/scalar/v1 | http://localhost:5015/openapi/v1.json | http://localhost:5015/health |
| Habit | http://localhost:5016/scalar/v1 | http://localhost:5016/openapi/v1.json | http://localhost:5016/health |

The UI and OpenAPI JSON are enabled only when `ASPNETCORE_ENVIRONMENT=Development`. Docker Compose sets this by default; `dotnet run` also defaults to Development.

## Suggested test flow

Each UI can call its own service. Use the generated IDs from responses in the later requests.

### 1. Student profile

`POST /api/profiles/student-demo`

```json
{
  "academicLevel": "Sophomore",
  "major": "Computer Science",
  "interests": ["backend", "databases"],
  "skills": ["C#", "SQL"],
  "careerInterests": ["software engineering"],
  "availability": [
    {
      "dayOfWeek": "Monday",
      "startTime": "18:00:00",
      "endTime": "20:00:00"
    }
  ]
}
```

Then call `GET /api/profiles/student-demo` and `GET /api/profiles/student-demo/availability`.

### 2. Academic goal

`POST /api/goals?studentId=student-demo`

```json
{
  "studentId": "student-demo",
  "title": "Finish the software engineering project",
  "description": "Complete the remaining milestones and prepare the demo.",
  "targetDate": "2026-12-31",
  "priority": "High"
}
```

Copy the returned `goalId` and use it with `POST /api/plans/generate` in the Goal Planner service.

### 3. Goal plan

`POST /api/plans/generate`

```json
{
  "studentId": "student-demo",
  "goalId": "<goalId from Academic>",
  "goalText": "Finish the software engineering project"
}
```

Use the returned `draftId` for `GET /api/plans/{draftId}`, `PUT /api/plans/{draftId}`, and `POST /api/plans/{draftId}/approve`. Approval verifies the goal through the Academic service.

### 4. Course and assessment

`POST /api/courses`

```json
{
  "studentId": "student-demo",
  "name": "Advanced Software Engineering",
  "code": "CSE454",
  "instructor": "Course Instructor",
  "semester": "2026 Fall",
  "credits": 3,
  "weeklyWorkloadHours": 6
}
```

`POST /api/assignments`

```json
{
  "title": "PM1 submission",
  "courseId": "<courseId from Course>",
  "studentId": "student-demo",
  "description": "Submit the design and DevOps report.",
  "dueDate": "2026-12-15",
  "priority": "High"
}
```

Then call `PATCH /api/assignments/{assignmentId}/status` with `{"status":"Completed"}` and `GET /api/courses/{courseId}/progress`. The Course service calls the Assessment service to build the progress response.

### 5. Habit

`POST /api/habits`

```json
{
  "studentId": "student-demo",
  "goalId": "<goalId from Academic>",
  "name": "Study two hours",
  "frequency": "Daily",
  "goalTarget": "Complete 20 sessions",
  "reminderTime": "18:30:00"
}
```

Then call `POST /api/habits/{habitId}/complete`, `GET /api/habits/streaks?studentId=student-demo`, and `GET /api/habits?studentId=student-demo`.

## Using the OpenAPI JSON elsewhere

The JSON documents can be imported into Postman, Insomnia, or other OpenAPI-compatible tools:

```bash
curl -sS http://localhost:5011/openapi/v1.json | jq '.info'
```

The API is unauthenticated in the current sprint, so no token configuration is required.
