using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace tickets.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TicketsController : ControllerBase
{
    private readonly ILogger<TicketsController> _logger;

    public TicketsController(ILogger<TicketsController> logger)
    {
        _logger = logger;
    }

    
}