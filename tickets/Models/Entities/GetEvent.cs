using System;
using System.Collections.Generic;

namespace tickets.Models.Entities
{
    public partial class GetEvent
    {
        public string? Tittle { get; set; }
        public decimal? Price { get; set; }
        public string? City { get; set; }
        public string? Place { get; set; }
        public DateTime? Date { get; set; }
        public string? PreviewLink { get; set; }
        public string? PosterLink { get; set; }
    }
}
