namespace tickets.Dto;

public class CardDto
{
    public string Number { get; set; } = string.Empty;
    public int ExpMonth { get; set; }
    public int ExpYear { get; set; }
    public string Cvc {get; set;} = string.Empty;
    
}