using System;
using System.Collections.Generic;

namespace LeosacDB
{
    public partial class PinCode
    {
        public long Id { get; set; }
        public string PinCode1 { get; set; } = null!;

        public virtual Credential IdNavigation { get; set; } = null!;
    }
}
