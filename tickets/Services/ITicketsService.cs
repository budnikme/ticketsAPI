using tickets.Dto;
using tickets.Models;

namespace tickets.Services;

public interface ITicketsService
{
    Task<ServiceResponse<List<TicketTypeDto>>> GetTicketTypes(int eventId);
}