using Aspirra.StudentService.Data;
using Aspirra.StudentService.Interfaces;
using Aspirra.StudentService.Models;
using Microsoft.EntityFrameworkCore;

namespace Aspirra.StudentService.Repositories
{
    public class ProfileRepository : IProfileRepository
    {
        private readonly StudentDbContext _context;

        public ProfileRepository(StudentDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Profile profile, CancellationToken cancellationToken = default)
        {
            await _context.Profiles.AddAsync(profile, cancellationToken);
        }

        public Task<Profile?> GetByIdAsync(string studentId, CancellationToken cancellationToken = default)
        {
            return _context.Profiles
                .Include(p => p.Availability)
                .FirstOrDefaultAsync(p => p.StudentId == studentId, cancellationToken);
        }

        public void Update(Profile profile)
        {
            var availability = profile.Availability ?? new List<TimeSlot>();

            _context.Entry(profile).State = EntityState.Modified;

            foreach (var slot in availability)
            {
                var entry = _context.Entry(slot);
                if (entry.State == EntityState.Detached)
                {
                    entry.State = EntityState.Added;
                }
            }

            var removedSlots = _context.ChangeTracker.Entries<TimeSlot>()
                .Where(entry => entry.Entity.StudentId == profile.StudentId &&
                                !availability.Contains(entry.Entity))
                .ToList();

            foreach (var entry in removedSlots)
            {
                entry.State = EntityState.Deleted;
            }
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return _context.SaveChangesAsync(cancellationToken);
        }
    }
}
