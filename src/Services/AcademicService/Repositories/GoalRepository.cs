using Aspirra.AcademicService.Data;
using Aspirra.AcademicService.Interfaces;
using Aspirra.AcademicService.Models;
using Microsoft.EntityFrameworkCore;

namespace Aspirra.AcademicService.Repositories
{
    public class GoalRepository : IGoalRepository
    {
        private readonly AcademicDbContext _context;

        public GoalRepository(AcademicDbContext context)
        {
            _context = context;
        }

        public async Task<Goal> AddAsync(Goal goal, CancellationToken cancellationToken = default)
        {
            await _context.Goals.AddAsync(goal, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return goal;
        }

        public Task<Goal?> GetByIdAsync(string goalId, CancellationToken cancellationToken = default)
        {
            return _context.Goals.FirstOrDefaultAsync(g => g.GoalId == goalId, cancellationToken);
        }

        public async Task Update(Goal goal, CancellationToken cancellationToken = default)
        {
            _context.Goals.Update(goal);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task Delete(Goal goal, CancellationToken cancellationToken = default)
        {
            _context.Goals.Remove(goal);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public Task UpdateAsync(Goal goal, CancellationToken cancellationToken = default)
        {
            return Update(goal, cancellationToken);
        }

        public Task DeleteAsync(Goal goal, CancellationToken cancellationToken = default)
        {
            return Delete(goal, cancellationToken);
        }

        public IQueryable<Goal> Query()
        {
            return _context.Goals.AsNoTracking();
        }
    }
}
