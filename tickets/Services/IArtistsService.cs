using tickets.Dto;
using tickets.Models;

namespace tickets.Services;

public interface IArtistsService
{
    Task<ServiceResponse<List<ArtistDto>>> GetArtists();
    Task<ServiceResponse<ArtistDto>> GetArtistById(int id);
}