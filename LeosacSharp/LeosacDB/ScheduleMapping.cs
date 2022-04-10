using System;
using System.Collections.Generic;

namespace LeosacDB
{
    public partial class ScheduleMapping
    {
        public long Id { get; set; }
        public string Alias { get; set; } = null!;
        public long OdbVersion { get; set; }
    }
}
