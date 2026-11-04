using Application.DTOs.User;

namespace Application.Interfaces;

public interface IUserService
{
    Task<UserDto> CreateUser(CreateUserDto dto, CancellationToken cancellationToken);
    Task<IEnumerable<UserDto>> GetAllUsers(CancellationToken cancellationToken);
    Task<UserDto> GetById(int id, CancellationToken cancellationToken);
    Task<UserDto> UpdateUser(int id, InputUserDto dto, CancellationToken cancellationToken);
    Task DeleteUser(int id, CancellationToken cancellationToken);
    Task ChangePassword(int id, ChangePasswordDto dto, CancellationToken ct);
}