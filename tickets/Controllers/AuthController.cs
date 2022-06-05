using Microsoft.AspNetCore.Mvc;
using tickets.Dto;
using tickets.Services;

namespace tickets.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<ActionResult> Register(RegisterDto registerDto)
    {
        var token = await _authService.Register(registerDto);
        if (token==null)
        {
            return BadRequest("User already exists");
        }

        return Ok(token);
        
    }

    [HttpPost("login")]
    public async Task<ActionResult<string>> Login(LoginDto loginDto)
    {
        var token = await _authService.Login(loginDto);
        if (token == null)
        {
            return BadRequest("Wrong Email or Password");
        }
        return Ok(token);
    }

    
}