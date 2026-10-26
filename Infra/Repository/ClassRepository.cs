using Application.DTOs;
using Application.Mappers;
using Domain.Entities;
using Domain.Interfaces;
using Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories;

public class ClassRepository(
    AITeacherDbContext context
) : IClassRepository
{
    private IQueryable<Class> GetBaseQuery()
    {
        return context.Classes
            .AsNoTracking()
            .Include(c => c.Subjects)
            .Where(c => c.IsActive == true)
            .AsSplitQuery();
    }

    public async Task<Class?> GetByIdAsync(int id, CancellationToken ct)
    {
        return await GetBaseQuery().FirstOrDefaultAsync(c => c.Id == id, ct);
    }

    public async Task<IReadOnlyList<Class>> GetAllAsync(CancellationToken ct)
    {
       return await GetBaseQuery().OrderBy(c => c.Name).ToListAsync(ct);
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
            .Include(c => c.Subjects)
            .FirstOrDefaultAsync(c => c.Id == entity.Id, ct);

        if (existing is null)
            return false;

        ClassMapper.UpdateEntity(
            new UpdateClassRequest
            {
                Name = entity.Name,
                TotalStudents = entity.TotalStudents,
                Schedule = entity.Schedule
            },
            existing);

        existing.Subjects.Clear();

        foreach (var subject in entity.Subjects)
        {
            var trackedSubject = await context.Subjects
                .FirstOrDefaultAsync(s => s.Id == subject.Id, ct);

            if (trackedSubject is not null)
                existing.Subjects.Add(trackedSubject);
        }

        await context.SaveChangesAsync(ct);
        return true;
    }

    public async Task<bool> ToggleActive(int id, CancellationToken ct)
    {
        var existing = await context.Classes.FirstOrDefaultAsync(c => c.Id == id, ct);

        if (existing is null)
            return false;

        existing.IsActive = !existing.IsActive;

        await context.SaveChangesAsync(ct);

        return true;
    }
}