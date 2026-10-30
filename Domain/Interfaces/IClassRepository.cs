using Domain.Entities;

namespace Domain.Interfaces;

public interface IClassRepository
{
    Task<Class?> GetByIdAsync(int id, CancellationToken ct);
    Task<IReadOnlyList<Class>> GetAllAsync(CancellationToken ct);
    Task<Class> AddAsync(Class entity, CancellationToken ct);
    Task<bool> UpdateAsync(Class entity, CancellationToken ct);
    Task<bool> ToggleActive(int id, CancellationToken ct);
    Task<List<Class>> GetByIdsAsync(List<int> ids, CancellationToken ct);
}
