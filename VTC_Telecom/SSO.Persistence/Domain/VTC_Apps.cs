namespace SSO.Persistence.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class VTC_Apps
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public VTC_Apps()
        {
            VTC_Roles = new HashSet<VTC_Roles>();
            VTC_Token = new HashSet<VTC_Token>();
        }

        [StringLength(50)]
        public string Id { get; set; }

        [StringLength(50)]
        public string AppId { get; set; }

        [StringLength(255)]
        public string Name { get; set; }

        [StringLength(100)]
        public string Domain { get; set; }

        [StringLength(50)]
        public string SecretKey { get; set; }

        public string Note { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VTC_Roles> VTC_Roles { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VTC_Token> VTC_Token { get; set; }
    }
}
