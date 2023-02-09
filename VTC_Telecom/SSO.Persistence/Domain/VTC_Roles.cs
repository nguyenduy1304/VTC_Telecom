namespace SSO.Persistence.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class VTC_Roles
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public VTC_Roles()
        {
            VTC_GroupRole = new HashSet<VTC_GroupRole>();
        }

        [StringLength(50)]
        public string Id { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        public string AppId { get; set; }

        public string Permission { get; set; }

        public string Description { get; set; }

        public virtual VTC_Apps VTC_Apps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VTC_GroupRole> VTC_GroupRole { get; set; }
    }
}
