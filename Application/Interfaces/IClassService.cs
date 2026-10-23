using Application.DTOs;

namespace Application.Interfaces;

public interface IClassService
{
    Task<IReadOnlyList<ClassResponse>> GetAllAsync(CancellationToken ct);
    Task<ClassResponse?> GetByIdAsync(int id, CancellationToken ct);
    Task<ClassResponse> CreateAsync(SaveClassRequest request, CancellationToken ct);
    Task<bool> UpdateAsync(int id, SaveClassRequest request, CancellationToken ct);
    Task<bool> DeleteAsync(int id, CancellationToken ct);
}