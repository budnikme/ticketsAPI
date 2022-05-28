using Microsoft.EntityFrameworkCore;
using tickets.Dto;
using tickets.Models;

namespace tickets.Services;

public class GenresService : IGenresService
{
    private readonly TicketsContext _context;

    public GenresService(TicketsContext context)
    {
        _context=context;
    }
    public async Task<ServiceResponse<List<GenresDto>>> GetGenres()
    {
        ServiceResponse<List<GenresDto>> response = new ServiceResponse<List<GenresDto>>();
        try
        {
            var query = await (from a in _context.Genres
                select new GenresDto
                {
                    Id = a.Id,
                    Genre = a.Genre1
                }).ToListAsync();
            response.Data = query;
        }
        catch (Exception e)
        {
            response.Success = false;
            response.Message = e.Message;
        }
        return response;
    }

    public async Task<ServiceResponse<GenresDto>> GetGenresById(int id)
    {
        ServiceResponse<GenresDto> response = new ServiceResponse<GenresDto>();
        try
        {
            var query = await (from a in _context.Genres where a.Id==id
                select new GenresDto
                {
                    Id = a.Id,
                    Genre = a.Genre1
                }).FirstOrDefaultAsync();
            if (query == null)
            {
                response.Success = false;
                response.Message = "Not Found";
            }
            else
            {
                response.Data = query;
            }
            
        }
        catch (Exception e)
        {
            response.Success = false;
            response.Message = e.Message;
        }
        return response;
    }
}