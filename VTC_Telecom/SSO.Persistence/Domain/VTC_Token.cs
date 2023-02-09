namespace SSO.Persistence.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class VTC_Token
    {
        [StringLength(50)]
        public string Id { get; set; }

        [StringLength(50)]
        public string UserId { get; set; }

        [StringLength(500)]
        public string Token { get; set; }

        public DateTime? LoginDate { get; set; }

        public DateTime? Expired { get; set; }

        [StringLength(50)]
        public string AppId { get; set; }

        public virtual VTC_Apps VTC_Apps { get; set; }

        public virtual VTC_Users VTC_Users { get; set; }
    }
}
