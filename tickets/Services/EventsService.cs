using Microsoft.EntityFrameworkCore;
using tickets.Dto;
using tickets.Models;


namespace tickets.Services;

public class EventsService: IEventsService
{
    private readonly TicketsContext _context;

    public EventsService(TicketsContext context)
    {
        _context = context;
    }
    
    public async Task<ServiceResponse<List<EventDto>>> Show()
    {
        ServiceResponse<List<EventDto>> response = new ServiceResponse<List<EventDto>>();
        try
        {
            var query = await (from e in _context.Events
                join tp in _context.TicketTypes on e.Id equals tp.EventId
                join p in _context.Places on e.PlaceId equals p.Id
                group tp by new {e.Id, e.Tittle, e.Type, p.City, Place = p.Tittle, e.Date, e.PreviewLink, e.PosterLink}
                into tPrice
                orderby tPrice.Key.Date
                select new EventDto
                {
                    Id = tPrice.Key.Id,
                    Tittle = tPrice.Key.Tittle,
                    Type = tPrice.Key.Type,
                    Price = tPrice.Min(t => t.Price),
                    City = tPrice.Key.City,
                    Place = tPrice.Key.Place,
                    Date = tPrice.Key.Date,
                    PreviewLink = tPrice.Key.PreviewLink,
                    PosterLink = tPrice.Key.PosterLink
                }).Take(10).ToListAsync();
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