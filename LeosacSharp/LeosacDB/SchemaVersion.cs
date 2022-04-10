using System;
using System.Collections.Generic;

namespace LeosacDB
{
    public partial class SchemaVersion
    {
        public string Name { get; set; } = null!;
        public long Version { get; set; }
        public bool Migration { get; set; }
    }
}
