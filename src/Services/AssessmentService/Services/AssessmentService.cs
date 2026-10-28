using Aspirra.AssessmentService.Contracts;
using Aspirra.AssessmentService.Interfaces;
using Aspirra.AssessmentService.Models;

namespace Aspirra.AssessmentService.Services
{
    public class AssessmentService : IAssessment
    {
        private readonly IAssessmentRepository _repository;

        public AssessmentService(IAssessmentRepository repository)
        {
            _repository = repository;
        }

        public async Task<AssessmentResult<Assignment>> AddAssignmentAsync(CreateAssignmentRequest request, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(request.Title))
            {
                return AssessmentResult<Assignment>.Failure(AssessmentErrorCodes.RequiredField, "Title is required.");
            }

            if (string.IsNullOrWhiteSpace(request.CourseId))
            {
                return AssessmentResult<Assignment>.Failure(AssessmentErrorCodes.RequiredField, "CourseId is required.");
            }

            if (request.DueDate is null)
            {
                return AssessmentResult<Assignment>.Failure(AssessmentErrorCodes.RequiredField, "DueDate is required.");
            }

            var now = DateTime.UtcNow;
            var assignment = new Assignment
            {
                AssignmentId = Guid.NewGuid().ToString(),
                CourseId = request.CourseId.Trim(),
                StudentId = request.StudentId?.Trim() ?? string.Empty,
                Title = request.Title.Trim(),
                Description = request.Description ?? string.Empty,
                DueDate = request.DueDate.Value,
                Priority = request.Priority ?? string.Empty,
                Status = AssignmentStatus.NotStarted,
                CreatedAt = now,
                UpdatedAt = now
            };

            await _repository.AddAssignmentAsync(assignment, cancellationToken);
            return AssessmentResult<Assignment>.Success(assignment);
        }

        public async Task<AssessmentResult<Exam>> AddExamAsync(CreateExamRequest request, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(request.Title))
            {
                return AssessmentResult<Exam>.Failure(AssessmentErrorCodes.RequiredField, "Title is required.");
            }

            if (string.IsNullOrWhiteSpace(request.CourseId))
            {
                return AssessmentResult<Exam>.Failure(AssessmentErrorCodes.RequiredField, "CourseId is required.");
            }

            if (request.EffectiveExamDate is null)
            {
                return AssessmentResult<Exam>.Failure(AssessmentErrorCodes.RequiredField, "ExamDate is required.");
            }

            if (request.EffectiveExamTime is null)
            {
                return AssessmentResult<Exam>.Failure(AssessmentErrorCodes.RequiredField, "ExamTime is required.");
            }

            if (request.EffectiveExamDate.Value < DateOnly.FromDateTime(DateTime.UtcNow))
            {
                return AssessmentResult<Exam>.Failure(AssessmentErrorCodes.InvalidDate, "Exam date cannot be in the past");
            }

            if (request.EffectivePreparationReminderDays is < 0)
            {
                return AssessmentResult<Exam>.Failure(AssessmentErrorCodes.InvalidReminder, "Reminder must be before the exam date");
            }

            var now = DateTime.UtcNow;
            var exam = new Exam
            {
                ExamId = Guid.NewGuid().ToString(),
                CourseId = request.CourseId.Trim(),
                StudentId = request.StudentId?.Trim() ?? string.Empty,
                Title = request.Title.Trim(),
                ExamDate = request.EffectiveExamDate.Value,
                ExamTime = request.EffectiveExamTime.Value,
                Location = request.Location ?? string.Empty,
                PreparationReminderDays = request.EffectivePreparationReminderDays,
                CreatedAt = now,
                UpdatedAt = now
            };

            await _repository.AddExamAsync(exam, cancellationToken);
            return AssessmentResult<Exam>.Success(exam);
        }

        public async Task<AssessmentResult<Assignment>> SetAssignmentStatusAsync(string assignmentId, string? status, CancellationToken cancellationToken = default)
        {
            var parsedStatus = ParseStatus(status);
            if (parsedStatus is null)
            {
                return AssessmentResult<Assignment>.Failure(AssessmentErrorCodes.InvalidStatus, "Invalid assignment status");
            }

            var assignment = await _repository.GetAssignmentAsync(assignmentId, cancellationToken);
            if (assignment is null)
            {
                return AssessmentResult<Assignment>.Failure(AssessmentErrorCodes.NotFound, "Assignment not found");
            }

            assignment.Status = parsedStatus.Value;
            assignment.UpdatedAt = DateTime.UtcNow;
            await _repository.SaveChangesAsync(cancellationToken);

            return AssessmentResult<Assignment>.Success(assignment);
        }

        public async Task<AssessmentResult<Exam>> SetExamReminderAsync(string examId, int? daysBefore, CancellationToken cancellationToken = default)
        {
            if (daysBefore is null or < 0)
            {
                return AssessmentResult<Exam>.Failure(AssessmentErrorCodes.InvalidReminder, "Reminder must be before the exam date");
            }

            var exam = await _repository.GetExamAsync(examId, cancellationToken);
            if (exam is null)
            {
                return AssessmentResult<Exam>.Failure(AssessmentErrorCodes.NotFound, "Exam not found");
            }

            exam.PreparationReminderDays = daysBefore.Value;
            exam.UpdatedAt = DateTime.UtcNow;
            await _repository.SaveChangesAsync(cancellationToken);

            return AssessmentResult<Exam>.Success(exam);
        }

        public async Task<AssessmentSummary> GetAssessmentsAsync(string? studentId, string? courseId, CancellationToken cancellationToken = default)
        {
            var assignments = await _repository.GetAssignmentsAsync(studentId, courseId, cancellationToken);
            var exams = await _repository.GetExamsAsync(studentId, courseId, cancellationToken);
            return new AssessmentSummary(assignments, exams);
        }

        public async Task<IReadOnlyList<CourseAssignmentItem>> GetAssignmentsByCourseAsync(string courseId, CancellationToken cancellationToken = default)
        {
            var assignments = await _repository.GetAssignmentsByCourseAsync(courseId, cancellationToken);
            return assignments
                .Select(x => new CourseAssignmentItem(x.AssignmentId, x.Title, x.Status))
                .ToList();
        }

        public Task<IReadOnlyList<Exam>> GetUpcomingExamsAsync(string? studentId, CancellationToken cancellationToken = default)
        {
            return _repository.GetUpcomingExamsAsync(studentId, DateOnly.FromDateTime(DateTime.UtcNow), cancellationToken);
        }

        private static AssignmentStatus? ParseStatus(string? status)
        {
            if (string.IsNullOrWhiteSpace(status))
            {
                return null;
            }

            var compact = new string(status.Where(c => !char.IsWhiteSpace(c) && c != '_' && c != '-').ToArray());

            if (compact.Equals("NotStarted", StringComparison.OrdinalIgnoreCase))
            {
                return AssignmentStatus.NotStarted;
            }

            if (compact.Equals("InProgress", StringComparison.OrdinalIgnoreCase))
            {
                return AssignmentStatus.InProgress;
            }

            if (compact.Equals("Completed", StringComparison.OrdinalIgnoreCase))
            {
                return AssignmentStatus.Completed;
            }

            return null;
        }
    }
}
