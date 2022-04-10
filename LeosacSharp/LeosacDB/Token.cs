using System;
using System.Collections.Generic;

namespace LeosacDB
{
    public partial class Token
    {
        public string Token1 { get; set; } = null!;
        public long Owner { get; set; }
        public DateTime Expiration { get; set; }
        public long Version { get; set; }

        public virtual User OwnerNavigation { get; set; } = null!;
    }
}
