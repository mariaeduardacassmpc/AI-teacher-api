namespace Application.DTOs.User;

public class BaseUserDto
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}

public class UserDto : BaseUserDto
{
    public int UserId { get; set; }
}

public class CreateUserDto : BaseUserDto
{
    public string Password { get; set; } = string.Empty;
}

public class InputUserDto : BaseUserDto
{
}

public class ChangePasswordDto
{
    public string CurrentPassword { get; set; } = string.Empty;
    public string NewPassword { get; set; } = string.Empty;
}