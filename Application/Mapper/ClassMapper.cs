using Application.DTOs;
using Domain.Entities;

namespace Application.Mappers;

public static class ClassMapper
{
    public static Class ToEntity(ClassRequest request) => new()
    {
        Name = request.Name.Trim(),
        TotalStudents = request.TotalStudents,
        Schedule = request.Schedule
    };

    public static ClassResponse ToResponse(Class entity) => new(
        entity.Id,
        entity.Name,
        entity.TotalStudents,
        entity.Schedule,
        entity.Subjects.Select(s => s.Id).ToList(),
        entity.IsActive
    );

    public static void UpdateEntity(UpdateClassRequest request, Class entity)
    {
        if (request.Name is not null)
            entity.Name = request.Name.Trim();

        if (request.TotalStudents.HasValue)
            entity.TotalStudents = request.TotalStudents.Value;

        if (request.Schedule is not null)
            entity.Schedule = request.Schedule;
    }
}