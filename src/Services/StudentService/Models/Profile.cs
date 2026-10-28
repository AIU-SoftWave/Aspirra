namespace Aspirra.StudentService.Models
{
    public class Profile
    {
        public string StudentId { get; set; } = string.Empty;
        public string? AcademicLevel { get; set; }
        public string? Major { get; set; }
        public List<string>? Interests { get; set; }
        public List<string>? Skills { get; set; }
        public List<string>? CareerInterests { get; set; }
        public List<TimeSlot>? Availability { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public bool IsComplete() =>
            !string.IsNullOrWhiteSpace(StudentId) &&
            !string.IsNullOrWhiteSpace(AcademicLevel) &&
            !string.IsNullOrWhiteSpace(Major);
    }
}
