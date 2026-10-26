using Application.DTOs;
using Application.Interfaces;
using Application.Mappers;
using Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace Application.Services;

public class ClassService(
    IClassRepository repository,
    ISubjectRepository subjectRepository,
     ILogger<ClassService> logger
) : IClassService
{
    public async Task<IReadOnlyList<ClassResponse>> GetAllAsync(CancellationToken ct)
    {
        logger.LogInformation("Buscando todas os Turmas");

        var classes = await repository.GetAllAsync(ct);
        return classes.Select(ClassMapper.ToResponse).ToList();
    }

    public async Task<ClassResponse?> GetByIdAsync(int id, CancellationToken ct)
    {
        logger.LogInformation("Buscando Turma por Id: {id}", id);

        var entity = await repository.GetByIdAsync(id, ct);

        if (entity == null)
        {
            logger.LogWarning("Turma não encontrado. Id: {id}", id);
            throw new InvalidOperationException($"Turma com Id {id} não encontrada.");
        }
        return ClassMapper.ToResponse(entity);
    }

    public async Task<ClassResponse> CreateAsync(ClassRequest request, CancellationToken ct)
    {
        logger.LogInformation("Criando turma");

        var entity = ClassMapper.ToEntity(request);
        var subjectIds = request.SubjectIds.Distinct().ToHashSet();

        var allSubjects = await subjectRepository.GetAllAsync(ct);
        var subjects = allSubjects.Where(s => subjectIds.Contains(s.Id)).ToList();

        if (subjects.Count != subjectIds.Count)
        {
            var foundIds = subjects.Select(s => s.Id).ToHashSet();
            var missingIds = subjectIds.Except(foundIds);

            throw new KeyNotFoundException(
                $"Disciplinas não encontradas: {string.Join(", ", missingIds)}");
        }

        entity.Subjects = subjects;
        var created = await repository.AddAsync(entity, ct);

        logger.LogInformation("Turma criada com sucesso.");

        return ClassMapper.ToResponse(created);
    }

    public async Task<bool> UpdateAsync(int id, UpdateClassRequest request, CancellationToken ct)
    {
        logger.LogInformation("Atualizando turma. Id: {Id}", id);

        var entity = await repository.GetByIdAsync(id, ct);

        if (entity is null)
        {
            logger.LogWarning("Turma não encontrada. Id: {Id}", id);
            throw new InvalidOperationException(
                $"Turma com Id {id} não encontrada.");
        }

        ClassMapper.UpdateEntity(request, entity);

        if (request.SubjectIds is not null)
        {
            var subjectIds = request.SubjectIds.Distinct().ToHashSet();
            var allSubjects = await subjectRepository.GetAllAsync(ct);
            var subjects = allSubjects.Where(s => subjectIds.Contains(s.Id)).ToList();

            if (subjects.Count != subjectIds.Count)
            {
                var foundIds = subjects.Select(s => s.Id).ToHashSet();
                var missingIds = subjectIds.Except(foundIds);

                throw new KeyNotFoundException($"Disciplinas não encontradas: {string.Join(", ", missingIds)}");
            }

            entity.Subjects.Clear();

            foreach (var subject in subjects)
                entity.Subjects.Add(subject);
        }

        var updated = await repository.UpdateAsync(entity, ct);

        if (updated)
            logger.LogInformation("Turma atualizada com sucesso. Id: {Id}", id);

        return updated;
    }

    public async Task<bool> ToggleActive(int id, CancellationToken ct)
    {
        logger.LogInformation("Alterando status da turma.");

        var updated = await repository.ToggleActive(id, ct);

        if (!updated)
        {
            logger.LogWarning("Turma não encontrada. Id: {Id}", id);
            throw new KeyNotFoundException($"Turma com Id {id} não encontrada.");
        }

        logger.LogInformation("Status da turma alterado. Id: {Id}", id);

        return true;
    }
}