using System;
using System.Collections.Generic;

namespace LeosacDB
{
    public partial class Rfidcard
    {
        public long Id { get; set; }
        public int NbBits { get; set; }
        public string CardId { get; set; } = null!;

        public virtual Credential IdNavigation { get; set; } = null!;
    }
}
