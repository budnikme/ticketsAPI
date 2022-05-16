namespace tickets.Models.Entities
{
    public partial class Ticket
    {
        public Guid Id { get; set; }
        public int? EventId { get; set; }
        public int? UserId { get; set; }

        public virtual Event? Event { get; set; }
        public virtual User? User { get; set; }
    }
}
