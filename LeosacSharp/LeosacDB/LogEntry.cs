using System;
using System.Collections.Generic;

namespace LeosacDB
{
    public partial class LogEntry
    {
        public long Id { get; set; }
        public DateTime Timestamp { get; set; }
        public string Msg { get; set; } = null!;
        public string RunId { get; set; } = null!;
        public short Level { get; set; }
        public long ThreadId { get; set; }
        public long Version { get; set; }
    }
}
