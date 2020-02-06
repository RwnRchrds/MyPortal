using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using MyPortal.Database.Models.Identity;

namespace MyPortal.Database.Models
{
    [Table("SystemResource")]
    public class SystemResource
    {
        public SystemResource()
        {
            Permissions = new HashSet<ApplicationPermission>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public Guid AreaId { get; set; }

        [Required]
        [StringLength(128)]
        public string TableName { get; set; }

        [Required]
        [StringLength(256)]
        public string Name { get; set; }

        public bool HasPermissions { get; set; }        

        public virtual SystemArea Area { get; set; }
        public virtual ICollection<ApplicationPermission> Permissions { get; set; }
    }
}
