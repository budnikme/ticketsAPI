namespace tickets.Dto;

public class TicketTypeDto
{
    public string Tittle { get; set; }
    public string Description { get; set; }
    public decimal? price { get; set; }
    public int? count { get; set; }
}