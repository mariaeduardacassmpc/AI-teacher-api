namespace Domain.Entities;

public class Material : BaseEntity
{
    public MaterialType Type { get; set; }
    public string Title { get; set; } = string.Empty;
    public string FilePath { get; set; } = string.Empty;
    public int SubjectId { get; set; } 
    public string Topic { get; set; } = string.Empty;
}
