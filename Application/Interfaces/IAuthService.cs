using Application.Dtos.Auth;

namespace Application.Interfaces;

public interface IAuthService
{
    Task Register(RegisterDto dto, CancellationToken cancellationToken);
}