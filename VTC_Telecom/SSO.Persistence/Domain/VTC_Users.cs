namespace SSO.Persistence.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class VTC_Users
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public VTC_Users()
        {
            VTC_GroupUser = new HashSet<VTC_GroupUser>();
            VTC_Token = new HashSet<VTC_Token>();
        }

        [StringLength(50)]
        public string Id { get; set; }

        [StringLength(30)]
        public string UserName { get; set; }

        [StringLength(50)]
        public string PassWord { get; set; }

        [StringLength(255)]
        public string FullName { get; set; }

        public DateTime? Birthday { get; set; }

        public DateTime? DateCreated { get; set; }

        [StringLength(255)]
        public string Email { get; set; }

        [StringLength(15)]
        public string Phone { get; set; }

        [StringLength(500)]
        public string Address { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VTC_GroupUser> VTC_GroupUser { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VTC_Token> VTC_Token { get; set; }
    }
}
