using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Models.Identity
{
    [Table("AspNetRolePermissions")]
    public class RolePermission
    {
        public int Id { get; set; }

        [Required]
        public string RoleId { get; set; }

        public int PermissionId { get; set; }

        public virtual Permission Permission { get; set; }
        public virtual ApplicationRole Role { get; set; }
    }
}
