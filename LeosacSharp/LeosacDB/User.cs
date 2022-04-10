using System;
using System.Collections.Generic;

namespace LeosacDB
{
    public partial class User
    {
        public User()
        {
            AuditEntries = new HashSet<AuditEntry>();
            Credentials = new HashSet<Credential>();
            Tokens = new HashSet<Token>();
            UserEvents = new HashSet<UserEvent>();
            UserGroupMembershipEvents = new HashSet<UserGroupMembershipEvent>();
            UserGroupMemberships = new HashSet<UserGroupMembership>();
        }

        public long Id { get; set; }
        public string Username { get; set; } = null!;
        public string? Password { get; set; }
        public string Firstname { get; set; } = null!;
        public string Lastname { get; set; } = null!;
        public string Email { get; set; } = null!;
        public int Rank { get; set; }
        public DateTime? ValidityStart { get; set; }
        public DateTime? ValidityEnd { get; set; }
        public bool ValidityEnabled { get; set; }
        public long Version { get; set; }

        public virtual ICollection<AuditEntry> AuditEntries { get; set; }
        public virtual ICollection<Credential> Credentials { get; set; }
        public virtual ICollection<Token> Tokens { get; set; }
        public virtual ICollection<UserEvent> UserEvents { get; set; }
        public virtual ICollection<UserGroupMembershipEvent> UserGroupMembershipEvents { get; set; }
        public virtual ICollection<UserGroupMembership> UserGroupMemberships { get; set; }
    }
}
