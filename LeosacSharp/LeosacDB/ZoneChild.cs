using System;
using System.Collections.Generic;

namespace LeosacDB
{
    public partial class ZoneChild
    {
        public long ObjectId { get; set; }
        public long Index { get; set; }
        public long Value { get; set; }

        public virtual Zone Object { get; set; } = null!;
        public virtual Zone ValueNavigation { get; set; } = null!;
    }
}
