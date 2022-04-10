using System;
using System.Collections.Generic;

namespace LeosacDB
{
    public partial class SmtpConfigServer
    {
        public long SmtpconfigId { get; set; }
        public long Index { get; set; }
        public string ValueUrl { get; set; } = null!;
        public string ValueFrom { get; set; } = null!;
        public string ValueUsername { get; set; } = null!;
        public string ValuePassword { get; set; } = null!;
        public int ValueMsTimeout { get; set; }
        public bool ValueVerifyHost { get; set; }
        public bool ValueVerifyPeer { get; set; }
        public string ValueCaInfoFile { get; set; } = null!;
        public bool ValueEnabled { get; set; }

        public virtual SmtpConfig Smtpconfig { get; set; } = null!;
    }
}
