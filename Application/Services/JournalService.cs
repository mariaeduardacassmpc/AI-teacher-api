using Application.Dtos.Journal;
using Application.Interfaces;
using Application.Mappers;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Application.Services;

public class JournalService(
    IJournalRepository repository
) : IJournalService
{
    public async Task<IReadOnlyList<JournalEntry>> GetAllAsync(CancellationToken ct)
    {
        return await repository.GetAllAsync(ct);
    }

    public async Task<JournalEntry?> GetByIdAsync(int id, CancellationToken ct)
    {
        return await repository.GetByIdAsync(id, ct);
    }

    public async Task<JournalEntry> CreateAsync(JournalRequest request, CancellationToken ct)
    {
        var journalEntry = JournalMapper.ToEntity(request);
        return await repository.CreateAsync(journalEntry, ct);
    }

    public async Task<bool> UpdateAsync(int id, JournalRequest request, CancellationToken ct)
    {
        var journalEntry = await repository.GetByIdAsync(id, ct);

        if (journalEntry == null)
            return false;

        JournalMapper.UpdateEntity(journalEntry, request);

        await repository.UpdateAsync(journalEntry, ct);

        return true;
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken ct)
    {
        var journalEntry = await repository.GetByIdAsync(id, ct);

        if (journalEntry == null)
            return false;

        await repository.DeleteAsync(journalEntry, ct);

        return true;
    }
}