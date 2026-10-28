namespace Aspirra.StudentService.Models
{
    public sealed class ProfileResponse
    {
        public string StudentId { get; init; } = string.Empty;
        public string AcademicLevel { get; init; } = string.Empty;
        public string Major { get; init; } = string.Empty;
        public List<string> Interests { get; init; } = new();
        public List<string> Skills { get; init; } = new();
        public List<string> CareerInterests { get; init; } = new();
        public List<TimeSlotResponse> Availability { get; init; } = new();
        public DateTime CreatedAt { get; init; }
        public DateTime UpdatedAt { get; init; }

        public static ProfileResponse From(Profile profile)
        {
            return new ProfileResponse
            {
                StudentId = profile.StudentId,
                AcademicLevel = string.IsNullOrWhiteSpace(profile.AcademicLevel)
                    ? "Not set"
                    : profile.AcademicLevel,
                Major = string.IsNullOrWhiteSpace(profile.Major)
                    ? "Not set"
                    : profile.Major,
                Interests = profile.Interests?.ToList() ?? new List<string>(),
                Skills = profile.Skills?.ToList() ?? new List<string>(),
                CareerInterests = profile.CareerInterests?.ToList() ?? new List<string>(),
                Availability = profile.Availability?
                    .Select(TimeSlotResponse.From)
                    .ToList() ?? new List<TimeSlotResponse>(),
                CreatedAt = profile.CreatedAt,
                UpdatedAt = profile.UpdatedAt
            };
        }
    }

    public sealed class TimeSlotResponse
    {
        public string TimeSlotId { get; init; } = string.Empty;
        public string StudentId { get; init; } = string.Empty;
        public string DayOfWeek { get; init; } = string.Empty;
        public TimeOnly StartTime { get; init; }
        public TimeOnly EndTime { get; init; }

        public static TimeSlotResponse From(TimeSlot slot)
        {
            return new TimeSlotResponse
            {
                TimeSlotId = slot.TimeSlotId,
                StudentId = slot.StudentId,
                DayOfWeek = slot.DayOfWeek,
                StartTime = slot.StartTime,
                EndTime = slot.EndTime
            };
        }
    }
}
