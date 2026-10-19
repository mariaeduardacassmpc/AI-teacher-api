//using Application.Dtos.Auth;
//using Application.Interfaces;
//using Application.Services;
//using Microsoft.AspNetCore.Mvc;

//namespace AITeacher.Controllers;

//[ApiController]
//[Route("api/[controller]")]
//public class ClassController(IClassService classService) : ControllerBase
//{
//    [HttpPost("register")]
//    public async Task<IActionResult> Register(RegisterDto dto, CancellationToken cancellation)
//    {
//        await classService.Register(dto, cancellation);

//        return Ok();
//    }
//}
