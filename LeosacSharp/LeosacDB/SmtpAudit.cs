using System;
using System.Collections.Generic;

namespace LeosacDB
{
    public partial class SmtpAudit
    {
        public long Id { get; set; }

        public virtual AuditEntry IdNavigation { get; set; } = null!;
    }
}
