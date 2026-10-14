namespace Domain.Entities;

public class Class : BaseEntity
{
    public string Name { get; set; }
    public int TotalStudents { get; set; }
    public DateOnly Hour { get; set; }
    public List<ClassSchedule> Schedules { get; set; } = [];
    public List<Subject> Subjects { get; set; } = [];
}

public class ClassSchedule
{
    public int Id { get; set; }
    public int SchoolClassId { get; set; }
    public DayOfWeek DayOfWeek { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
}