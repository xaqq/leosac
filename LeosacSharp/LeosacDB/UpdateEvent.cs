using System;
using System.Collections.Generic;

namespace LeosacDB
{
    public partial class UpdateEvent
    {
        public long Id { get; set; }
        public long? Target { get; set; }

        public virtual AuditEntry IdNavigation { get; set; } = null!;
        public virtual Update? TargetNavigation { get; set; }
    }
}
