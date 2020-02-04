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

        public int Id { get; set; }
        public int AreaId { get; set; }

        [Required]
        [StringLength(128)]
        public string Code { get; set; }

        [StringLength(256)]
        public string Name { get; set; }

        public virtual SystemArea Area { get; set; }
        public virtual ICollection<ApplicationPermission> Permissions { get; set; }
    }
}
