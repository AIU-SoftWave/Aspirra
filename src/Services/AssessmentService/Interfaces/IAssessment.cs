using Aspirra.AssessmentService.Models;

namespace Aspirra.AssessmentService.Interfaces
{
    public interface IAssessment
    {
        Assignment AddAssignment(Assignment assignment);
        Exam AddExam(Exam exam);
        void SetAssignmentStatus(string assignmentId, string status);
        void SetExamReminder(string examId, int daysBefore);
        object GetAssessments(string studentId);
    }
}
