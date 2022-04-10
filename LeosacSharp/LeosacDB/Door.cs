using System;
using System.Collections.Generic;

namespace LeosacDB
{
    public partial class Door
    {
        public Door()
        {
            DoorEvents = new HashSet<DoorEvent>();
        }

        public long Id { get; set; }
        public string Alias { get; set; } = null!;
        public string Desc { get; set; } = null!;
        public long? AccessPoint { get; set; }
        public long Version { get; set; }

        public virtual AccessPoint? AccessPointNavigation { get; set; }
        public virtual ICollection<DoorEvent> DoorEvents { get; set; }
    }
}
