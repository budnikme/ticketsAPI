using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using tickets.Dto;
using tickets.Services;



namespace tickets.Controllers;

[Route("api/[controller]")]
[ApiController]
//[Authorize(Roles = "Admin")]
public class AdminController : ControllerBase
{
    private readonly IAdminService _adminService;

    public AdminController(IAdminService adminService)
    {
        _adminService = adminService;
    }
    [HttpPut("addEvent")]
    public async Task<ActionResult> AddEvent(AdminEventDto e){
        if (!await _adminService.AddEvent(e))
        {
            return BadRequest();
        }
        return Ok();
    }

    [HttpDelete("deleteEvent")]
    public async Task<ActionResult> DeleteEvent(int eventId)
    {
        if (!await _adminService.DeleteEvent(eventId))
        {
            return BadRequest();
        }
        return Ok();
    }

    [HttpPost("changeEvent/{eventId:int}")]
    public async Task<ActionResult> ChangeEvent(int eventId, AdminEventDto e)
    {
        if (!await _adminService.ChangeEvent(eventId,e))
        {
            return BadRequest();
        }
        return Ok();
    }

}