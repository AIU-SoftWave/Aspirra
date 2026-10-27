using Aspirra.AssessmentService.Interfaces;
using Aspirra.AssessmentService.Models;

namespace Aspirra.AssessmentService.Services
{
    public class AssessmentService : IAssessment
    {
        public Assignment AddAssignment(Assignment assignment) => assignment;
        public Exam AddExam(Exam exam) => exam;
        public void SetAssignmentStatus(string assignmentId, string status) { }
        public void SetExamReminder(string examId, int daysBefore) { }
        public object GetAssessments(string studentId) => new();
    }
}
