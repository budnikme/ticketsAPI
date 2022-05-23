using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using tickets.Models.Entities;

namespace tickets.Models.Entities
{
    public partial class TicketType
    {
        public TicketType()
        {
            Tickets = new HashSet<Ticket>();
        }

        public int Id { get; set; }
        public int EventId { get; set; }
        public string? Tittle { get; set; }
        public string? Description { get; set; }
        public decimal? Price { get; set; }
        public int? Count { get; set; }

        public virtual Event Event { get; set; } = null!;
        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
