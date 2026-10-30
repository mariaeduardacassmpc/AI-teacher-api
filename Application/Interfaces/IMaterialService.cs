using Application.Dtos.Material;
using Domain.Entities;

namespace Application.Interfaces;

public interface IMaterialService
{
    Task<IReadOnlyList<MaterialResponse>> GetAllAsync(CancellationToken ct);
    Task<MaterialResponse?> GetByIdAsync(int ids, CancellationToken ct);
    Task<MaterialResponse> CreateAsync(MaterialUploadRequest request, CancellationToken ct);
    Task<MaterialResponse> UpdateAsync(int id, MaterialUploadRequest request, CancellationToken ct);
    Task<bool> DeleteAsync(int id, CancellationToken ct);
    Task<IReadOnlyList<MaterialResponse>> GetByClassAndSubjectNameAsync(string? className, string? subjectName, CancellationToken ct);
}