using System;
using System.Collections.Generic;

namespace tickets.Models.Entities
{
    public partial class Ticket
    {
        public Guid Id { get; set; }
        public int? EventId { get; set; }
        public int? UserId { get; set; }
        public int? TicketTypeId { get; set; }
        public int? PaymentId { get; set; }

        public virtual Payment? Payment { get; set; }
        public virtual TicketType? TicketType { get; set; }
        public virtual User? User { get; set; }
    }
}
