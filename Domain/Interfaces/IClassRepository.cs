using Domain.Entities;

namespace Domain.Interfaces;

public interface IClassRepository
{
    Task<Class?> GetByIdAsync(int id, CancellationToken ct);
    Task<IReadOnlyList<Class>> GetAllAsync(CancellationToken ct);
    Task<bool> ExistsAsync(int id, CancellationToken ct);
    Task<Class> AddAsync(Class entity, CancellationToken ct);
    Task<bool> UpdateAsync(Class entity, CancellationToken ct);
    Task<bool> DeleteAsync(int id, CancellationToken ct);
}
