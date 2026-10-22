using Application.DTOs;

namespace Application.Interfaces;

public interface IClassService
{
    Task<IReadOnlyList<ClassResponse>> GetAllAsync(CancellationToken ct = default);
    Task<ClassResponse?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<ClassResponse> CreateAsync(SaveClassRequest request, CancellationToken ct = default);
    Task<bool> UpdateAsync(int id, SaveClassRequest request, CancellationToken ct = default);
    Task<bool> DeleteAsync(int id, CancellationToken ct = default);
}