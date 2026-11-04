using Application.DTOs.User;
using Domain.Entities;

namespace Application.Mappers;

public static class UserMapper
{
    public static UserDto ToDto(this User user)
    {
        return new UserDto
        {
            UserId = user.UserId,
            Email = user.Email,
            Name = user.Name
        };
    }
    public static User ToEntity(this CreateUserDto dto)
    {
        return new User
        {
            Name = dto.Name,
            Email = dto.Email,
            PasswordHash = string.Empty
        };
    }
}