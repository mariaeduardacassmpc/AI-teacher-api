using Application.Dtos.Journal;
using Domain.Entities;

namespace Application.Interfaces;

public interface IJournalService
{
    Task<IReadOnlyList<JournalEntry>> GetAllAsync(CancellationToken ct);
    Task<JournalEntry?> GetByIdAsync(int id, CancellationToken ct);
    Task<JournalEntry> CreateAsync(JournalRequest request, CancellationToken ct);
    Task<bool> UpdateAsync(int id, JournalRequest request, CancellationToken ct);
    Task<bool> DeleteAsync(int id, CancellationToken ct);
}