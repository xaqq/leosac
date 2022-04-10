using System;
using System.Collections.Generic;

namespace LeosacDB
{
    public partial class Group
    {
        public Group()
        {
            GroupEvents = new HashSet<GroupEvent>();
            UserGroupMembershipEvents = new HashSet<UserGroupMembershipEvent>();
            UserGroupMemberships = new HashSet<UserGroupMembership>();
        }

        public long Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public long Version { get; set; }

        public virtual ICollection<GroupEvent> GroupEvents { get; set; }
        public virtual ICollection<UserGroupMembershipEvent> UserGroupMembershipEvents { get; set; }
        public virtual ICollection<UserGroupMembership> UserGroupMemberships { get; set; }
    }
}
