using Microsoft.EntityFrameworkCore;
using tickets.Dto;
using tickets.Models;

namespace tickets.Services;

public class ArtistsService : IArtistsService
{
    private readonly TicketsContext _context;

    public ArtistsService(TicketsContext context)
    {
        _context=context;
    }
    
    public async Task<ServiceResponse<List<ArtistDto>>> GetArtists()
    {
        ServiceResponse<List<ArtistDto>> response = new ServiceResponse<List<ArtistDto>>();
        try
        {
            var query = await (from a in _context.Artists
                select new ArtistDto
                {
                    Id = a.Id,
                    Name = a.Name,
                    Description = a.Description
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

    public async Task<ServiceResponse<ArtistDto>> GetArtistById(int id)
    {
        ServiceResponse<ArtistDto> response = new ServiceResponse<ArtistDto>();
        try
        {
            var query = await (from a in _context.Artists where a.Id==id
                select new ArtistDto
                {
                    Id = a.Id,
                    Name = a.Name,
                    Description = a.Description
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