namespace Application.DTOs;

public record ClassSummaryResponse(int Id,int TotalStudents);

public record SubjectSummaryResponse(int Id, string Name, string Description, int TotalClasses);

public record SubjectResponse(int Id, string Name, string Description, string Content, List<ClassSummaryResponse> Classes);

public record SaveSubjectRequest(string Name, string Description, string Content);