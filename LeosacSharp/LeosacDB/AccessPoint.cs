using System;
using System.Collections.Generic;

namespace LeosacDB
{
    public partial class AccessPoint
    {
        public AccessPoint()
        {
            AccessPointEvents = new HashSet<AccessPointEvent>();
        }

        public long Id { get; set; }
        public string Typeid { get; set; } = null!;
        public string Alias { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string ControllerModule { get; set; } = null!;
        public long Version { get; set; }

        public virtual Door? Door { get; set; }
        public virtual ICollection<AccessPointEvent> AccessPointEvents { get; set; }
    }
}
