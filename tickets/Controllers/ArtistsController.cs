using Microsoft.AspNetCore.Mvc;
using tickets.Dto;
using tickets.Models;
using tickets.Services;

namespace tickets.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ArtistsController : ControllerBase
{
    private readonly IArtistsService _artistsService;

    public ArtistsController(IArtistsService artistsService)
    {
        _artistsService=artistsService;
    }
    
    [HttpGet]
    public async Task<ActionResult<ServiceResponse<List<ArtistDto>>>> Get()
    {
        ServiceResponse<List<ArtistDto>> response = await _artistsService.GetArtists();
        if (response.Success == false)
        {
            return NotFound(response);
        }
        return Ok(response);
        
    }
    [HttpGet("{id:int}")]
    public async Task<ActionResult<ServiceResponse<List<ArtistDto>>>> Get(int id)
    {
        ServiceResponse<ArtistDto> response = await _artistsService.GetArtistById(id);
        if (response.Success == false)
        {
            return NotFound(response);
        }
        return Ok(response);
    }

}