using System;
using System.Collections.Generic;

namespace LeosacDB
{
    public partial class CredentialEvent
    {
        public long Id { get; set; }
        public long? Target { get; set; }
        public long TargetCredId { get; set; }
        public string Before { get; set; } = null!;
        public string After { get; set; } = null!;

        public virtual AuditEntry IdNavigation { get; set; } = null!;
        public virtual Credential? TargetNavigation { get; set; }
    }
}
