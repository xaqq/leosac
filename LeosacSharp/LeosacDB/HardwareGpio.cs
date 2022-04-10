using System;
using System.Collections.Generic;

namespace LeosacDB
{
    public partial class HardwareGpio
    {
        public HardwareGpio()
        {
            HardwareBuzzers = new HashSet<HardwareBuzzer>();
            HardwareLeds = new HashSet<HardwareLed>();
        }

        public Guid IdUuid { get; set; }
        public short Number { get; set; }
        public int Direction { get; set; }
        public bool DefaultValue { get; set; }

        public virtual HardwareDevice IdUu { get; set; } = null!;
        public virtual ICollection<HardwareBuzzer> HardwareBuzzers { get; set; }
        public virtual ICollection<HardwareLed> HardwareLeds { get; set; }
    }
}
