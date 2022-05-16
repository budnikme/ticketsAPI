using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace tickets.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly ILogger<UserController> _logger;

    public UserController(ILogger<UserController> logger)
    {
        _logger = logger;
    }

    
}