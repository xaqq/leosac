using System;
using System.Collections.Generic;

namespace LeosacDB
{
    public partial class HardwareLed
    {
        public Guid IdUuid { get; set; }
        public long DefaultBlinkDuration { get; set; }
        public long DefaultBlinkSpeed { get; set; }
        public Guid? GpioUuid { get; set; }

        public virtual HardwareGpio? GpioUu { get; set; }
        public virtual HardwareDevice IdUu { get; set; } = null!;
    }
}
