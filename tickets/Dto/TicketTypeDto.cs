namespace tickets.Dto;

public class TicketTypeDto
{
    public int Id { get; set; }
    public string? Tittle { get; set; }
    public string? Description { get; set; }
    public decimal? Price { get; set; }
    public int? Count { get; set; }
}