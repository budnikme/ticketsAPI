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
                    Count = t.Count,
                    Price = t.Price,
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

    public async Task<ServiceResponse<List<TicketsDto>>> GetUserTickets(int userId)
    {
        ServiceResponse<List<TicketsDto>> response = new ServiceResponse<List<TicketsDto>>();
        try
        {
            //query to db
            var query = await (from t in _context.Tickets where t.UserId == userId
                join e in _context.Events on t.EventId equals e.Id
                join p in _context.Payments on t.PaymentId equals p.Id
                join tp in _context.TicketTypes on t.TicketTypeId equals tp.Id
                join pl in _context.Places on e.PlaceId equals pl.Id
                select new TicketsDto
                {
                    Id=t.Id,
                    EventId=e.Id,
                    EventTittle=e.Tittle,
                    EventCity=pl.City,
                    EventPlace=pl.Tittle,
                    EventAddress=pl.Address,
                    EventDate=e.Date,
                    PurchaseDate = p.Time,
                    IsPaid=p.Confirmed
                    
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