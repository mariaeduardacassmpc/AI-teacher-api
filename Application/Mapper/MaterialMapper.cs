using Application.Dtos.Material;
using Domain.Entities;

namespace Application.Mappers;

public static class MaterialMapper
{
    public static Material ToEntity(MaterialRequest request) => new()
    {
        Type = request.Type,
        Title = request.Title.Trim(),
        FilePath = request.FilePath.Trim(),
        SubjectId = request.SubjectId,
        Topic = request.Topic.Trim()
    };

    public static MaterialResponse ToResponse(Material entity) => new(
        entity.Id,
        entity.Type,
        entity.Classes.Select(c => c.Id).ToList(),
        entity.Title,
        entity.FilePath,
        entity.SubjectId,
        entity.Subject.Name,
        entity.Topic
    );

    public static void UpdateEntity(
        MaterialRequest request,
        Material entity)
    {
        entity.Type = request.Type;
        entity.Title = request.Title.Trim();
        entity.FilePath = request.FilePath.Trim();
        entity.SubjectId = request.SubjectId;
        entity.Topic = request.Topic.Trim();
    }
}
