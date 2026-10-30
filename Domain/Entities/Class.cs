namespace Domain.Entities;

public class Class : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public int TotalStudents { get; set; } 
    public string Schedule { get; set; } = string.Empty;
    public List<Subject> Subjects { get; set; } = [];
    public bool IsActive { get; set; } = true;
    public ICollection<Material> Materials { get; set; } = new List<Material>();
}
