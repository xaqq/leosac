using System;
using System.Collections.Generic;

namespace LeosacDB
{
    public partial class AuditEntryChild
    {
        public long ObjectId { get; set; }
        public long Index { get; set; }
        public long Value { get; set; }

        public virtual AuditEntry Object { get; set; } = null!;
        public virtual AuditEntry ValueNavigation { get; set; } = null!;
    }
}
