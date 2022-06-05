using Microsoft.EntityFrameworkCore;
using tickets.Dto;
using tickets.Models;

namespace tickets.Services;

public class UserService : IUserService
{
    private readonly TicketsContext _context;

    public UserService(TicketsContext context)
    {
        _context = context;
    }
    public async Task<ServiceResponse<UserViewDto>> GetMe(int userId)
    {
        ServiceResponse<UserViewDto> response = new ServiceResponse<UserViewDto>();
        try
        {
            //query to db
            var query = await (from u in _context.Users
                where u.Id == userId
                select
                    new UserViewDto
                    {
                        Name = u.Name,
                        LastName = u.LastName,
                        Email = u.Email,
                        PhoneNumber = u.PhoneNumber
                    }).FirstAsync();
            response.Data = query;
        }
        catch (Exception e)
        {
            response.Success = false;
            response.Message = e.Message;
        }

        return response;
    }
}