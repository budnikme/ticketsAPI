using Microsoft.EntityFrameworkCore;
using tickets.Dto;
using tickets.Models;

namespace tickets.Services;

public class TicketsService : ITicketsService
{
    private readonly TicketsContext _context;

    public TicketsService(TicketsContext context)
    {
        _context=context;
    }
    public async Task<ServiceResponse<List<TicketTypeDto>>> GetTicketTypes(int eventId)
    {
        ServiceResponse<List<TicketTypeDto>> response = new ServiceResponse<List<TicketTypeDto>>();
        try
        {
            //query to db
            var query = await (from t in _context.TicketTypes where t.EventId == eventId
                select new TicketTypeDto
                {
                    Tittle = t.Tittle,
                    Description = t.Description,
                    count = t.Count,
                    price = t.Price,
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
}