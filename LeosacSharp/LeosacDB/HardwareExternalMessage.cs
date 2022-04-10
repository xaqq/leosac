using System;
using System.Collections.Generic;

namespace LeosacDB
{
    public partial class HardwareExternalMessage
    {
        public Guid IdUuid { get; set; }
        public string Subject { get; set; } = null!;
        public int Direction { get; set; }
        public int Virtualtype { get; set; }
        public string Payload { get; set; } = null!;

        public virtual HardwareDevice IdUu { get; set; } = null!;
    }
}
