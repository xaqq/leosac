using System;
using System.Collections.Generic;

namespace LeosacDB
{
    public partial class ScheduleTimeframe
    {
        public long ScheduleId { get; set; }
        public long Index { get; set; }
        public int TimeframeDay { get; set; }
        public int TimeframeStartHour { get; set; }
        public int TimeframeStartMin { get; set; }
        public int TimeframeEndHour { get; set; }
        public int TimeframeEndMin { get; set; }

        public virtual Schedule Schedule { get; set; } = null!;
    }
}
