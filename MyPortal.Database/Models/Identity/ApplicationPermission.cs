using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MyPortal.Database.Models.Identity
{
    [Table("AspNetPermissions")]
    public class ApplicationPermission
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public Guid AreaId { get; set; }

        [Required]
        [StringLength(128)]
        public string ShortDescription { get; set; }        

        [Required]
        [StringLength(256)]
        public string FullDescription { get; set; }

        [Required]
        [StringLength(128)]
        public string ClaimValue { get; set; }

        public virtual SystemArea Area { get; set; }

        public virtual ICollection<ApplicationRolePermission> RolePermissions { get; set; }
    }
}
