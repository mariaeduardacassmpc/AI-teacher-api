namespace Domain.Entities;

public class QuestionBank : BaseEntity
{
    public int SubjectId { get; set; }
    public string Topic { get; set; }
    public Difficulty Difficulty { get; set; }
    public TypeQuestion Question { get; set; }
    public string? AnswerKey { get; set; }
    public int UsageCount { get; set; }
}
