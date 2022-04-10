using System;
using System.Collections.Generic;

namespace LeosacDB
{
    public partial class ZoneDoor
    {
        public long ObjectId { get; set; }
        public long Index { get; set; }
        public long Value { get; set; }

        public virtual Zone Object { get; set; } = null!;
        public virtual Door ValueNavigation { get; set; } = null!;
    }
}
