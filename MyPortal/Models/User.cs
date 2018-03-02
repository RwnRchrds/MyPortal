namespace MyPortal
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class User
    {
        [StringLength(50)]
        public string Id { get; set; }

        [Required]
        [StringLength(255)]
        public string PasswordHash { get; set; }

        public int Type { get; set; }
    }
}
