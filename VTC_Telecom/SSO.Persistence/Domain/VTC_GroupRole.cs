namespace SSO.Persistence.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class VTC_GroupRole
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(50)]
        public string Id { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string GroupId { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(50)]
        public string RoleId { get; set; }

        public virtual VTC_Groups VTC_Groups { get; set; }

        public virtual VTC_Roles VTC_Roles { get; set; }
    }
}
