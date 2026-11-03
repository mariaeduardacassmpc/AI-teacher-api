using Domain.Entities;

namespace Application.Interfaces;

public interface IJournalRepository
{
    Task<IReadOnlyList<JournalEntry>> GetAllAsync(CancellationToken ct);
    Task<JournalEntry?> GetByIdAsync(int id, CancellationToken ct);
    Task<JournalEntry> CreateAsync(JournalEntry entity, CancellationToken ct);
    Task UpdateAsync(JournalEntry entity, CancellationToken ct);
    Task DeleteAsync(JournalEntry entity, CancellationToken ct);
}