namespace Aspirra.StudentService.Models
{
    public class TimeSlot
    {
        public string Day { get; set; } = string.Empty;
        public TimeOnly Start { get; set; }
        public TimeOnly End { get; set; }
    }
}
