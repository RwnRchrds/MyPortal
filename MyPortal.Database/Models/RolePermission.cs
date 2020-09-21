using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text;
using MyPortal.Database.BaseTypes;

namespace MyPortal.Database.Models
{
    [Table("RolePermissions")]
    public class RolePermission : Entity
    {
        [Column(Order = 1)]
        public Guid RoleId { get; set; }

        [Column(Order = 2)]
        public Guid PermissionId { get; set; }

        public virtual Role Role { get; set; }
        public virtual Permission Permission { get; set; }
    }
}
