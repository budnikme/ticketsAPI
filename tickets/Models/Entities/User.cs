namespace tickets.Models.Entities
{
    public partial class User
    {
        public User()
        {
            Payments = new HashSet<Payment>();
            Tickets = new HashSet<Ticket>();
        }

        public int Id { get; set; }
        public string? Email { get; set; }
        public string? PasswordHash { get; set; }
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public string? PhoneNumber { get; set; }

        public virtual ICollection<Payment> Payments { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
