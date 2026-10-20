using Application.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Json;
using System.Text.Json;

namespace Infra.Services;

public class GeminiService(
    HttpClient httpClient,
    IConfiguration configuration) : IGeminiService
{
    public async Task<string> GerarSugestoesAsync(
        string planejamento,
        CancellationToken cancellationToken = default)
    {
        var apiKey = configuration["Gemini:ApiKey"];

        if (string.IsNullOrWhiteSpace(apiKey))
            throw new InvalidOperationException(
                "A chave da API do Gemini não foi configurada.");

        using var request = new HttpRequestMessage(
            HttpMethod.Post,
            "https://generativelanguage.googleapis.com/v1beta/models/gemini-3.5-flash-lite:generateContent");

        request.Headers.Add("x-goog-api-key", apiKey);

        request.Content = JsonContent.Create(new
        {
            contents = new[]
            {
                new
                {
                    parts = new[]
                    {
                        new
                        {
                            text = $"Você é um assistente pedagógico. " +
                                   $"Sugira atividades e exercícios para este planejamento: {planejamento}"
                        }
                    }
                }
            }
        });

        using var response = await httpClient.SendAsync(request, cancellationToken);

        var responseBody = await response.Content.ReadAsStringAsync(cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException($"Erro na API Gemini ({(int)response.StatusCode}): {responseBody}");
        }

        using var json = JsonDocument.Parse(responseBody);
        return json.RootElement
            .GetProperty("candidates")[0]
            .GetProperty("content")
            .GetProperty("parts")[0]
            .GetProperty("text")
            .GetString() ?? string.Empty;
    }
}