namespace Application.DTOs;

public class ClassScheduleRequest
{   
    public int Id { get; set; }
    public DayOfWeek DayOfWeek { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
}

public class SaveClassRequest
{
    public string Name { get; set; } = string.Empty;
    public int TotalStudents { get; set; }
    public DateOnly Hour { get; set; }
    public List<ClassScheduleRequest> Schedules { get; set; } = [];
}

public record ClassScheduleResponse(int Id,DayOfWeek DayOfWeek, TimeOnly StartTime, TimeOnly EndTime);

public record ClassResponse(int Id, string Name, int TotalStudents, DateOnly Hour, List<ClassScheduleResponse> Schedules);