namespace Aspirra.HabitService.Models
{
    public class HabitLog
    {
        public string LogId { get; set; } = string.Empty;
        public string HabitId { get; set; } = string.Empty;
        public DateOnly LogDate { get; set; }
        public bool Completed { get; set; }
    }
}
