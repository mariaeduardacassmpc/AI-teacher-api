using Application.Dtos.Material;
using Application.Mappers;
using Domain.Entities;
using Domain.Interfaces;
using Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repository;

public class MaterialRepository(
    AITeacherDbContext context
) : IMaterialrepository
{
    private IQueryable<Material> GetBaseQuery()
    {
        return context.Materials
            .AsNoTracking()
            .Include(m => m.Subject)
            .Include(m => m.Classes)
            .AsSplitQuery();
    }

    public async Task<Material?> GetByIdAsync(int id, CancellationToken ct)
    {
        return await GetBaseQuery().FirstOrDefaultAsync(s => s.Id == id, ct);
    }

    public async Task<IReadOnlyList<Material>> GetAllAsync(CancellationToken ct)
    {
        return await GetBaseQuery().OrderBy(s => s.Title).ToListAsync(ct);
    }

    public async Task<Material> AddAsync(Material entity, CancellationToken ct)
    {
        await context.Materials.AddAsync(entity, ct);
        await context.SaveChangesAsync(ct);

        return await GetBaseQuery()
            .FirstAsync(m => m.Id == entity.Id, ct);
    }

    public async Task<bool> UpdateAsync(Material entity, CancellationToken ct)
    {
        var existing = await context.Materials
            .Include(m => m.Classes)
            .FirstOrDefaultAsync(m => m.Id == entity.Id, ct);

        if (existing is null)
            return false;

        MaterialMapper.UpdateEntity(
            new MaterialRequest(
                entity.Type,
                entity.Title,
                entity.FilePath,
                entity.SubjectId,
                entity.Classes.Select(c => c.Id).ToList(),
                entity.Topic
            ),
            existing
        );

        existing.Classes.Clear();

        foreach (var turma in entity.Classes)
            existing.Classes.Add(turma);

        await context.SaveChangesAsync(ct);
        return true;
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken ct)
    {
        var existing = await context.Materials.FirstOrDefaultAsync(c => c.Id == id, ct);

        if (existing is null)
            return false;

        context.Materials.Remove(existing);
        await context.SaveChangesAsync(ct);
        return true;
    }

    public async Task<IReadOnlyList<Material>> GetByClassAndSubjectNameAsync(string? className, string? subjectName, CancellationToken ct)
    {
        var query = GetBaseQuery();

        if (!string.IsNullOrWhiteSpace(className))
            query = query.Where(m => m.Classes.Any(c => c.Name.Contains(className)));

        if (!string.IsNullOrWhiteSpace(subjectName))
            query = query.Where(m => m.Subject.Name.Contains(subjectName));

        return await query.ToListAsync(ct);
    }
}
