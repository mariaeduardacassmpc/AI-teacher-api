using Application.DTOs.User;
using Application.Interfaces;
using Application.Mappers;
using Application.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

public class UserService(
    IUserRepository userRepository,
    IPasswordHasher<User> passwordHasher,
    ILogger<UserService> logger) : IUserService
{
    public async Task<UserDto> CreateUser(CreateUserDto dto, CancellationToken ct)
    {
        logger.LogInformation("Criando usuário com e-mail: {Email}", dto.Email);

        var emailExists = await userRepository.EmailExistsAsync(dto.Email, null, ct);

        if (emailExists)
            throw new InvalidOperationException("Já existe um cadastro com esse e-mail.");

        PasswordValidator.Validate(dto.Password);

        var user = dto.ToEntity();

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

        logger.LogInformation("Foram encontrados {Count} usuários", users.Count());

        return users.Select(u => u.ToDto());
    }

    public async Task<UserDto> GetById(int id, CancellationToken ct)
    {
        logger.LogInformation("Buscando usuário por Id: {UserId}", id);

        var user = await userRepository.GetByIdAsync(id, ct);

        if (user == null)
        {
            logger.LogWarning("Usuário não encontrado. Id: {UserId}", id);
            throw new InvalidOperationException($"Usuário com Id {id} não encontrado.");
        }

        return user.ToDto();
    }

    public async Task<UserDto> UpdateUser(int id, InputUserDto dto, CancellationToken ct)
    {
        logger.LogInformation("Atualizando usuário. Id: {UserId}", id);

        var existingUser = await userRepository.GetByIdAsync(id, ct);

        if (existingUser == null)
        {
            logger.LogWarning("Usuário não encontrado para atualização. Id: {UserId}", id);
            throw new InvalidOperationException($"Usuário com Id {id} não encontrado.");
        }

        var emailExists = await userRepository.EmailExistsAsync(dto.Email, id, ct);

        if (emailExists)
            throw new InvalidOperationException("Já existe um cadastro com esse e-mail.");

        existingUser.Email = dto.Email;
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
            throw new InvalidOperationException($"Usuário com Id {id} não encontrado.");
        }

        await userRepository.DeleteAsync(user, ct);

        logger.LogInformation("Usuário excluído com sucesso. Id: {UserId}", id);
    }

    public async Task ChangePassword(int id, ChangePasswordDto dto, CancellationToken ct)
    {
        logger.LogInformation("Alterando senha do usuário. Id: {UserId}", id);

        var existingUser = await userRepository.GetByIdAsync(id, ct);

        if (existingUser == null)
            throw new InvalidOperationException($"Usuário com Id {id} não encontrado.");

        var passwordResult = passwordHasher.VerifyHashedPassword(
            existingUser,
            existingUser.PasswordHash,
            dto.CurrentPassword
        );

        if (passwordResult == PasswordVerificationResult.Failed)
            throw new InvalidOperationException("A senha atual está incorreta.");

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