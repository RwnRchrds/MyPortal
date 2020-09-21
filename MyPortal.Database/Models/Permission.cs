using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using MyPortal.Database.BaseTypes;

namespace MyPortal.Database.Models
{
    [Table("Permissions")]
    public class Permission : Entity
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
