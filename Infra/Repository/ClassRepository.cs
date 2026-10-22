using Domain.Entities;
using Domain.Interfaces;
using Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories;

public class ClassRepository(
    AITeacherDbContext context
) : IClassRepository
{
    public async Task<Class?> GetByIdAsync(int id, CancellationToken ct)
    {
        return await context.Classes
            .AsNoTracking()
            .Include(c => c.Schedules)
            .Include(c => c.Subjects)
            .AsSplitQuery()
            .FirstOrDefaultAsync(c => c.Id == id, ct);
    }

    public async Task<IReadOnlyList<Class>> GetAllAsync(CancellationToken ct)
    {
        return await context.Classes
            .AsNoTracking()
            .Include(c => c.Schedules)
            .Include(c => c.Subjects)
            .AsSplitQuery()
            .OrderBy(c => c.Name)
            .ToListAsync(ct);
    }

    public Task<bool> ExistsAsync(int id, CancellationToken ct)
    {
        return context.Classes.AnyAsync(c => c.Id == id, ct);
    }

    public async Task<Class> AddAsync(Class entity, CancellationToken ct)
    {
        await context.Classes.AddAsync(entity, ct);
        await context.SaveChangesAsync(ct);
        return entity;
    }

    public async Task<bool> UpdateAsync(Class entity, CancellationToken ct)
    {
        var existing = await context.Classes
            .Include(c => c.Schedules)
            .FirstOrDefaultAsync(c => c.Id == entity.Id, ct);

        if (existing is null)
            return false;

        existing.Name = entity.Name;
        existing.TotalStudents = entity.TotalStudents;
        existing.Hour = entity.Hour;

        SyncSchedules(existing, entity.Schedules);

        await context.SaveChangesAsync(ct);
        return true;
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken ct)
    {
        var existing = await context.Classes.FirstOrDefaultAsync(c => c.Id == id, ct);

        if (existing is null)
            return false;

        context.Classes.Remove(existing);
        await context.SaveChangesAsync(ct);
        return true;
    }

    private static void SyncSchedules(Class existing, List<ClassSchedule> incoming)
    {
        var incomingIds = incoming
            .Where(s => s.Id != 0)
            .Select(s => s.Id)
            .ToHashSet();

        existing.Schedules.RemoveAll(s => !incomingIds.Contains(s.Id));

        foreach (var schedule in incoming)
        {
            var current = schedule.Id == 0
                ? null
                : existing.Schedules.FirstOrDefault(s => s.Id == schedule.Id);

            if (current is null)
            {
                existing.Schedules.Add(new ClassSchedule
                {
                    DayOfWeek = schedule.DayOfWeek,
                    StartTime = schedule.StartTime,
                    EndTime = schedule.EndTime
                });
                continue;
            }

            current.DayOfWeek = schedule.DayOfWeek;
            current.StartTime = schedule.StartTime;
            current.EndTime = schedule.EndTime;
        }
    }
}