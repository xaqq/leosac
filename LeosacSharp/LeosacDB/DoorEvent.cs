using System;
using System.Collections.Generic;

namespace LeosacDB
{
    public partial class DoorEvent
    {
        public long Id { get; set; }
        public long? Target { get; set; }
        public long TargetDoorId { get; set; }
        public string Before { get; set; } = null!;
        public string After { get; set; } = null!;
        public long AccessPointIdBefore { get; set; }
        public long AccessPointIdAfter { get; set; }

        public virtual AuditEntry IdNavigation { get; set; } = null!;
        public virtual Door? TargetNavigation { get; set; }
    }
}
