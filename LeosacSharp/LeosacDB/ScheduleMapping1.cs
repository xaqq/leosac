using System;
using System.Collections.Generic;

namespace LeosacDB
{
    public partial class ScheduleMapping1
    {
        public long ScheduleId { get; set; }
        public long Index { get; set; }
        public long? ScheduleMappingId { get; set; }

        public virtual Schedule Schedule { get; set; } = null!;
        public virtual ScheduleMapping? ScheduleMapping { get; set; }
    }
}
