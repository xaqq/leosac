using System;
using System.Collections.Generic;

namespace LeosacDB
{
    public partial class HardwareDevice
    {
        public Guid IdUuid { get; set; }
        public string Typeid { get; set; } = null!;
        public string Name { get; set; } = null!;
        public int DeviceClass { get; set; }
        public bool Enabled { get; set; }
        public long Version { get; set; }

        public virtual HardwareBuzzer HardwareBuzzer { get; set; } = null!;
        public virtual HardwareExternalMessage HardwareExternalMessage { get; set; } = null!;
        public virtual HardwareExternalServer HardwareExternalServer { get; set; } = null!;
        public virtual HardwareGpio HardwareGpio { get; set; } = null!;
        public virtual HardwareLed HardwareLed { get; set; } = null!;
        public virtual HardwareRfidreader HardwareRfidreader { get; set; } = null!;
    }
}
