namespace Application.DTOs;

public class ClassRequest
{
    public string Name { get; set; } = string.Empty;
    public int TotalStudents { get; set; }
    public List<int> SubjectIds { get; set; } = [];
    public string Schedule { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
}

public record ClassResponse(int Id, string Name, int TotalStudents, string Schedule, List<int> SubjectIds, bool IsActive);

public class UpdateClassRequest
{
    public string? Name { get; set; }
    public int? TotalStudents { get; set; }
    public string? Schedule { get; set; }
    public List<int>? SubjectIds { get; set; }
}
