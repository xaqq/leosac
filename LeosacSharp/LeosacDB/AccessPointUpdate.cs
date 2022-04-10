using System;
using System.Collections.Generic;

namespace LeosacDB
{
    public partial class AccessPointUpdate
    {
        public long Id { get; set; }

        public virtual Update IdNavigation { get; set; } = null!;
    }
}
