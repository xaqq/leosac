using System;
using System.Collections.Generic;

namespace LeosacDB
{
    public partial class AccessPointEvent
    {
        public long Id { get; set; }
        public long? Target { get; set; }
        public long TargetApId { get; set; }
        public string Before { get; set; } = null!;
        public string After { get; set; } = null!;

        public virtual AuditEntry IdNavigation { get; set; } = null!;
        public virtual AccessPoint? TargetNavigation { get; set; }
    }
}
