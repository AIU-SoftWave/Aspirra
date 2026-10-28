namespace Aspirra.StudentService.Models
{
    public class TimeSlot
    {
        public string TimeSlotId { get; set; } = string.Empty;
        public string StudentId { get; set; } = string.Empty;
        public string DayOfWeek { get; set; } = string.Empty;
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
    }
}
