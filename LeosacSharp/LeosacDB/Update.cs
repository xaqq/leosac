using System;
using System.Collections.Generic;

namespace LeosacDB
{
    public partial class Update
    {
        public Update()
        {
            UpdateEvents = new HashSet<UpdateEvent>();
        }

        public long Id { get; set; }
        public string Typeid { get; set; } = null!;
        public DateTime? GeneratedAt { get; set; }
        public long? CheckpointLast { get; set; }
        public DateTime? StatusUpdatedAt { get; set; }
        public string SourceModule { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int Status { get; set; }
        public long OdbVersion { get; set; }

        public virtual AuditEntry? CheckpointLastNavigation { get; set; }
        public virtual AccessPointUpdate AccessPointUpdate { get; set; } = null!;
        public virtual ICollection<UpdateEvent> UpdateEvents { get; set; }
    }
}
