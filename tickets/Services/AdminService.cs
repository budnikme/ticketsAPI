using System.Collections.ObjectModel;
using Microsoft.EntityFrameworkCore;
using tickets.Dto;
using tickets.Models.Entities;

namespace tickets.Services;

public class AdminService : IAdminService
{   
    private readonly TicketsContext _context;

    public AdminService(TicketsContext context)
    {
        _context = context;
    }
    
    public async Task<bool> AddEvent(AdminEventDto e)
    {
        Event ev = new Event
        {
            Type = e.Type,
            PlaceId = e.PlaceId,
            Tittle = e.Tittle,
            Date = e.Date,
            Description = e.Description,
            PreviewLink = e.PreviewLink,
            PosterLink = e.PosterLink,

        };
        //getting artists and genres from db and after that adding them to events
        //TODO it would be better to do it in another way to not send multiple requests to db
        foreach(int id in e.ArtistsId)
        {
            var ar = await _context.Artists.FirstOrDefaultAsync(a => a.Id == id);
            ev.Artists.Add(ar);
        }
        foreach(int id in e.GenresId)
        {
            var gnr = await _context.Genres.FirstOrDefaultAsync(g => g.Id == id);
            ev.Genres.Add(gnr);
        }

        foreach (var ticketType in e.TicketTypes)
        {
            ev.TicketTypes.Add(new TicketType
            {
                Tittle = ticketType.Tittle,
                Description = ticketType.Description,
                Price = ticketType.Price,
                Count = ticketType.Count
                
            });
        }
        
        _context.Events.Add(ev);

        try
        {
            await _context.SaveChangesAsync();
            return true;

        }
        catch (Exception)
        {
            return false;
        }


    }

    public async Task<bool> DeleteEvent(int eventId)
    {
        try
        {
            var delEvent = await _context.Events.Include(a => a.Artists).Include(g => g.Genres).Include(t => t.TicketTypes).FirstAsync(ev => ev.Id == eventId);
            _context.Events.Remove(delEvent);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return false;
        }
        
    }

    public async Task<bool> ChangeEvent(int eventId, AdminEventDto e)
    {
        var ev = await _context.Events.Include(a => a.Artists).Include(g => g.Genres).Include(t => t.TicketTypes).FirstAsync(ev => ev.Id == eventId);
        ev.Type=e.Type;
        ev.PlaceId=e.PlaceId;
        ev.Tittle=e.Tittle;
        ev.Date = e.Date;
        ev.Description = e.Description;
        ev.PreviewLink = e.PreviewLink;
        ev.PosterLink = e.PosterLink;
        
        ev.Artists = new Collection<Artist>();
        
        foreach(int id in e.ArtistsId)
        {
            var ar = await _context.Artists.FirstOrDefaultAsync(a => a.Id == id);
            ev.Artists.Add(ar);
        
        }
        ev.Genres = new Collection<Genre>();
        foreach(int id in e.GenresId)
        {
            var gnr = await _context.Genres.FirstOrDefaultAsync(g => g.Id == id);
            ev.Genres.Add(gnr);
        }
        ev.TicketTypes = new Collection<TicketType>();
        foreach (var ticketType in e.TicketTypes)
        {
            ev.TicketTypes.Add(new TicketType
            {
                Tittle = ticketType.Tittle,
                Description = ticketType.Description,
                Price = ticketType.Price,
                Count = ticketType.Count
                
            });
        }
        try
        {
            await _context.SaveChangesAsync();
            return true;

        }
        catch (Exception)
        {
            return false;
        }

    }
}