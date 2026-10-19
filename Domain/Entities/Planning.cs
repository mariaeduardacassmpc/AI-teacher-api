namespace Domain.Entities;

public class Planning : BaseEntity
{
    public string Content { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public TimeOnly StartTime { get; set; }
    public int DurationMinutes { get; set; }
    public string Objective { get; set; } = string.Empty;
    public List<PlanningActivity> Activities { get; set; } = [];
    public List<Material> Materials { get; set; } = [];
    public int SubjectId { get; set; }
    public PlanningStatus Status { get; set; } = PlanningStatus.Draft;
}

public class PlanningActivity
{
    public int Id { get; set; }
    public int PlanningId { get; set; }
    public int Order { get; set; }
    public string Description { get; set; } = string.Empty;
}