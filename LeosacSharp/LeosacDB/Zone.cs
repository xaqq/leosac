using System;
using System.Collections.Generic;

namespace LeosacDB
{
    public partial class Zone
    {
        public Zone()
        {
            ZoneEvents = new HashSet<ZoneEvent>();
        }

        public long Id { get; set; }
        public string Alias { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int Type { get; set; }
        public long Version { get; set; }

        public virtual ICollection<ZoneEvent> ZoneEvents { get; set; }
    }
}
