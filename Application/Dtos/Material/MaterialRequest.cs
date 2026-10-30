
using Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Application.Dtos.Material;

public record MaterialRequest(
    MaterialType Type,
    string Title,
    string FilePath,
    int SubjectId,
    List<int> ClassIds,
    string Topic
);

public record MaterialResponse(
    int Id,
    MaterialType Type,
    List<int> ClassIds,
    string Title,
    string FilePath,
    int SubjectId,
    string SubjectName,
    string Topic
);

public record MaterialSummaryResponse(
    int Id,
    string Title,
    MaterialType Type,
    string Topic);

public class MaterialUploadRequest
{
    public MaterialType Type { get; set; }
    public string Title { get; set; } = string.Empty;
    public IFormFile? File { get; set; }
    public string? Link { get; set; }
    public string? Content { get; set; }
    public int SubjectId { get; set; }
    public List<int> ClassIds { get; set; } = [];
    public string Topic { get; set; } = string.Empty;
}
