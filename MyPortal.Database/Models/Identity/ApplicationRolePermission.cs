using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using MyPortal.Database.BaseTypes;
using MyPortal.Database.Interfaces;

namespace MyPortal.Database.Models.Identity
{
    [Table("AspNetRolePermissions")]
    public class ApplicationRolePermission : Entity
    {
        [Column(Order = 1)]
        public Guid RoleId { get; set; }

        [Column(Order = 2)]
        public Guid PermissionId { get; set; }

        public virtual ApplicationRole Role { get; set; }
        public virtual ApplicationPermission Permission { get; set; }
    }
}
