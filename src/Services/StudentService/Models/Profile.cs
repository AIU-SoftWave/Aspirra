namespace Aspirra.StudentService.Models
{
    public class Profile
    {
        public string StudentId { get; set; } = string.Empty;
        public string AcademicLevel { get; set; } = string.Empty;
        public string Major { get; set; } = string.Empty;
        public List<string> Interests { get; set; } = new();
        public List<string> CareerInterests { get; set; } = new();
        public List<TimeSlot> Availability { get; set; } = new();
        public bool IsComplete() => !string.IsNullOrEmpty(StudentId) && !string.IsNullOrEmpty(Major);
    }
}
