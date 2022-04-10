using System;
using System.Collections.Generic;

namespace LeosacDB
{
    public partial class AuditEntry
    {
        public AuditEntry()
        {
            Updates = new HashSet<Update>();
        }

        public long Id { get; set; }
        public string Typeid { get; set; } = null!;
        public DateTime Timestamp { get; set; }
        public string Msg { get; set; } = null!;
        public long? Author { get; set; }
        public string EventMask { get; set; } = null!;
        public long Duration { get; set; }
        public bool Finalized { get; set; }
        public long Version { get; set; }

        public virtual User? AuthorNavigation { get; set; }
        public virtual AccessPointEvent AccessPointEvent { get; set; } = null!;
        public virtual CredentialEvent CredentialEvent { get; set; } = null!;
        public virtual DoorEvent DoorEvent { get; set; } = null!;
        public virtual GroupEvent GroupEvent { get; set; } = null!;
        public virtual ScheduleEvent ScheduleEvent { get; set; } = null!;
        public virtual SmtpAudit SmtpAudit { get; set; } = null!;
        public virtual UpdateEvent UpdateEvent { get; set; } = null!;
        public virtual UserEvent UserEvent { get; set; } = null!;
        public virtual UserGroupMembershipEvent UserGroupMembershipEvent { get; set; } = null!;
        public virtual Wsapicall Wsapicall { get; set; } = null!;
        public virtual ZoneEvent ZoneEvent { get; set; } = null!;
        public virtual ICollection<Update> Updates { get; set; }
    }
}
