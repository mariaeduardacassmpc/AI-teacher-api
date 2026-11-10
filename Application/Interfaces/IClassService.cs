using Application.DTOs;

namespace Application.Interfaces;

public interface IClassService
{
    Task<IReadOnlyList<ClassResponse>> GetAllAsync(CancellationToken ct);
    Task<ClassResponse?> GetByIdAsync(int id, CancellationToken ct);
    Task<ClassResponse> CreateAsync(ClassRequest request, CancellationToken ct);
    Task<bool> UpdateAsync(int id, UpdateClassRequest request, CancellationToken ct);
    Task<bool> ToggleActiveAsync(int id, CancellationToken ct);
}
