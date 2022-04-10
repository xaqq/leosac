using System;
using System.Collections.Generic;

namespace LeosacDB
{
    public partial class ScheduleMappingUser
    {
        public long ScheduleMappingId { get; set; }
        public long UserId { get; set; }

        public virtual ScheduleMapping ScheduleMapping { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
