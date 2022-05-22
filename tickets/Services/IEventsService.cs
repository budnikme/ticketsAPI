using tickets.Dto;
using tickets.Models;


namespace tickets.Services;

public interface IEventsService
{
    Task<ServiceResponse<List<EventDto>>> Show();




}