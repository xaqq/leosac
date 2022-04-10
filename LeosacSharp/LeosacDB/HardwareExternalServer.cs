using System;
using System.Collections.Generic;

namespace LeosacDB
{
    public partial class HardwareExternalServer
    {
        public Guid IdUuid { get; set; }
        public string Host { get; set; } = null!;
        public short Port { get; set; }

        public virtual HardwareDevice IdUu { get; set; } = null!;
    }
}
