# Running Aspirra Sprint 1

This guide explains how to run the current Sprint 1 implementation locally.

Sprint 1 includes the documented workflows for:

- Student profiles
- Academic goals
- Courses and course progress
- Assignments and exams
- Habits and streaks
- AI-assisted goal-plan drafts, review, approval, and discard

## Prerequisites

Install the following tools:

- .NET 8 SDK
- Node.js 20 or later
- npm

### Install .NET 8 SDK on Windows

The `dotnet` command is provided by the .NET SDK. Install the .NET 8 SDK
using `winget` from PowerShell:

```powershell
winget install --id Microsoft.DotNet.SDK.8 --exact
```

If `winget` is unavailable, download the **.NET 8 SDK x64 installer** from
the official Microsoft page:

<https://dotnet.microsoft.com/download/dotnet/8.0>

Choose **SDK**, not only **Runtime**. After installation, close all existing
PowerShell or VS Code terminal windows and open a new PowerShell window so
that the updated PATH is loaded.

Verify the installations:

```powershell
dotnet --version
node --version
npm --version
```

The first command should print an 8.0.x version. If it still reports that
`dotnet` is not recognized, check whether the executable exists:

```powershell
Test-Path "C:\Program Files\dotnet\dotnet.exe"
```

If the result is `True`, temporarily add the standard installation directory
to the current terminal PATH:

```powershell
$env:Path += ";C:\Program Files\dotnet"
dotnet --version
```

If the result is `False`, the SDK installation did not complete successfully.
Install it again and restart PowerShell.

## Start the backend

### Run the full Sprint 1 stack with Docker Compose

Docker Desktop must be installed and running. From the repository root:

```powershell
docker compose -f docker-compose.sprint1.yml up --build
```

If Docker reports an error involving
`dockerDesktopLinuxEngine/_ping`, start Docker Desktop and wait until its
status shows that the engine is running, then retry the command. You can
verify the daemon with:

```powershell
docker info
```

If the build reports:

```text
read-only file system
/var/lib/desktop-containerd/daemon/io.containerd.metadata.v1.bolt/meta.db
```

Docker Desktop's internal Linux storage needs to be restarted. Stop Docker
Desktop completely from its tray menu, start it again, wait for the engine to
be ready, and verify with `docker info`. Then retry the Compose command.

If the same error continues, inspect storage usage:

```powershell
docker system df
```

Only if you approve removing unused Docker build cache, run:

```powershell
docker builder prune
```

The command asks for confirmation and removes unused build cache only. Do not
use `docker system prune --volumes` for Sprint 1 because it can remove
unrelated containers, images, and database volumes.

Open the applications at:

```text
Frontend: http://localhost:3000
Backend:  http://localhost:5000
Swagger:  http://localhost:5000/swagger
Health:   http://localhost:5000/api/health
```

To stop the Sprint 1 containers, press `Ctrl+C`, or run from another terminal:

```powershell
docker compose -f docker-compose.sprint1.yml down
```

To rebuild after source changes:

```powershell
docker compose -f docker-compose.sprint1.yml up --build
```

To view container logs:

```powershell
docker compose -f docker-compose.sprint1.yml logs -f
```

The Sprint 1 Compose file is intentionally separate from the existing
platform-wide `docker-compose.yml`. The platform-wide file references the
future service containers, which do not yet have implementation projects.

Open PowerShell in the repository root and run:

```powershell
Set-Location ".\src\Sprint1\Aspirra.Sprint1.Api"
dotnet restore
dotnet run --urls "http://localhost:5000"
```

The backend should be available at:

```text
http://localhost:5000
```

Swagger is available at:

```text
http://localhost:5000/swagger
```

Health check:

```powershell
Invoke-WebRequest "http://localhost:5000/api/health"
```

Expected response:

```json
{"status":"ok"}
```

The backend project is:

- `src\Sprint1\Aspirra.Sprint1.Api\Aspirra.Sprint1.Api.csproj`

The solution file is:

- `src\Sprint1\Aspirra.Sprint1.sln`

## Configure the AI provider

The goal-plan endpoint uses the documented `IAIProvider` abstraction. Set an
AI provider key before starting the backend:

```powershell
$env:AI_PROVIDER_API_KEY = "your_ai_provider_key"
```

If the key is not configured, goal-plan generation returns HTTP `503
Service Unavailable`, which represents the documented AI-unavailable behavior.

The external provider request protocol is not defined in the repository yet.
The current provider implementation therefore supplies the documented
structured milestone/task shape through the injectable `IAIProvider`
boundary. Replace that implementation only when the provider contract is
defined.

## Start the frontend

Open a second PowerShell window in the repository root:

```powershell
Set-Location ".\src\WebFrontend"
npm install
$env:NEXT_PUBLIC_API_URL = "http://localhost:5000"
npm run dev
```

Open the frontend at:

```text
http://localhost:3000
```

The frontend currently provides the Sprint 1 goal, habit, and AI planning
surface. It sends requests to the backend configured by
`NEXT_PUBLIC_API_URL`; if that variable is omitted, it defaults to
`http://localhost:5000`.

## Run frontend tests

From `src\WebFrontend`:

```powershell
npm test
```

## Build the frontend

From `src\WebFrontend`:

```powershell
npm run build
```

To run the production frontend after building:

```powershell
npm start
```

## Build the backend

After installing the .NET 8 SDK:

```powershell
Set-Location ".\src\Sprint1"
dotnet restore
dotnet build ".\Aspirra.Sprint1.sln"
```

## Sprint 1 API surface

The backend routes are defined in
`src\Sprint1\Aspirra.Sprint1.Api\Program.cs`.

| Method | Route | Sprint 1 behavior |
|---|---|---|
| GET | `/api/health` | Service health check |
| GET | `/api/students/{studentId}/profile` | View profile |
| POST | `/api/students/{studentId}/profile` | Create profile |
| PUT | `/api/students/{studentId}/profile` | Update profile |
| GET | `/api/students/{studentId}/goals` | List goals |
| POST | `/api/students/{studentId}/goals` | Create goal |
| PUT | `/api/goals/{goalId}` | Edit goal |
| POST | `/api/goals/{goalId}/complete` | Complete goal |
| GET | `/api/students/{studentId}/courses` | List courses |
| POST | `/api/students/{studentId}/courses` | Add course |
| GET | `/api/courses/{courseId}/progress` | View course progress |
| POST | `/api/students/{studentId}/assignments` | Add assignment |
| POST | `/api/students/{studentId}/exams` | Add exam |
| PATCH | `/api/assignments/{assignmentId}/status` | Track assignment status |
| GET | `/api/students/{studentId}/habits` | List habits |
| POST | `/api/students/{studentId}/habits` | Create habit |
| POST | `/api/habits/{habitId}/complete` | Complete habit and calculate streak |
| POST | `/api/students/{studentId}/goal-plans` | Generate a goal-plan draft |
| PUT | `/api/goal-plans/{draftId}` | Edit a plan draft |
| POST | `/api/goal-plans/{draftId}/approve` | Approve a plan draft |
| DELETE | `/api/goal-plans/{draftId}` | Discard a plan draft |

## Example API requests

Create a goal:

```powershell
$body = @{
  title = "Reach a 3.5 GPA"
  description = "Improve semester performance"
  targetDate = "2026-12-20"
  priority = "High"
} | ConvertTo-Json

Invoke-RestMethod `
  -Method Post `
  -Uri "http://localhost:5000/api/students/demo-student/goals" `
  -ContentType "application/json" `
  -Body $body
```

Create a habit:

```powershell
$body = @{
  name = "Study mathematics"
  frequency = "daily"
} | ConvertTo-Json

Invoke-RestMethod `
  -Method Post `
  -Uri "http://localhost:5000/api/students/demo-student/habits" `
  -ContentType "application/json" `
  -Body $body
```

Generate a goal-plan draft:

```powershell
$body = @{
  goalText = "Reach a 3.5 GPA this semester"
} | ConvertTo-Json

Invoke-RestMethod `
  -Method Post `
  -Uri "http://localhost:5000/api/students/demo-student/goal-plans" `
  -ContentType "application/json" `
  -Body $body
```

## Docker status

The repository contains Docker Compose definitions for the planned complete
platform. The Sprint 1 ASP.NET Core project is located under
`src\Sprint1`, while the existing service Dockerfiles are still generic
service placeholders and do not yet build the Sprint 1 project automatically.

For reliable local Sprint 1 development, run the backend with `dotnet run` and
the frontend with `npm run dev` as described above.

## Current limitations

- Backend data is held in the Sprint 1 in-memory store and is lost when the
  backend restarts.
- PostgreSQL and EF Core persistence are listed in the documented technology
  stack but are not yet wired to the Sprint 1 store.
- Authentication and authorization are not implemented because no repository
  authentication contract exists.
- The frontend currently exposes goal, habit, and AI-planning interactions.
  Additional Sprint 1 forms still need to be connected to the API.
- Backend runtime verification requires the .NET 8 SDK.
