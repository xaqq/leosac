using System;
using System.Collections.Generic;

namespace LeosacDB
{
    public partial class ScheduleMappingGroup
    {
        public long ScheduleMappingId { get; set; }
        public long GroupId { get; set; }

        public virtual Group Group { get; set; } = null!;
        public virtual ScheduleMapping ScheduleMapping { get; set; } = null!;
    }
}
