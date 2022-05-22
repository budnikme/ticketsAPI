namespace tickets.Dto;

public class EventDto
{
    public int Id { get; set; }
    public string Tittle { get; set; }
    public string Type { get; set; }
    public decimal? Price { get; set; }
    public string City { get; set; } = string.Empty;
    public string Place { get; set; } = string.Empty;
    public DateTime? Date { get; set; }
    public string PreviewLink {get; set; } = string.Empty;
    public string PosterLink {get; set; } = string.Empty;
}