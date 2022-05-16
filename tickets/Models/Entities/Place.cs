namespace tickets.Models.Entities
{
    public partial class Place
    {
        public Place()
        {
            Events = new HashSet<Event>();
        }

        public int Id { get; set; }
        public string? City { get; set; }
        public string? Address { get; set; }
        public string? Tittle { get; set; }

        public virtual ICollection<Event> Events { get; set; }
    }
}
