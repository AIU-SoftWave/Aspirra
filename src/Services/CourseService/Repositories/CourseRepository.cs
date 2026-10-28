using Aspirra.CourseService.Data;
using Aspirra.CourseService.Interfaces;
using Aspirra.CourseService.Models;
using Microsoft.EntityFrameworkCore;

namespace Aspirra.CourseService.Repositories;

public class CourseRepository : ICourseRepository
{
    private readonly CourseDbContext _db;

    public CourseRepository(CourseDbContext db)
    {
        _db = db;
    }

    public Task<Course?> GetByIdAsync(string courseId) =>
        _db.Courses.FirstOrDefaultAsync(c => c.CourseId == courseId);

    public Task<List<Course>> GetByStudentAsync(string? studentId)
    {
        var query = _db.Courses.AsQueryable();

        if (!string.IsNullOrWhiteSpace(studentId))
        {
            query = query.Where(c => c.StudentId == studentId);
        }

        return query.OrderBy(c => c.Name).ToListAsync();
    }

    public Task<bool> ExistsAsync(string code, string semester, string? excludeCourseId = null)
    {
        var query = _db.Courses.Where(c => c.Code == code && c.Semester == semester);

        if (!string.IsNullOrWhiteSpace(excludeCourseId))
        {
            query = query.Where(c => c.CourseId != excludeCourseId);
        }

        return query.AnyAsync();
    }

    public async Task AddAsync(Course course)
    {
        _db.Courses.Add(course);
        await _db.SaveChangesAsync();
    }

    public async Task UpdateAsync(Course course)
    {
        _db.Courses.Update(course);
        await _db.SaveChangesAsync();
    }

    public async Task RemoveAsync(Course course)
    {
        _db.Courses.Remove(course);
        await _db.SaveChangesAsync();
    }
}
