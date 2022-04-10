using System;
using System.Collections.Generic;

namespace LeosacDB
{
    public partial class ScheduleMappingCred
    {
        public long ScheduleMappingId { get; set; }
        public long CredentialId { get; set; }

        public virtual Credential Credential { get; set; } = null!;
        public virtual ScheduleMapping ScheduleMapping { get; set; } = null!;
    }
}
