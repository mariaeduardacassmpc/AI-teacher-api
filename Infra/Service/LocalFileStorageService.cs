using Application.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Infra.Services;

public class LocalFileStorageService(
    IWebHostEnvironment environment
) : IFileStorageService
{
    private static readonly HashSet<string> AllowedExtensions =
    [
        ".pdf", ".doc", ".docx", ".ppt", ".pptx",
        ".png", ".jpg", ".jpeg"
    ];

    public async Task<string> SaveAsync(
        IFormFile file,
        CancellationToken ct)
    {
        if (file is null || file.Length == 0)
            throw new ArgumentException("Selecione um arquivo válido.");

        if (file.Length > 20 * 1024 * 1024)
            throw new ArgumentException("O arquivo não pode exceder 20 MB.");

        var extension = Path.GetExtension(file.FileName).ToLowerInvariant();

        if (!AllowedExtensions.Contains(extension))
            throw new ArgumentException("Formato de arquivo não permitido.");

        var webRoot = environment.WebRootPath
            ?? Path.Combine(environment.ContentRootPath, "wwwroot");

        var uploadDirectory = Path.Combine(
            webRoot, "uploads", "materials");

        Directory.CreateDirectory(uploadDirectory);

        var fileName = $"{Guid.NewGuid():N}{extension}";
        var filePath = Path.Combine(uploadDirectory, fileName);

        await using var stream = new FileStream(filePath, FileMode.Create);

        await file.CopyToAsync(stream, ct);

        return $"/uploads/materials/{fileName}";
    }
}