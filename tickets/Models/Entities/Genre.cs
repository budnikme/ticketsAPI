namespace tickets.Models.Entities
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
