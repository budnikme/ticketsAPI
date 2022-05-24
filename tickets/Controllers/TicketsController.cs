using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
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
    private readonly IHttpContextAccessor _httpContextAccessor;


    public TicketsController(ITicketsService ticketsService,IHttpContextAccessor httpContextAccessor)
    {
        _ticketsService=ticketsService;
        _httpContextAccessor = httpContextAccessor;

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

    [HttpGet("my"),Authorize]
    public async Task<ActionResult<ServiceResponse<List<TicketsDto>>>> GetUserTickets()
    {
        //getting userId
        int userId = int.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
        ServiceResponse<List<TicketsDto>> response = await _ticketsService.GetUserTickets(userId);
        if (response.Success == false)
        {
            return NotFound(response);
        }
        return Ok(response);
    }
    

    
}