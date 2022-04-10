using System;
using System.Collections.Generic;

namespace LeosacDB
{
    public partial class HardwareRfidreader
    {
        public Guid IdUuid { get; set; }

        public virtual HardwareDevice IdUu { get; set; } = null!;
    }
}
