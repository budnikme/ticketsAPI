namespace tickets.Dto;

public class FullEventDto
{
    public int Id { get; set; }
    public string? Tittle { get; set; }
    public string? Type { get; set; }
    public decimal? Price { get; set; }
    public string? City { get; set; }
    public string? Address { get; set; }
    public string? Place { get; set; } 
    public DateTime? Date { get; set; }
    public string? PreviewLink {get; set; } 
    public string? PosterLink {get; set; } 
    public string? Description { get; set; }
    public List<string>? Genres { get; set; }
    public List<string>? Artists { get; set; }

}