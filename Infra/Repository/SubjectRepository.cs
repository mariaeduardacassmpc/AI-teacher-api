using Domain.Entities;
using Domain.Interfaces;
using Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories;

public class SubjectRepository(
    AITeacherDbContext context
) : ISubjectRepository
{
    public async Task<Subject?> GetByIdAsync(int id, CancellationToken ct)
    {
        return await context.Subjects
            .AsNoTracking()
            .Include(s => s.Classes)
            .FirstOrDefaultAsync(s => s.Id == id, ct);
    }

    public async Task<IReadOnlyList<Subject>> GetAllAsync(CancellationToken ct)
    {
        return await context.Subjects
            .AsNoTracking()
            .Include(s => s.Classes)
            .OrderBy(s => s.Name)
            .ToListAsync(ct);
    }

    public async Task<Subject> AddAsync(Subject entity, CancellationToken ct)
    {
        await context.Subjects.AddAsync(entity, ct);
        await context.SaveChangesAsync(ct);
        return entity;
    }

    public async Task<bool> UpdateAsync(Subject entity, CancellationToken ct)
    {
        var existing = await context.Subjects.FirstOrDefaultAsync(c => c.Id == entity.Id, ct);

        if (existing is null)
            return false;

        existing.Name = entity.Name;
        existing.Description = entity.Description;
        existing.Content = entity.Content;

        await context.SaveChangesAsync(ct);
        return true;
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken ct)
    {
        var existing = await context.Subjects.FirstOrDefaultAsync(c => c.Id == id, ct);

        if (existing is null)
            return false;

        context.Subjects.Remove(existing);
        await context.SaveChangesAsync(ct);
        return true;
    }
}