using Microsoft.AspNetCore.Mvc;
using tickets.Dto;
using tickets.Models;
using tickets.Services;

namespace tickets.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TicketsController : ControllerBase
{
    private readonly ITicketsService _ticketsService;

    public TicketsController(ITicketsService ticketsService)
    {
        _ticketsService=ticketsService;
    }

    [HttpGet("{eventId:int}")]
    public async Task<ActionResult<ServiceResponse<List<TicketTypeDto>>>> GetTicketTypes(int eventId)
    {
        ServiceResponse<List<TicketTypeDto>> response = await _ticketsService.GetTicketTypes(eventId);
        if (response.Success == false)
        {
            return NotFound(response);
        }
        return Ok(response);
    }
    

    
}