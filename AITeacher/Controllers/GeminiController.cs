using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AITeacher.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class GeminiController(IGeminiService geminiService) : ControllerBase
{
    [HttpPost("sugestoes")]
    public async Task<IActionResult> GerarSugestoes([FromBody] GeminiRequestDto request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Planejamento))
            return BadRequest(new { mensagem = "Informe o planejamento da aula." });

        var sugestoes = await geminiService.GerarSugestoesAsync(request.Planejamento, cancellationToken);

        return Ok(new { sugestoes });
    }
}