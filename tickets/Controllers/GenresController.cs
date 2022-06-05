using Microsoft.AspNetCore.Mvc;
using tickets.Dto;
using tickets.Models;
using tickets.Services;

namespace tickets.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GenresController : ControllerBase
{
    private readonly IGenresService _genresService;

    public GenresController(IGenresService genresService)
    {
        _genresService = genresService;
    }
    
    [HttpGet]
    public async Task<ActionResult<ServiceResponse<List<GenresDto>>>> Get()
    {
        ServiceResponse<List<GenresDto>> response = await _genresService.GetGenres();
        if (response.Success == false)
        {
            return NotFound(response);
        }
        return Ok(response);
        
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ServiceResponse<GenresDto>>> Get(int id)
    {
        ServiceResponse<GenresDto> response = await _genresService.GetGenresById(id);
        if (response.Success == false)
        {
            return NotFound(response);
        }
        return Ok(response);
    }
    

}