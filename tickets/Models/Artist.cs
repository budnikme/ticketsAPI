using System;
using System.Collections.Generic;

namespace tickets.Models
{
    public partial class Artist
    {
        public Artist()
        {
            Events = new HashSet<Event>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }

        public virtual ICollection<Event> Events { get; set; }
    }
}
