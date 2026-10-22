using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services;

public class ClassService(
    IClassRepository repository
) : IClassService
{
    public async Task<IReadOnlyList<ClassResponse>> GetAllAsync(CancellationToken ct)
    {
        var classes = await repository.GetAllAsync(ct);
        return classes.Select(ToResponse).ToList();
    }

    public async Task<ClassResponse?> GetByIdAsync(int id, CancellationToken ct)
    {
        var entity = await repository.GetByIdAsync(id, ct);
        return entity is null ? null : ToResponse(entity);
    }

    public async Task<ClassResponse> CreateAsync(SaveClassRequest request, CancellationToken ct)
    {
        var entity = ToEntity(request);
        var created = await repository.AddAsync(entity, ct);
        return ToResponse(created);
    }

    public Task<bool> UpdateAsync(int id, SaveClassRequest request, CancellationToken ct)
    {
        var entity = ToEntity(request);
        entity.Id = id;
        return repository.UpdateAsync(entity, ct);
    }

    public Task<bool> DeleteAsync(int id, CancellationToken ct)
    {
        return repository.DeleteAsync(id, ct);
    }

    private static Class ToEntity(SaveClassRequest request) => new()
    {
        Name = request.Name.Trim(),
        TotalStudents = request.TotalStudents,
        Hour = request.Hour,
        Schedules = request.Schedules
            .Select(s => new ClassSchedule
            {
                Id = s.Id,
                DayOfWeek = s.DayOfWeek,
                StartTime = s.StartTime,
                EndTime = s.EndTime
            })
            .ToList()
    };

    private static ClassResponse ToResponse(Class entity) => new(
        entity.Id,
        entity.Name,
        entity.TotalStudents,
        entity.Hour,
        entity.Schedules
            .OrderBy(s => s.DayOfWeek)
            .ThenBy(s => s.StartTime)
            .Select(s => new ClassScheduleResponse(s.Id, s.DayOfWeek, s.StartTime, s.EndTime))
            .ToList());
}