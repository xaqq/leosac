using System;
using System.Collections.Generic;

namespace LeosacDB
{
    public partial class Wsapicall
    {
        public long Id { get; set; }
        public string ApiMethod { get; set; } = null!;
        public string Uuid { get; set; } = null!;
        public int StatusCode { get; set; }
        public string StatusString { get; set; } = null!;
        public string SourceEndpoint { get; set; } = null!;
        public string RequestContent { get; set; } = null!;
        public string ResponseContent { get; set; } = null!;
        public short DatabaseOperations { get; set; }

        public virtual AuditEntry IdNavigation { get; set; } = null!;
    }
}
