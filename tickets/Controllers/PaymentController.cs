using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using tickets.Dto;
using tickets.Models;
using tickets.Services;

namespace tickets.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "User,Admin")]
public class PaymentController : ControllerBase
{
    private readonly IPaymentService _paymentService;
    private readonly int _userId;


    public PaymentController(IPaymentService paymentService, IHttpContextAccessor httpContextAccessor)
    {
        _paymentService=paymentService;
        //getting user id
        _userId = int.Parse(httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
    }
    [HttpPost("buy/{ticketTypeId:int}")]
    public async Task<ActionResult<ServiceResponse<TicketsDto>>> Get(int ticketTypeId)
    {
        var response = await _paymentService.BuyTicket(ticketTypeId, _userId);
        if (response.Success == false)
        {
            return BadRequest(response);
        }
        return Ok(response);
        
    }
    
    [HttpPut("addCard")]
    public async Task<ActionResult<ServiceResponse<CardDataDto>>> AddCard(CardDto card)
    {
        var response = await _paymentService.AddCard(card, _userId);
        if (response.Success == false)
        {
            return BadRequest(response);
        }
        return Ok(response);
    }

    [HttpDelete("deleteCard")]
    public async Task<ActionResult<ServiceResponse<bool>>> DeleteCard(int cardId)
    {
        var response = await _paymentService.DeleteCard(cardId, _userId);
        if (response.Success == false)
        {
            return BadRequest(response);
        }

        return Ok(response);

    }

    [HttpGet("myCards")]
    public async Task<ActionResult<ServiceResponse<List<CardDataDto>>>> GetCards()
    {
        ServiceResponse<List<CardDataDto>> response = await _paymentService.UserCards(_userId);
        if (response.Success == false)
        {
            return BadRequest(response);
        }
        return Ok(response);
    }

    

}