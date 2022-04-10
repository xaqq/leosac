using System;
using System.Collections.Generic;

namespace LeosacDB
{
    public partial class Credential
    {
        public Credential()
        {
            CredentialEvents = new HashSet<CredentialEvent>();
        }

        public long Id { get; set; }
        public string Typeid { get; set; } = null!;
        public long? Owner { get; set; }
        public string Alias { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime? ValidityStart { get; set; }
        public DateTime? ValidityEnd { get; set; }
        public bool ValidityEnabled { get; set; }
        public long OdbVersion { get; set; }

        public virtual User? OwnerNavigation { get; set; }
        public virtual PinCode PinCode { get; set; } = null!;
        public virtual Rfidcard Rfidcard { get; set; } = null!;
        public virtual ICollection<CredentialEvent> CredentialEvents { get; set; }
    }
}
