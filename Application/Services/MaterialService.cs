using Application.Dtos.Material;
using Application.Interfaces;
using Application.Mappers;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Application.Services;

public class MaterialService(
  IMaterialrepository repository,
  ISubjectRepository subjectRepository,
  IClassRepository classRepository,
  IFileStorageService fileStorageService
) : IMaterialService    
{
    public async Task<IReadOnlyList<MaterialResponse>> GetAllAsync(CancellationToken ct)
    {
        var materials = await repository.GetAllAsync(ct);

        return materials.Select(MaterialMapper.ToResponse).ToList();
    }

    public async Task<MaterialResponse?> GetByIdAsync(int id, CancellationToken ct)
    {
        var material = await repository.GetByIdAsync(id, ct);

        return material is null ? null : MaterialMapper.ToResponse(material);
    }

    public async Task<MaterialResponse> CreateAsync(MaterialUploadRequest request, CancellationToken ct)
    {
        (Subject subject, List<Class> classes) = await ValidateRelationsAsync(
        request.SubjectId,
        request.ClassIds,
        ct);

        var filePath = await SaveFileIfProvidedAsync(request.File, ct);

        var entity = new Material
        {
            Type = request.Type,
            Title = request.Title.Trim(),
            FilePath = filePath ?? request.Link?.Trim() ?? string.Empty,
            SubjectId = subject.Id,
            Topic = request.Topic.Trim(),
            Classes = classes
        };

        var created = await repository.AddAsync(entity, ct);

        return MaterialMapper.ToResponse(created);
    }

    public async Task<MaterialResponse?> UpdateAsync(int id, MaterialUploadRequest request, CancellationToken ct)
    {
        var entity = await repository.GetByIdAsync(id, ct);

        if (entity is null)
            return null;

        (Subject subject, List<Class> classes) = await ValidateRelationsAsync(
            request.SubjectId,
            request.ClassIds,
            ct);

        var filePath = await SaveFileIfProvidedAsync(request.File, ct);

        entity.Type = request.Type;
        entity.Title = request.Title.Trim();
        entity.SubjectId = subject.Id;
        entity.Subject = subject;
        entity.Topic = request.Topic.Trim();
        entity.Classes = classes;

        if (filePath is not null)
            entity.FilePath = filePath;
        else if (!string.IsNullOrWhiteSpace(request.Link))
            entity.FilePath = request.Link.Trim();

        var updated = await repository.UpdateAsync(entity, ct);

        if (!updated)
            return null;

        return MaterialMapper.ToResponse(entity);
    }

    private async Task<(Subject Subject, List<Class> Classes)> ValidateRelationsAsync(int subjectId, List<int> classIds, CancellationToken ct)
    {
        var subject = await subjectRepository.GetByIdAsync(subjectId, ct);

        if (subject is null)
            throw new InvalidOperationException("Disciplina não encontrada.");

        var ids = classIds.Distinct().ToList();

        var classes = await classRepository.GetByIdsAsync(ids, ct);

        if (classes.Count != ids.Count)
            throw new InvalidOperationException("Uma ou mais turmas não foram encontradas.");

        return (subject, classes);
    }

    private async Task<string?> SaveFileIfProvidedAsync(IFormFile? file, CancellationToken ct)
    {
        if (file is null || file.Length == 0)
            return null;

        return await fileStorageService.SaveAsync(file, ct);
    }

    public Task<bool> DeleteAsync(int id, CancellationToken ct)
    {
        return repository.DeleteAsync(id, ct);
    }

    public async Task<IReadOnlyList<MaterialResponse>> GetByClassAndSubjectNameAsync(string? className, string? subjectName, CancellationToken ct)
    {
        var materials = await repository.GetByClassAndSubjectNameAsync(
            className,
            subjectName,
            ct
        );

        return materials.Select(MaterialMapper.ToResponse).ToList();
    }
}