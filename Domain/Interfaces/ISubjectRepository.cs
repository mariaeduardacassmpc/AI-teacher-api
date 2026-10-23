using Domain.Entities;

namespace Domain.Interfaces;

public interface ISubjectRepository
{
    Task<Subject?> GetByIdAsync(int id, CancellationToken ct);
    Task<IReadOnlyList<Subject>> GetAllAsync(CancellationToken ct);
    Task<Subject> AddAsync(Subject subject, CancellationToken ct);
    Task<bool> UpdateAsync(Subject subject, CancellationToken ct);
    Task<bool> DeleteAsync(int id, CancellationToken ct);
}