namespace tickets.Models.Entities
{
    public partial class TicketType
    {
        public int Id { get; set; }
        public int? EventId { get; set; }
        public string? Tittle { get; set; }
        public string? Description { get; set; }
        public decimal? Price { get; set; }

        public virtual Event? Event { get; set; }
    }
}
