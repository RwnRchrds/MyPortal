using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models.Entity
{
    [Table("Permissions")]
    public class Permission : BaseTypes.Entity
    {
        public Permission()
        {
            RolePermissions = new HashSet<RolePermission>();
        }

        [Column(Order = 1)]
        public Guid AreaId { get; set; }

        [Column(Order = 2)]
        public string ShortDescription { get; set; }

        [Column(Order = 3)]
        public string FullDescription { get; set; }

        public virtual SystemArea SystemArea { get; set; }

        public virtual ICollection<RolePermission> RolePermissions { get; set; }
    }
}
