namespace Domain.Entities;

public class Subject : BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Content { get; set; }
    public List<Class> Classes { get; set; }    
}
