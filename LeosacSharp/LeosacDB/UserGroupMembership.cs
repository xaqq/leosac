using System;
using System.Collections.Generic;

namespace LeosacDB
{
    public partial class UserGroupMembership
    {
        public long Id { get; set; }
        public long User { get; set; }
        public long Group { get; set; }
        public DateTime Timestamp { get; set; }
        public int Rank { get; set; }
        public long Version { get; set; }

        public virtual Group GroupNavigation { get; set; } = null!;
        public virtual User UserNavigation { get; set; } = null!;
    }
}
