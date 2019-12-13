using System.Collections.Generic;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MyPortal.BusinessLogic.Models.Identity
{
    public class ApplicationRole : IdentityRole
    {
        public ApplicationRole() : base()
        {
        }

        public ApplicationRole(string name) : base(name)
        {
            System = false;
        }

        public string Description { get; set; }
        public bool System { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RolePermission> RolePermissions { get; set; }
    }
}
