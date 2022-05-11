using System;
using System.Collections.Generic;

namespace tickets.Models
{
    public partial class Genre
    {
        public Genre()
        {
            Events = new HashSet<Event>();
        }

        public int Id { get; set; }
        public string? Genre1 { get; set; }

        public virtual ICollection<Event> Events { get; set; }
    }
}
