using System;
using System.Collections.Generic;

namespace tickets
{
    public partial class Payment
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public int? EventId { get; set; }
        public DateTime? Time { get; set; }
        public byte? Confirmed { get; set; }
        public Guid? TransactionId { get; set; }

        public virtual Event? Event { get; set; }
        public virtual User? User { get; set; }
    }
}
