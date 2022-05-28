using tickets.Dto;
using tickets.Models;

namespace tickets.Services;

public interface IGenresService
{
    Task<ServiceResponse<List<GenresDto>>> GetGenres();
    Task<ServiceResponse<GenresDto>> GetGenresById(int id);

}