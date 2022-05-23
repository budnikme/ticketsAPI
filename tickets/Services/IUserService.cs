using tickets.Dto;
using tickets.Models;

namespace tickets.Services;

public interface IUserService
{
    Task<ServiceResponse<UserViewDto>> GetMe(int userId);
}