using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using MyPortal.Database.BaseTypes;
using MyPortal.Database.Interfaces;

namespace MyPortal.Database.Models.Identity
{
    [Table("AspNetPermissions")]
    public class ApplicationPermission : Entity
    {
        [Column(Order = 1)]
        public Guid AreaId { get; set; }

        [Required]
        [StringLength(128)]
        [Column(Order = 2)]
        public string ShortDescription { get; set; }        

        [Required]
        [StringLength(256)]
        [Column(Order = 3)]
        public string FullDescription { get; set; }

        [Required]
        [StringLength(128)]
        [Column(Order = 4)]
        public string ClaimValue { get; set; }

        public virtual SystemArea Area { get; set; }

        public virtual ICollection<ApplicationRolePermission> RolePermissions { get; set; }
    }
}
