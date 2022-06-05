namespace tickets.Dto;

public class AdminEventDto
{
    public string? Type { get; set; }
    public int PlaceId { get; set; }
    public string? Tittle { get; set; }
    public DateTime Date { get; set; }
    public string? Description { get; set; }
    public string? PreviewLink {get; set; }
    public string? PosterLink {get; set; }
    public int[]? ArtistsId {get; set;}
    public int[]? GenresId { get; set; }
    public List<TicketTypeDto>? TicketTypes { get; set; }
    
    
}