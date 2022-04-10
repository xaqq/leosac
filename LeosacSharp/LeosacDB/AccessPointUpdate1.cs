using System;
using System.Collections.Generic;

namespace LeosacDB
{
    public partial class AccessPointUpdate1
    {
        public long ObjectId { get; set; }
        public long Index { get; set; }
        public long Value { get; set; }

        public virtual AccessPoint Object { get; set; } = null!;
        public virtual AccessPointUpdate ValueNavigation { get; set; } = null!;
    }
}
