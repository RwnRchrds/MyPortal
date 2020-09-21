using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using MyPortal.Database.BaseTypes;

namespace MyPortal.Database.Models
{
    [Table("Roles")]
    public class Role : Entity
    {
        public Role()
        {
            UserRoles = new HashSet<UserRole>();
        }

        public string Name { get; set; }

        public bool System { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; }
        public virtual ICollection<RolePermission> RolePermissions { get; set; }
    }
}
