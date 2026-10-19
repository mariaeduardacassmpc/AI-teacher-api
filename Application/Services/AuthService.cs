using Application.Dtos.Auth;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Application.Services;

public class AuthService(
    IUserRepository repository,
    IPasswordHasher<User> passwordHasher,
    ITokenService tokenService,
    ILogger<AuthService> logger
) : IAuthService
{
    public async Task<AuthResponseDto> Login(LoginDto dto, CancellationToken cancellation)
    {
        logger.LogInformation("Tentativa de login para o e-mail: {Email}", dto.Email);

        var user = await repository.GetByEmailAsync(dto.Email, cancellation);

        if (user == null)
        {
            logger.LogWarning("Login falhou para o e-mail: {Email}", dto.Email);

            throw new InvalidOperationException("E-mail ou senha inválidos.");
        }

        var result = passwordHasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);

        if (result == PasswordVerificationResult.Failed)
        {
            logger.LogWarning("Senha inválida para o e-mail: {Email}", dto.Email);
            throw new InvalidOperationException("E-mail ou senha inválidos.");
        }

        var (token, expiresAt) = tokenService.GenerateToken(user);

        logger.LogInformation("Login realizado com sucesso. UserId: {UserId}", user.UserId);

        return new AuthResponseDto
        {
            Token = token,
            ExpiresAt = expiresAt,
            User = new UserResponseDto
            {
                Id = user.UserId,
                Email = user.Email
            }
        };
    }
   
    public async Task Register(RegisterDto dto, CancellationToken cancellation)
    {
        var existingUser = await repository.GetByEmailAsync(dto.Email, cancellation);

        if (existingUser != null)
            throw new InvalidOperationException("Este e-mail já está cadastrado.");

        var user = new User
        {
            Email = dto.Email,
            PasswordHash = string.Empty,
            Name = dto.Name 
        };

        user.PasswordHash = passwordHasher.HashPassword(user, dto.Password);

        await repository.AddAsync(user, cancellation);

        logger.LogInformation("Usuário registrado com sucesso. E-mail: {Email}", dto.Email);
    }
}
