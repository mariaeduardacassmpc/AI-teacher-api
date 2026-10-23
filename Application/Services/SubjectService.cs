using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services;

public class SubjectService(
    ISubjectRepository repository
) : ISubjectService
{
    public async Task<IReadOnlyList<SubjectSummaryResponse>> GetAllAsync(
        CancellationToken ct)
    {
        var subjects = await repository.GetAllAsync(ct);

        return subjects
            .Select(s => new SubjectSummaryResponse(
                s.Id,
                s.Name,
                s.Description,
                s.Classes.Count))
            .ToList();
    }

    public async Task<SubjectResponse?> GetByIdAsync(
        int id,
        CancellationToken ct)
    {
        var subject = await repository.GetByIdAsync(id, ct);

        return subject is null
            ? null
            : new SubjectResponse(
                subject.Id,
                subject.Name,
                subject.Description,
                subject.Content,
                ToClassSummaries(subject));
    }

    public async Task<IReadOnlyList<ClassSummaryResponse>?> GetClassesAsync(
        int id,
        CancellationToken ct = default)
    {
        var subject = await repository.GetByIdAsync(id, ct);

        return subject is null ? null : ToClassSummaries(subject);
    }

    public async Task<Subject> CreateAsync(
        SaveSubjectRequest request,
        CancellationToken ct)
    {
        var entity = new Subject
        {
            Name = request.Name.Trim(),
            Description = request.Description,
            Content = request.Content
        };

        return await repository.AddAsync(entity, ct);
    }

    public async Task<bool> UpdateAsync(
        int id,
        SaveSubjectRequest request,
        CancellationToken ct)
    {
        var entity = await repository.GetByIdAsync(id, ct);

        if (entity is null)
            return false;

        entity.Name = request.Name.Trim();
        entity.Description = request.Description;
        entity.Content = request.Content;

        return await repository.UpdateAsync(entity, ct);
    }

    public Task<bool> DeleteAsync(int id, CancellationToken ct)
    {
        return repository.DeleteAsync(id, ct);
    }

    private static List<ClassSummaryResponse> ToClassSummaries(
        Subject subject) =>
        subject.Classes
            .OrderBy(c => c.Name)
            .Select(c => new ClassSummaryResponse(
                c.Id,
                c.Name,
                c.TotalStudents))
            .ToList();
}