using Application.Dtos.Material;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AITeacher.Controllers;

[ApiController]
//[Authorize]
[Route("api/[controller]")]
public class MaterialsController(
    IMaterialService service
) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken ct)
    {
        var classes = await service.GetAllAsync(ct);
        return Ok(classes);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id, CancellationToken ct)
    {
        var result = await service.GetByIdAsync(id, ct);
        return result is null ? NotFound() : Ok(result);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        var deleted = await service.DeleteAsync(id, ct);
        return deleted ? NoContent() : NotFound();
    }

    [HttpGet("search")]
    public async Task<ActionResult<IReadOnlyList<MaterialResponse>>> Search([FromQuery] string? className, [FromQuery] string? subjectName, CancellationToken ct)
    {
        var materials = await service.GetByClassAndSubjectNameAsync(
            className,
            subjectName,
            ct
        );

        return Ok(materials);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id,[FromBody] MaterialUploadRequest request, CancellationToken ct)
    {
        var updated = await service.UpdateAsync(id, request, ct);

        return updated is not null ? Ok(updated) : NotFound();
    }

    [HttpPost("upload")]
    [Consumes("multipart/form-data")]
    public async Task<ActionResult<MaterialResponse>> Create([FromForm] MaterialUploadRequest request, CancellationToken ct)
    {
        var material = await service.CreateAsync(request, ct);

        return Ok(material);
    }
}
