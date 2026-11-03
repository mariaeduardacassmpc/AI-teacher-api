using Domain.Entities;

namespace Application.Dtos.Journal;

public class JournalRequest
{
    public int SchoolClassId { get; set; }
    public Class Class { get; set; }
    public int? PlanningId { get; set; }
    public Planning? Planning { get; set; }
    public string Content { get; set; } = string.Empty;
    public string? Observations { get; set; }
    public DateTime Date { get; set; }
}
