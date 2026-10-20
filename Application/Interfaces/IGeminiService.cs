namespace Application.Interfaces;

public interface IGeminiService
{
    Task<string> GerarSugestoesAsync(string planejamento, CancellationToken cancellationToken = default);
}