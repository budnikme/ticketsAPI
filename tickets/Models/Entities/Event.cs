using System;
using System.Collections.Generic;

namespace tickets.Models.Entities
{
    public partial class Event
    {
        public Event()
        {
            TicketTypes = new HashSet<TicketType>();
            Artists = new HashSet<Artist>();
            Genres = new HashSet<Genre>();
        }

        public int Id { get; set; }
        public string? Type { get; set; }
        public int? PlaceId { get; set; }
        public string? Tittle { get; set; }
        public DateTime? Date { get; set; }
        public string? Description { get; set; }
        public string? PreviewLink { get; set; }
        public string? PosterLink { get; set; }

        public virtual Place? Place { get; set; }
        public virtual ICollection<TicketType> TicketTypes { get; set; }

        public virtual ICollection<Artist> Artists { get; set; }
        public virtual ICollection<Genre> Genres { get; set; }
    }
}
