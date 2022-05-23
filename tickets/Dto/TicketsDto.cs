namespace tickets.Dto;

public class TicketsDto
{
    public Guid Id { get; set; }
    public int EventId { get; set; }
    public string? EventTittle { get; set; }
    public string? EventCity { get; set; }
    public string? EventPlace { get; set; }
    public string? EventAddress { get; set; }
    public DateTime? EventDate { get; set; }
    public DateTime? PurchaseDate { get; set; }
    public byte? IsPaid { get; set; }

    
    
}