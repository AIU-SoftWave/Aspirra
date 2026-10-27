namespace Aspirra.Sprint1.Api;

public sealed class Sprint1Store
{
    public Dictionary<string, Profile> Profiles { get; } = [];
    public Dictionary<Guid, Goal> Goals { get; } = [];
    public Dictionary<Guid, Course> Courses { get; } = [];
    public Dictionary<Guid, Assignment> Assignments { get; } = [];
    public Dictionary<Guid, Exam> Exams { get; } = [];
    public Dictionary<Guid, Habit> Habits { get; } = [];
    public Dictionary<Guid, PlanDraft> Drafts { get; } = [];
}

public record TimeSlot(string DayOfWeek, TimeOnly StartTime, TimeOnly EndTime);
public record Profile(string StudentId, string AcademicLevel, string Major, List<string> Interests,
    List<string> Skills, List<string> CareerInterests, List<TimeSlot> Availability);
public record Goal(Guid Id, string StudentId, string Title, string Description, DateOnly TargetDate,
    string Priority, GoalStatus Status, DateOnly? CompletionDate);
public enum GoalStatus { Active, Completed, Archived }
public record Course(Guid Id, string StudentId, string Name, string Code, string Instructor, string Semester,
    int Credits, int WeeklyWorkloadHours);
public enum AssignmentStatus { NotStarted, InProgress, Completed }
public record Assignment(Guid Id, string StudentId, Guid CourseId, string Title, string Description,
    DateOnly DueDate, string Priority, AssignmentStatus Status);
public record Exam(Guid Id, string StudentId, Guid CourseId, string Title, DateOnly ExamDate,
    TimeOnly ExamTime, string Location, int? PreparationReminderDays);
public record CourseProgress(IEnumerable<Assignment> Completed, IEnumerable<Assignment> Outstanding);
public record Habit(Guid Id, string StudentId, string Name, string Frequency, string GoalTarget,
    TimeOnly? ReminderTime, List<DateOnly> Completions, int CurrentStreak);
public record PlanDraft(Guid Id, string StudentId, string GoalText, string Status, List<Milestone> Milestones);
public record Milestone(Guid Id, string Title, DateOnly Deadline, List<PlanTask> Tasks);
public record PlanTask(Guid Id, string Title, int DurationHours, DateOnly Deadline);
public record Plan(Guid Id, string StudentId, string GoalText, List<Milestone> Milestones);

public record ProfileRequest(string? AcademicLevel, string? Major, List<string>? Interests, List<string>? Skills,
    List<string>? CareerInterests, List<TimeSlot>? Availability);
public record GoalRequest(string? Title, string? Description, DateOnly TargetDate, string? Priority);
public record CourseRequest(string? Name, string? Code, string? Instructor, string? Semester, int Credits, int WeeklyWorkloadHours);
public record AssignmentRequest(string? Title, Guid CourseId, DateOnly DueDate, string? Priority, string? Description);
public record ExamRequest(string? Title, Guid CourseId, DateOnly ExamDate, TimeOnly ExamTime, string? Location, int? PreparationReminderDays);
public record StatusRequest(AssignmentStatus Status);
public record HabitRequest(string? Name, string? Frequency, string? GoalTarget, TimeOnly? ReminderTime);
public record CompletionRequest(DateOnly Date);
public record PlanRequest(string? GoalText);
public record DraftEditRequest(List<Milestone>? Milestones);
