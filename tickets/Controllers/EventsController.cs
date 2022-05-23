using Microsoft.AspNetCore.Mvc;
using tickets.Dto;
using tickets.Models;
using tickets.Services;


namespace tickets.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EventsController : ControllerBase
{
    private readonly IEventsService _eventsService;

    public EventsController(IEventsService eventsService)
    {
        _eventsService = eventsService;
    }

    [HttpGet]
    public async Task<ActionResult<ServiceResponse<List<EventDto>>>> GetEvents()
    {
        ServiceResponse<List<EventDto>> response = await _eventsService.Show();
        if (response.Success == false)
        {
            return NotFound(response);
        }
        return Ok(response);
    }
    
    [HttpGet("{id:int}")]
    public async Task<ActionResult<ServiceResponse<FullEventDto>>> GetFullEvent(int id)
    {
        ServiceResponse<FullEventDto> response = await _eventsService.ShowFull(id);
        if (response.Success == false)
        {
            return NotFound(response);
        }
        return Ok(response);
    }

    
}