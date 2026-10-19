namespace Domain.Entities;

public class JournalEntry : BaseEntity
{
    public int SchoolClassId { get; set; }
    public Class Class { get; set; }
    public int? PlanningId { get; set; }          
    public Planning? Planning { get; set; }
    public string Content { get; set; } = string.Empty;
    public string? Observations { get; set; }
    public DateTime Date { get; set; }
}
