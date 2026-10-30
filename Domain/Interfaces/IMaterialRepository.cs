using Domain.Entities;

namespace Domain.Interfaces;

public interface IMaterialrepository
{
    Task<Material> GetByIdAsync(int id, CancellationToken ct);
    Task<IReadOnlyList<Material>> GetAllAsync(CancellationToken ct);
    Task<Material> AddAsync(Material subject, CancellationToken ct);
    Task<bool> UpdateAsync(Material subject, CancellationToken ct);
    Task<bool> DeleteAsync(int id, CancellationToken ct);
    Task<IReadOnlyList<Material>> GetByClassAndSubjectNameAsync(string? className, string? subjectName, CancellationToken ct);
}