namespace Aspirra.CourseService.Models
{
    public class Course
    {
        public string CourseId { get; set; } = string.Empty;
        public string StudentId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string Instructor { get; set; } = string.Empty;
        public string Semester { get; set; } = string.Empty;
        public int Credits { get; set; }
        public int WeeklyWorkloadHours { get; set; }
    }
}
