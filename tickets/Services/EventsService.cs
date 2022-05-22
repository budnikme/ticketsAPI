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
            //query to db
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
                    Price = tPrice.Min(t => t.Price), //select only minimal price
                    City = tPrice.Key.City,
                    Place = tPrice.Key.Place,
                    Date = tPrice.Key.Date,
                    PreviewLink = tPrice.Key.PreviewLink,
                    PosterLink = tPrice.Key.PosterLink
                }).ToListAsync();
            response.Data = query;
        }
        catch (Exception e) //if error send error message to client
        {
            response.Success = false;
            response.Message = e.Message;
        }
        


        return response;
    }

    public async Task<ServiceResponse<FullEventDto>> ShowFull(int id)
    {
        ServiceResponse<FullEventDto> response = new ServiceResponse<FullEventDto>();
        try
        {
            //db query
            var query = await (from e in _context.Events.
                    Include(a => a.Artists).
                    Include(g => g.Genres) where e.Id == id 
                join tp in _context.TicketTypes on e.Id equals tp.EventId 
                join p in _context.Places on e.PlaceId equals p.Id
                select new FullEventDto
                {
                    Id=e.Id,
                    Tittle = e.Tittle,
                    Type=e.Type,
                    Price=tp.Price,
                    City=p.City,
                    Address=p.Address,
                    Place=p.Tittle,
                    Date=e.Date,
                    Description=e.Description,
                    PosterLink=e.PosterLink,
                    PreviewLink=e.PreviewLink,
                    //convert from Genres and Artists list to string list
                    Genres = e.Genres.ToList().Select(g => g.Genre1).ToList(),
                    Artists = e.Artists.ToList().Select(a => a.Name).ToList()
                }).FirstAsync();

            
            response.Data = query; 
        }
        catch (Exception e) //if error send error message to client
        {
            response.Success = false;
            if (e.Message == "Sequence contains no elements.") //if there is no event in db send Not Found error to client
            {
                response.Message = "Not Found Error";
            }
            else
            {
                response.Message = e.Message;
            }
            
            
        }
        
        
        
        return response;
    }
}