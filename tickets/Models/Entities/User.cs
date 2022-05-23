using System;
using System.Collections.Generic;

namespace tickets.Models.Entities
{
    public partial class User
    {
        public User()
        {
            Tickets = new HashSet<Ticket>();
        }

        public int Id { get; set; }
        public string? Email { get; set; }
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public byte[] PasswordHash { get; set; } = null!;
        public byte[] PasswordSalt { get; set; } = null!;
        public string Type { get; set; } = null!;

        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
