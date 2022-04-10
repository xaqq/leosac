using System;
using System.Collections.Generic;

namespace LeosacDB
{
    public partial class Schedule
    {
        public Schedule()
        {
            ScheduleEvents = new HashSet<ScheduleEvent>();
        }

        public long Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public long OdbVersion { get; set; }

        public virtual ICollection<ScheduleEvent> ScheduleEvents { get; set; }
    }
}
