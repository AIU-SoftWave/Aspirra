using Aspirra.Sprint1.Api;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<Sprint1Store>();
builder.Services.AddSingleton<StreakCalculator>();
builder.Services.AddSingleton<IAIProvider, ConfiguredAIProvider>();
builder.Services.AddSingleton<GoalPlannerService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options => options.AddDefaultPolicy(policy =>
    policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

var app = builder.Build();
app.UseCors();
app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/api/health", () => Results.Ok(new { status = "ok" }));

app.MapGet("/api/students/{studentId}/profile", (string studentId, Sprint1Store store) =>
    store.Profiles.TryGetValue(studentId, out var profile)
        ? Results.Ok(profile)
        : Results.NotFound(new { message = "Profile not found" }));

app.MapPost("/api/students/{studentId}/profile", (string studentId, ProfileRequest request, Sprint1Store store) =>
{
    var error = ProfileValidator.Validate(request);
    if (error is not null) return Results.BadRequest(new { message = error });
    var profile = new Profile(studentId, request.AcademicLevel!, request.Major!, request.Interests ?? [],
        request.Skills ?? [], request.CareerInterests ?? [], request.Availability ?? []);
    store.Profiles[studentId] = profile;
    return Results.Created($"/api/students/{studentId}/profile", profile);
});

app.MapPut("/api/students/{studentId}/profile", (string studentId, ProfileRequest request, Sprint1Store store) =>
{
    if (!store.Profiles.ContainsKey(studentId)) return Results.NotFound();
    var error = ProfileValidator.Validate(request);
    if (error is not null) return Results.BadRequest(new { message = error });
    var profile = new Profile(studentId, request.AcademicLevel!, request.Major!, request.Interests ?? [],
        request.Skills ?? [], request.CareerInterests ?? [], request.Availability ?? []);
    store.Profiles[studentId] = profile;
    return Results.Ok(profile);
});

app.MapGet("/api/students/{studentId}/goals", (string studentId, Sprint1Store store) =>
    Results.Ok(store.Goals.Values.Where(goal => goal.StudentId == studentId)));

app.MapPost("/api/students/{studentId}/goals", (string studentId, GoalRequest request, Sprint1Store store) =>
{
    var error = GoalValidator.Validate(request);
    if (error is not null) return Results.BadRequest(new { message = error });
    var goal = new Goal(Guid.NewGuid(), studentId, request.Title!, request.Description ?? "",
        request.TargetDate, request.Priority!, GoalStatus.Active, null);
    store.Goals[goal.Id] = goal;
    return Results.Created($"/api/goals/{goal.Id}", goal);
});

app.MapPut("/api/goals/{goalId:guid}", (Guid goalId, GoalRequest request, Sprint1Store store) =>
{
    if (!store.Goals.TryGetValue(goalId, out var existing)) return Results.NotFound();
    var error = GoalValidator.Validate(request);
    if (error is not null) return Results.BadRequest(new { message = error });
    var updated = existing with { Title = request.Title!, Description = request.Description ?? "",
        TargetDate = request.TargetDate, Priority = request.Priority! };
    store.Goals[goalId] = updated;
    return Results.Ok(updated);
});

app.MapPost("/api/goals/{goalId:guid}/complete", (Guid goalId, Sprint1Store store) =>
{
    if (!store.Goals.TryGetValue(goalId, out var existing)) return Results.NotFound();
    var completed = existing with { Status = GoalStatus.Completed, CompletionDate = DateOnly.FromDateTime(DateTime.UtcNow) };
    store.Goals[goalId] = completed;
    return Results.Ok(completed);
});

app.MapGet("/api/students/{studentId}/courses", (string studentId, Sprint1Store store) =>
    Results.Ok(store.Courses.Values.Where(course => course.StudentId == studentId)));

app.MapPost("/api/students/{studentId}/courses", (string studentId, CourseRequest request, Sprint1Store store) =>
{
    if (string.IsNullOrWhiteSpace(request.Name) || string.IsNullOrWhiteSpace(request.Code) ||
        string.IsNullOrWhiteSpace(request.Semester)) return Results.BadRequest(new { message = "Required field" });
    if (store.Courses.Values.Any(c => c.StudentId == studentId && c.Code == request.Code && c.Semester == request.Semester))
        return Results.Conflict(new { message = "Course already exists" });
    var course = new Course(Guid.NewGuid(), studentId, request.Name!, request.Code!, request.Instructor ?? "",
        request.Semester!, request.Credits, request.WeeklyWorkloadHours);
    store.Courses[course.Id] = course;
    return Results.Created($"/api/courses/{course.Id}", course);
});

app.MapGet("/api/courses/{courseId:guid}/progress", (Guid courseId, Sprint1Store store) =>
{
    if (!store.Courses.ContainsKey(courseId)) return Results.NotFound();
    var assignments = store.Assignments.Values.Where(item => item.CourseId == courseId).ToList();
    return Results.Ok(new CourseProgress(assignments.Where(item => item.Status == AssignmentStatus.Completed),
        assignments.Where(item => item.Status != AssignmentStatus.Completed)));
});

app.MapPost("/api/students/{studentId}/assignments", (string studentId, AssignmentRequest request, Sprint1Store store) =>
{
    if (string.IsNullOrWhiteSpace(request.Title) || request.CourseId == Guid.Empty)
        return Results.BadRequest(new { message = "Required field" });
    var assignment = new Assignment(Guid.NewGuid(), studentId, request.CourseId, request.Title!, request.Description ?? "",
        request.DueDate, request.Priority ?? "Normal", AssignmentStatus.NotStarted);
    store.Assignments[assignment.Id] = assignment;
    return Results.Created($"/api/assignments/{assignment.Id}", assignment);
});

app.MapPost("/api/students/{studentId}/exams", (string studentId, ExamRequest request, Sprint1Store store) =>
{
    if (string.IsNullOrWhiteSpace(request.Title) || request.CourseId == Guid.Empty)
        return Results.BadRequest(new { message = "Required field" });
    if (request.ExamDate < DateOnly.FromDateTime(DateTime.UtcNow))
        return Results.BadRequest(new { message = "Exam date must not be in the past" });
    var exam = new Exam(Guid.NewGuid(), studentId, request.CourseId, request.Title!, request.ExamDate,
        request.ExamTime, request.Location ?? "", request.PreparationReminderDays);
    store.Exams[exam.Id] = exam;
    return Results.Created($"/api/exams/{exam.Id}", exam);
});

app.MapPatch("/api/assignments/{assignmentId:guid}/status", (Guid assignmentId, StatusRequest request, Sprint1Store store) =>
{
    if (!store.Assignments.TryGetValue(assignmentId, out var assignment)) return Results.NotFound();
    if (!Enum.IsDefined(request.Status)) return Results.BadRequest(new { message = "Invalid status" });
    var updated = assignment with { Status = request.Status };
    store.Assignments[assignmentId] = updated;
    return Results.Ok(updated);
});

app.MapGet("/api/students/{studentId}/habits", (string studentId, Sprint1Store store) =>
    Results.Ok(store.Habits.Values.Where(habit => habit.StudentId == studentId)));

app.MapPost("/api/students/{studentId}/habits", (string studentId, HabitRequest request, Sprint1Store store) =>
{
    if (string.IsNullOrWhiteSpace(request.Name) || string.IsNullOrWhiteSpace(request.Frequency))
        return Results.BadRequest(new { message = "Required field" });
    var habit = new Habit(Guid.NewGuid(), studentId, request.Name!, request.Frequency!, request.GoalTarget ?? "",
        request.ReminderTime, [], 0);
    store.Habits[habit.Id] = habit;
    return Results.Created($"/api/habits/{habit.Id}", habit);
});

app.MapPost("/api/habits/{habitId:guid}/complete", (Guid habitId, CompletionRequest request, Sprint1Store store, StreakCalculator calculator) =>
{
    if (!store.Habits.TryGetValue(habitId, out var habit)) return Results.NotFound();
    var completions = habit.Completions.Append(request.Date).Distinct().Order().ToList();
    var updated = habit with { Completions = completions, CurrentStreak = calculator.Calculate(completions, habit.Frequency) };
    store.Habits[habitId] = updated;
    return Results.Ok(updated);
});

app.MapPost("/api/students/{studentId}/goal-plans", async (string studentId, PlanRequest request,
    GoalPlannerService planner, CancellationToken cancellationToken) =>
{
    if (string.IsNullOrWhiteSpace(request.GoalText))
        return Results.BadRequest(new { message = "Required field" });
    try { return Results.Ok(await planner.GenerateDraft(studentId, request.GoalText, cancellationToken)); }
    catch (InvalidOperationException) { return Results.StatusCode(StatusCodes.Status503ServiceUnavailable); }
});

app.MapPut("/api/goal-plans/{draftId:guid}", (Guid draftId, DraftEditRequest request, GoalPlannerService planner) =>
    planner.UpdateDraft(draftId, request) is { } draft ? Results.Ok(draft) : Results.NotFound());

app.MapPost("/api/goal-plans/{draftId:guid}/approve", (Guid draftId, GoalPlannerService planner) =>
    planner.Approve(draftId) is { } plan ? Results.Ok(plan) : Results.NotFound());

app.MapDelete("/api/goal-plans/{draftId:guid}", (Guid draftId, GoalPlannerService planner) =>
    planner.Discard(draftId) ? Results.NoContent() : Results.NotFound());

app.Run();
