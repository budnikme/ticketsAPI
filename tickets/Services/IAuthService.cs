using tickets.Dto;

namespace tickets.Services;

public interface IAuthService
{
    Task<string?> Register(UserDto userDto);
    Task<string?> Login(LoginDto loginDto);
}