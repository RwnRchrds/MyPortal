using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.BusinessLogic.Models.Identity
{
    [Table("AspNetPermissions")]
    public class Permission
    {
        public int Id { get; set; }
        public int AreaId { get; set; }

        [StringLength(50)] [Required] public string Name { get; set; }

        [StringLength(255)] [Required] public string Description { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage",
            "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RolePermission> RolePermissions { get; set; }
    }
}
