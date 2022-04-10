using System;
using System.Collections.Generic;

namespace LeosacDB
{
    public partial class UserGroupMembershipEvent
    {
        public long Id { get; set; }
        public long? TargetGroup { get; set; }
        public long? TargetUser { get; set; }
        public long TargetGroupId { get; set; }
        public long TargetUserId { get; set; }

        public virtual AuditEntry IdNavigation { get; set; } = null!;
        public virtual Group? TargetGroupNavigation { get; set; }
        public virtual User? TargetUserNavigation { get; set; }
    }
}
