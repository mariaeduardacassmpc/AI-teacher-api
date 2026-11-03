using Application.Dtos.Journal;
using Domain.Entities;

namespace Application.Mappers;

public static class JournalMapper
{
    public static JournalEntry ToEntity(JournalRequest request)
    {
        return new JournalEntry
        {
            SchoolClassId = request.SchoolClassId,
            PlanningId = request.PlanningId,
            Content = request.Content,
            Observations = request.Observations,
            Date = request.Date
        };
    }

    public static void UpdateEntity(JournalEntry entity, JournalRequest request)
    {
        entity.SchoolClassId = request.SchoolClassId;
        entity.PlanningId = request.PlanningId;
        entity.Content = request.Content;
        entity.Observations = request.Observations;
        entity.Date = request.Date;
    }
}