using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models.Entity
{
    [Table("AspNetRolePermissions")]
    public class RolePermission
    {
        [Key]
        [Column(Order = 1)]
        public Guid RoleId { get; set; }

        [Key]
        [Column(Order = 2)]
        public Guid PermissionId { get; set; }

        public virtual Role Role { get; set; }
        public virtual Permission Permission { get; set; }
    }
}
