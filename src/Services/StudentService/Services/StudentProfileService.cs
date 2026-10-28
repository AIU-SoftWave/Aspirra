using Aspirra.StudentService.Interfaces;
using Aspirra.StudentService.Models;

namespace Aspirra.StudentService.Services
{
    public class StudentProfileService : IStudentProfile
    {
        private readonly IProfileRepository _repository;

        public StudentProfileService(IProfileRepository repository)
        {
            _repository = repository;
        }

        public async Task<ServiceResult<Profile>> CreateProfileAsync(
            string studentId,
            Profile data,
            CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(studentId))
            {
                return ServiceResult<Profile>.RequiredField("StudentId");
            }

            if (data is null || string.IsNullOrWhiteSpace(data.AcademicLevel))
            {
                return ServiceResult<Profile>.RequiredField("AcademicLevel");
            }

            if (string.IsNullOrWhiteSpace(data.Major))
            {
                return ServiceResult<Profile>.RequiredField("Major");
            }

            if (data.Availability is not null && !AllSlotsAreValid(data.Availability))
            {
                return ServiceResult<Profile>.InvalidTimeSlot();
            }

            var existing = await _repository.GetByIdAsync(studentId, cancellationToken);
            if (existing is not null)
            {
                return ServiceResult<Profile>.DuplicateProfile("Profile already exists");
            }

            var now = DateTime.UtcNow;
            var profile = new Profile
            {
                StudentId = studentId,
                AcademicLevel = data.AcademicLevel,
                Major = data.Major,
                Interests = data.Interests?.ToList() ?? new List<string>(),
                Skills = data.Skills?.ToList() ?? new List<string>(),
                CareerInterests = data.CareerInterests?.ToList() ?? new List<string>(),
                Availability = data.Availability?
                    .Select(slot => CreateSlot(studentId, slot))
                    .ToList() ?? new List<TimeSlot>(),
                CreatedAt = now,
                UpdatedAt = now
            };

            await _repository.AddAsync(profile, cancellationToken);
            await _repository.SaveChangesAsync(cancellationToken);

            return ServiceResult<Profile>.Success(profile);
        }

        public async Task<ServiceResult<Profile>> UpdateProfileAsync(
            string studentId,
            Profile data,
            CancellationToken cancellationToken = default)
        {
            var existing = await _repository.GetByIdAsync(studentId, cancellationToken);
            if (existing is null)
            {
                return ServiceResult<Profile>.NotFound("Profile not found");
            }

            if (data is null)
            {
                return ServiceResult<Profile>.RequiredField("AcademicLevel");
            }

            if (data.AcademicLevel is not null && string.IsNullOrWhiteSpace(data.AcademicLevel))
            {
                return ServiceResult<Profile>.RequiredField("AcademicLevel");
            }

            if (data.Major is not null && string.IsNullOrWhiteSpace(data.Major))
            {
                return ServiceResult<Profile>.RequiredField("Major");
            }

            if (data.Availability is not null && !AllSlotsAreValid(data.Availability))
            {
                return ServiceResult<Profile>.InvalidTimeSlot();
            }

            if (data.AcademicLevel is not null)
            {
                existing.AcademicLevel = data.AcademicLevel;
            }

            if (data.Major is not null)
            {
                existing.Major = data.Major;
            }

            if (data.Interests is not null)
            {
                existing.Interests = data.Interests.ToList();
            }

            if (data.Skills is not null)
            {
                existing.Skills = data.Skills.ToList();
            }

            if (data.CareerInterests is not null)
            {
                existing.CareerInterests = data.CareerInterests.ToList();
            }

            if (data.Availability is not null)
            {
                existing.Availability ??= new List<TimeSlot>();
                existing.Availability.Clear();

                foreach (var slot in data.Availability)
                {
                    existing.Availability.Add(CreateSlot(studentId, slot));
                }
            }

            existing.UpdatedAt = DateTime.UtcNow;

            _repository.Update(existing);
            await _repository.SaveChangesAsync(cancellationToken);

            return ServiceResult<Profile>.Success(existing);
        }

        public async Task<ServiceResult<Profile>> GetProfileAsync(
            string studentId,
            CancellationToken cancellationToken = default)
        {
            var profile = await _repository.GetByIdAsync(studentId, cancellationToken);

            return profile is null
                ? ServiceResult<Profile>.NotFound("Profile not found")
                : ServiceResult<Profile>.Success(profile);
        }

        public async Task<ServiceResult<List<TimeSlot>>> GetAvailabilityAsync(
            string studentId,
            CancellationToken cancellationToken = default)
        {
            var profile = await _repository.GetByIdAsync(studentId, cancellationToken);

            return profile is null
                ? ServiceResult<List<TimeSlot>>.NotFound("Profile not found")
                : ServiceResult<List<TimeSlot>>.Success(profile.Availability?.ToList() ?? new List<TimeSlot>());
        }

        private static bool AllSlotsAreValid(IEnumerable<TimeSlot> slots)
        {
            return slots.All(slot => slot.EndTime > slot.StartTime);
        }

        private static TimeSlot CreateSlot(string studentId, TimeSlot source)
        {
            return new TimeSlot
            {
                TimeSlotId = Guid.NewGuid().ToString(),
                StudentId = studentId,
                DayOfWeek = source.DayOfWeek,
                StartTime = source.StartTime,
                EndTime = source.EndTime
            };
        }
    }
}
