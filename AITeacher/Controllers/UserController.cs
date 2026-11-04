using Application.DTOs.User;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AITeacher.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class UserController(
    IUserService userService
) : ControllerBase
{
    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> CreateUser(CreateUserDto user, CancellationToken ct)
    {
        var created = await userService.CreateUser(user, ct);

        return CreatedAtAction(nameof(GetById), new { id = created.UserId }, userService);
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetAll(CancellationToken ct)
    {
        var users = await userService.GetAllUsers(ct);

        return Ok(users);
    }

    [HttpGet("{id}")]
    [Authorize]
    public async Task<IActionResult> GetById(int id, CancellationToken ct)
    {
        var user = await userService.GetById(id, ct);

        return user is null ? NotFound() : Ok(user);
    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<IActionResult> UpdateUser(int id, InputUserDto user, CancellationToken ct)
    {
        var updatedUser = await userService.UpdateUser(id, user, ct);

        return updatedUser != null ? NoContent() : NotFound();
    }

    [HttpPut("change-password")]
    [Authorize]
    public async Task<IActionResult> ChangePassword(ChangePasswordDto dto, CancellationToken ct)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        await userService.ChangePassword(userId, dto, ct);

        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> DeleteUser(int id, CancellationToken ct)
    {
        await userService.DeleteUser(id, ct);

        return NoContent();
    }
}