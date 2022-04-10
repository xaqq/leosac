using System;
using System.Collections.Generic;

namespace LeosacDB
{
    public partial class ZoneEvent
    {
        public long Id { get; set; }
        public long? Target { get; set; }
        public long TargetZoneId { get; set; }
        public string Before { get; set; } = null!;
        public string After { get; set; } = null!;

        public virtual AuditEntry IdNavigation { get; set; } = null!;
        public virtual Zone? TargetNavigation { get; set; }
    }
}
