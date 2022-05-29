using System;
using System.Collections.Generic;

namespace tickets.Models.Entities
{
    public partial class Payment
    {
        public Payment()
        {
            Tickets = new HashSet<Ticket>();
        }

        public int Id { get; set; }
        public DateTime? Time { get; set; }
        public byte? Confirmed { get; set; }
        public Guid? TransactionId { get; set; }
        public decimal? Sum { get; set; }
        public int? UserId { get; set; }

        public virtual User? User { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
