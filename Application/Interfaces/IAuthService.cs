using Application.Dtos.Auth;
using Application.Dtos.Password;

namespace Application.Interfaces;

public interface IAuthService
{
    Task <AuthResponseDto>Login (LoginDto dto, CancellationToken cancellationToken);
    Task Register(RegisterDto dto, CancellationToken cancellationToken);
}