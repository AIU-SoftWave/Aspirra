using Aspirra.AssessmentService.Contracts;
using Aspirra.AssessmentService.Data;
using Aspirra.AssessmentService.Interfaces;
using Aspirra.AssessmentService.Models;
using Aspirra.AssessmentService.Repositories;
using Microsoft.EntityFrameworkCore;
using AssessmentServiceUnderTest = Aspirra.AssessmentService.Services.AssessmentService;

namespace AssessmentService.Tests
{
    public class AssessmentServiceTests
    {
        private static readonly DateOnly Today = DateOnly.FromDateTime(DateTime.UtcNow);

        [Fact]
        public async Task AddAssignment_WithRequiredFields_SetsNotStarted()
        {
            await using var context = CreateContext();
            var service = CreateService(context);

            var result = await service.AddAssignmentAsync(new CreateAssignmentRequest
            {
                Title = "Write essay",
                CourseId = "CS101",
                StudentId = "S1",
                DueDate = Today.AddDays(7)
            });

            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Value);
            Assert.Equal(AssignmentStatus.NotStarted, result.Value!.Status);
            Assert.Single(context.Assignments);
        }

        [Fact]
        public async Task AddAssignment_MissingTitle_ReturnsRequiredFieldError()
        {
            await using var context = CreateContext();
            var service = CreateService(context);

            var result = await service.AddAssignmentAsync(new CreateAssignmentRequest
            {
                Title = " ",
                CourseId = "CS101",
                DueDate = Today.AddDays(7)
            });

            Assert.False(result.IsSuccess);
            Assert.NotNull(result.Error);
            Assert.Equal("REQUIRED_FIELD", result.Error!.Code);
            Assert.Equal("Title is required.", result.Error.Message);
        }

        [Fact]
        public async Task AddExam_WithValidData_IsAddedToUpcoming()
        {
            await using var context = CreateContext();
            var service = CreateService(context);

            var result = await service.AddExamAsync(new CreateExamRequest
            {
                Title = "Midterm",
                CourseId = "CS101",
                StudentId = "S1",
                ExamDate = Today.AddDays(5),
                ExamTime = new TimeOnly(10, 30),
                Location = "Hall A"
            });

            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Value);

            var upcoming = await service.GetUpcomingExamsAsync("S1");
            Assert.Contains(upcoming, x => x.ExamId == result.Value!.ExamId);
        }

        [Fact]
        public async Task AddExam_PastDate_ReturnsInvalidDateError()
        {
            await using var context = CreateContext();
            var service = CreateService(context);

            var result = await service.AddExamAsync(new CreateExamRequest
            {
                Title = "Old exam",
                CourseId = "CS101",
                StudentId = "S1",
                ExamDate = Today.AddDays(-1),
                ExamTime = new TimeOnly(9, 0)
            });

            Assert.False(result.IsSuccess);
            Assert.NotNull(result.Error);
            Assert.Equal("INVALID_DATE", result.Error!.Code);
            Assert.Equal("Exam date cannot be in the past", result.Error.Message);
        }

        [Fact]
        public async Task SetAssignmentStatus_ToCompleted_UpdatesStatus()
        {
            await using var context = CreateContext();
            var service = CreateService(context);

            var assignment = await AddAssignment(service, "S1", "CS101");
            var result = await service.SetAssignmentStatusAsync(assignment.AssignmentId, "Completed");

            Assert.True(result.IsSuccess);
            Assert.Equal(AssignmentStatus.Completed, result.Value!.Status);

            var stored = await context.Assignments.SingleAsync(x => x.AssignmentId == assignment.AssignmentId);
            Assert.Equal(AssignmentStatus.Completed, stored.Status);
        }

        [Fact]
        public async Task SetAssignmentStatus_InvalidStatus_ReturnsError()
        {
            await using var context = CreateContext();
            var service = CreateService(context);

            var assignment = await AddAssignment(service, "S1", "CS101");
            var result = await service.SetAssignmentStatusAsync(assignment.AssignmentId, "done");

            Assert.False(result.IsSuccess);
            Assert.NotNull(result.Error);
            Assert.Equal("INVALID_STATUS", result.Error!.Code);
            Assert.Equal("Invalid assignment status", result.Error.Message);
        }

        [Fact]
        public async Task SetAssignmentStatus_AssignmentNotFound_ReturnsNotFound()
        {
            await using var context = CreateContext();
            var service = CreateService(context);

            var result = await service.SetAssignmentStatusAsync("missing", "InProgress");

            Assert.False(result.IsSuccess);
            Assert.NotNull(result.Error);
            Assert.Equal("NOT_FOUND", result.Error!.Code);
            Assert.Equal("Assignment not found", result.Error.Message);
        }

        [Fact]
        public async Task SetExamReminder_StoresDaysBefore()
        {
            await using var context = CreateContext();
            var service = CreateService(context);

            var exam = await AddExam(service, "S1", "CS101");
            var result = await service.SetExamReminderAsync(exam.ExamId, 3);

            Assert.True(result.IsSuccess);
            Assert.Equal(3, result.Value!.PreparationReminderDays);

            var stored = await context.Exams.SingleAsync(x => x.ExamId == exam.ExamId);
            Assert.Equal(3, stored.PreparationReminderDays);
        }

        [Fact]
        public async Task SetExamReminder_NegativeDays_ReturnsInvalidReminder()
        {
            await using var context = CreateContext();
            var service = CreateService(context);

            var exam = await AddExam(service, "S1", "CS101");
            var result = await service.SetExamReminderAsync(exam.ExamId, -1);

            Assert.False(result.IsSuccess);
            Assert.NotNull(result.Error);
            Assert.Equal("INVALID_REMINDER", result.Error!.Code);
            Assert.Equal("Reminder must be before the exam date", result.Error.Message);
        }

        [Fact]
        public async Task GetAssignmentsByCourse_ReturnsExpectedShape()
        {
            await using var context = CreateContext();
            var service = CreateService(context);

            var first = await AddAssignment(service, "S1", "CS101", "First");
            await AddAssignment(service, "S1", "CS101", "Second");
            await AddAssignment(service, "S1", "CS102", "Other course");

            var items = await service.GetAssignmentsByCourseAsync("CS101");

            Assert.Equal(2, items.Count);
            var item = items.Single(x => x.AssignmentId == first.AssignmentId);
            Assert.Equal("First", item.Title);
            Assert.Equal(AssignmentStatus.NotStarted, item.Status);

            var propertyNames = typeof(CourseAssignmentItem)
                .GetProperties()
                .Select(x => x.Name)
                .OrderBy(x => x)
                .ToArray();

            Assert.Equal(new[] { "AssignmentId", "Status", "Title" }, propertyNames);
        }

        [Fact]
        public async Task GetAssessments_FiltersByStudent()
        {
            await using var context = CreateContext();
            var service = CreateService(context);

            await AddAssignment(service, "S1", "CS101", "S1 assignment");
            await AddAssignment(service, "S2", "CS101", "S2 assignment");
            await AddExam(service, "S1", "CS101", "S1 exam");
            await AddExam(service, "S2", "CS101", "S2 exam");

            var summary = await service.GetAssessmentsAsync("S1", null);

            Assert.Single(summary.Assignments);
            Assert.Single(summary.Exams);
            Assert.All(summary.Assignments, x => Assert.Equal("S1", x.StudentId));
            Assert.All(summary.Exams, x => Assert.Equal("S1", x.StudentId));
            Assert.Equal("S1 assignment", summary.Assignments[0].Title);
            Assert.Equal("S1 exam", summary.Exams[0].Title);
        }

        private static AssessmentDbContext CreateContext()
        {
            var options = new DbContextOptionsBuilder<AssessmentDbContext>()
                .UseInMemoryDatabase($"assessment-tests-{Guid.NewGuid():N}")
                .Options;

            return new AssessmentDbContext(options);
        }

        private static IAssessment CreateService(AssessmentDbContext context)
        {
            return new AssessmentServiceUnderTest(new EfAssessmentRepository(context));
        }

        private static async Task<Assignment> AddAssignment(IAssessment service, string studentId, string courseId, string title = "Assignment")
        {
            var result = await service.AddAssignmentAsync(new CreateAssignmentRequest
            {
                Title = title,
                CourseId = courseId,
                StudentId = studentId,
                DueDate = Today.AddDays(7)
            });

            Assert.True(result.IsSuccess);
            return result.Value!;
        }

        private static async Task<Exam> AddExam(IAssessment service, string studentId, string courseId, string title = "Exam")
        {
            var result = await service.AddExamAsync(new CreateExamRequest
            {
                Title = title,
                CourseId = courseId,
                StudentId = studentId,
                ExamDate = Today.AddDays(10),
                ExamTime = new TimeOnly(12, 0)
            });

            Assert.True(result.IsSuccess);
            return result.Value!;
        }
    }
}
