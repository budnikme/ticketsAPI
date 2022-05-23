using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using tickets.Dto;
using tickets.Models;
using tickets.Services;

namespace tickets.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly int _userId;

    public UserController(IUserService userService,IHttpContextAccessor httpContextAccessor)
    {
        _userService = userService;
        //getting users id
        _userId = int.Parse(httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
    }
    [HttpGet,Authorize]
    public async Task<ActionResult<ServiceResponse<UserViewDto>>> GetMe()
    {
        ServiceResponse<UserViewDto> response = await _userService.GetMe(_userId);
        if (response.Success == false)
        {
            return BadRequest(response);
        }
        return Ok(response);
    }
}