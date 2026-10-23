using Application.DTOs;
using Domain.Entities;

namespace Application.Interfaces;

public interface ISubjectService
{
    Task<IReadOnlyList<SubjectSummaryResponse>> GetAllAsync(CancellationToken ct);
    Task<SubjectResponse?> GetByIdAsync(int id, CancellationToken ct);
    Task<IReadOnlyList<ClassSummaryResponse>?> GetClassesAsync(int id, CancellationToken ct);
    Task<Subject> CreateAsync(SaveSubjectRequest request, CancellationToken ct);
    Task<bool> UpdateAsync(int id, SaveSubjectRequest request, CancellationToken ct);
    Task<bool> DeleteAsync(int id, CancellationToken ct);
}