using System;
using System.Collections.Generic;

namespace tickets.Models.Entities
{
    public partial class PaymentToken
    {
        public PaymentToken()
        {
            Users = new HashSet<User>();
        }

        public int Id { get; set; }
        public string? Token { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
