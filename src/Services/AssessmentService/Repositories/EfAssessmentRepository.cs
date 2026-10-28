using Aspirra.AssessmentService.Data;
using Aspirra.AssessmentService.Interfaces;
using Aspirra.AssessmentService.Models;
using Microsoft.EntityFrameworkCore;

namespace Aspirra.AssessmentService.Repositories
{
    public class EfAssessmentRepository : IAssessmentRepository
    {
        private readonly AssessmentDbContext _context;

        public EfAssessmentRepository(AssessmentDbContext context)
        {
            _context = context;
        }

        public async Task AddAssignmentAsync(Assignment assignment, CancellationToken cancellationToken = default)
        {
            await _context.Assignments.AddAsync(assignment, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task AddExamAsync(Exam exam, CancellationToken cancellationToken = default)
        {
            await _context.Exams.AddAsync(exam, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public Task<Assignment?> GetAssignmentAsync(string assignmentId, CancellationToken cancellationToken = default)
        {
            return _context.Assignments.FirstOrDefaultAsync(x => x.AssignmentId == assignmentId, cancellationToken);
        }

        public Task<Exam?> GetExamAsync(string examId, CancellationToken cancellationToken = default)
        {
            return _context.Exams.FirstOrDefaultAsync(x => x.ExamId == examId, cancellationToken);
        }

        public async Task<IReadOnlyList<Assignment>> GetAssignmentsAsync(string? studentId, string? courseId, CancellationToken cancellationToken = default)
        {
            var query = _context.Assignments.AsNoTracking().AsQueryable();

            if (!string.IsNullOrWhiteSpace(studentId))
            {
                query = query.Where(x => x.StudentId == studentId);
            }

            if (!string.IsNullOrWhiteSpace(courseId))
            {
                query = query.Where(x => x.CourseId == courseId);
            }

            return await query
                .OrderBy(x => x.DueDate)
                .ThenBy(x => x.Title)
                .ToListAsync(cancellationToken);
        }

        public async Task<IReadOnlyList<Assignment>> GetAssignmentsByCourseAsync(string courseId, CancellationToken cancellationToken = default)
        {
            return await _context.Assignments
                .AsNoTracking()
                .Where(x => x.CourseId == courseId)
                .OrderBy(x => x.DueDate)
                .ThenBy(x => x.Title)
                .ToListAsync(cancellationToken);
        }

        public async Task<IReadOnlyList<Exam>> GetExamsAsync(string? studentId, string? courseId, CancellationToken cancellationToken = default)
        {
            var query = _context.Exams.AsNoTracking().AsQueryable();

            if (!string.IsNullOrWhiteSpace(studentId))
            {
                query = query.Where(x => x.StudentId == studentId);
            }

            if (!string.IsNullOrWhiteSpace(courseId))
            {
                query = query.Where(x => x.CourseId == courseId);
            }

            return await query
                .OrderBy(x => x.ExamDate)
                .ThenBy(x => x.ExamTime)
                .ToListAsync(cancellationToken);
        }

        public async Task<IReadOnlyList<Exam>> GetUpcomingExamsAsync(string? studentId, DateOnly fromDate, CancellationToken cancellationToken = default)
        {
            var query = _context.Exams
                .AsNoTracking()
                .Where(x => x.ExamDate >= fromDate);

            if (!string.IsNullOrWhiteSpace(studentId))
            {
                query = query.Where(x => x.StudentId == studentId);
            }

            return await query
                .OrderBy(x => x.ExamDate)
                .ThenBy(x => x.ExamTime)
                .ToListAsync(cancellationToken);
        }

        public Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return _context.SaveChangesAsync(cancellationToken);
        }
    }
}
