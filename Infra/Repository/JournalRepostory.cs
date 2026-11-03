using Application.Interfaces;
using Domain.Entities;
using Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repository;

public class JournalRepository(
    AITeacherDbContext context
) : IJournalRepository
{
    public async Task<JournalEntry> CreateAsync(JournalEntry entity, CancellationToken ct)
    {
        await context.JournalEntries.AddAsync(entity, ct);
        await context.SaveChangesAsync(ct);

        return entity;
    }

    public async Task DeleteAsync(JournalEntry entity, CancellationToken ct)
    {
        context.JournalEntries.Remove(entity);
        await context.SaveChangesAsync(ct);
    }

    public async Task<IReadOnlyList<JournalEntry>> GetAllAsync(CancellationToken ct)
    {
        return await context.JournalEntries
            .AsNoTracking()
            .Include(j => j.Class)
            .Include(j => j.Planning)
            .OrderByDescending(j => j.Date)
            .ToListAsync(ct);
    }

    public async Task<JournalEntry?> GetByIdAsync(int id, CancellationToken ct)
    {
        return await context.JournalEntries
            .Include(j => j.Class)
            .Include(j => j.Planning)
            .FirstOrDefaultAsync(j => j.Id == id, ct);
    }

    public async Task UpdateAsync(JournalEntry entity, CancellationToken ct)
    {
        context.JournalEntries.Update(entity);
        await context.SaveChangesAsync(ct);
    }
}