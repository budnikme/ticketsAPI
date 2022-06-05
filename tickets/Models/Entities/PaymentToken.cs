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
        public string? CardBrand { get; set; }
        public int? ExpMonth { get; set; }
        public int? ExpYear { get; set; }
        public int? Last4 { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
