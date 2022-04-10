using System;
using System.Collections.Generic;

namespace LeosacDB
{
    public partial class ScheduleMappingDoor
    {
        public long ScheduleMappingId { get; set; }
        public long DoorId { get; set; }

        public virtual Door Door { get; set; } = null!;
        public virtual ScheduleMapping ScheduleMapping { get; set; } = null!;
    }
}
