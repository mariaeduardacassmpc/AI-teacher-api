using Application.DTOs.User;
using Application.Interfaces;
using Application.Mappers;
using Application.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Application.Exceptions;

public class UserService(
    IUserRepository userRepository,
    IPasswordHasher<User> passwordHasher,
    ILogger<UserService> logger) : IUserService
{
    public async Task<UserDto> CreateUser(CreateUserDto dto, CancellationToken ct)
    {
        logger.LogInformation("Criando usuário com e-mail: {Email}", dto.Email);
        var normalizedEmail = dto.Email.Trim().ToLowerInvariant();

        var emailExists = await userRepository.EmailExistsAsync(dto.Email, null, ct);

        if (emailExists)
            throw new ConflictException("Já existe um cadastro com esse e-mail.");

        PasswordValidator.Validate(dto.Password);

        var user = dto.ToEntity();

        user.Email = normalizedEmail;
        user.PasswordHash = passwordHasher.HashPassword(user, dto.Password);

        await userRepository.AddAsync(user, ct);
        await userRepository.SaveChangesAsync(ct);

        logger.LogInformation("Usuário criado com sucesso. Id: {UserId}", user.UserId);

        return user.ToDto();
    }

    public async Task<IEnumerable<UserDto>> GetAllUsers(CancellationToken ct)
    {
        logger.LogInformation("Buscando todos os usuários");

        var users = await userRepository.GetAllAsync(ct);
        var usersDto = users.Select(user => user.ToDto()).ToList();

        logger.LogInformation("Foram encontrados {Count} usuários", usersDto.Count());

        return usersDto;
    }

    public async Task<UserDto> GetById(int id, CancellationToken ct)
    {
        logger.LogInformation("Buscando usuário por Id: {UserId}", id);

        var user = await userRepository.GetByIdAsync(id, ct);

        if (user is null)
        {
            logger.LogWarning("Usuário não encontrado. Id: {UserId}", id);
            throw new NotFoundException($"Usuário com Id {id} não encontrado.");
        }

        return user.ToDto();
    }

    public async Task<UserDto> UpdateUser(int id, InputUserDto dto, CancellationToken ct)
    {
        logger.LogInformation("Atualizando usuário. Id: {UserId}", id);

        var existingUser = await userRepository.GetByIdAsync(id, ct);

        if (existingUser is null)
        {
            logger.LogWarning("Usuário não encontrado para atualização. Id: {UserId}", id);
            throw new NotFoundException($"Usuário com Id {id} não encontrado.");
        }

        var normalizedEmail = dto.Email.Trim().ToLowerInvariant();
        var emailExists = await userRepository.EmailExistsAsync(normalizedEmail, id, ct);

        if (emailExists)
            throw new InvalidOperationException("Já existe um cadastro com esse e-mail.");

        existingUser.Email = normalizedEmail;
        existingUser.Name = dto.Name;
        
        await userRepository.UpdateAsync(existingUser, ct);

        logger.LogInformation("Usuário atualizado com sucesso. Id: {UserId}", id);

        return existingUser.ToDto();
    }

    public async Task DeleteUser(int id, CancellationToken ct)
    {
        logger.LogInformation("Excluindo usuário. Id: {UserId}", id);

        var user = await userRepository.GetByIdAsync(id, ct);

        if (user == null)
        {
            logger.LogWarning("Usuário não encontrado para exclusão. Id: {UserId}", id);
            throw new NotFoundException($"Usuário com Id {id} não encontrado.");
        }

        await userRepository.DeleteAsync(user, ct);

        logger.LogInformation("Usuário excluído com sucesso. Id: {UserId}", id);
    }

    public async Task ChangePassword(int id, ChangePasswordDto dto, CancellationToken ct)
    {
        logger.LogInformation("Alterando senha do usuário. Id: {UserId}", id);

        var existingUser = await userRepository.GetByIdAsync(id, ct);

        if (existingUser == null)
            throw new NotFoundException($"Usuário com Id {id} não encontrado.");

        var passwordResult = passwordHasher.VerifyHashedPassword(
            existingUser,
            existingUser.PasswordHash,
            dto.CurrentPassword
        );

        if (passwordResult == PasswordVerificationResult.Failed)
            throw new BusinessException("A senha atual está incorreta.");

        if (dto.CurrentPassword == dto.NewPassword)
        {
            throw new BusinessException(
                "A nova senha deve ser diferente da atual."
            );
        }

        PasswordValidator.Validate(dto.NewPassword);

        existingUser.PasswordHash = passwordHasher.HashPassword(
            existingUser,
            dto.NewPassword
        );

        await userRepository.UpdateAsync(existingUser, ct);
        await userRepository.SaveChangesAsync(ct);

        logger.LogInformation("Senha alterada com sucesso. Id: {UserId}", id);
    }
}