using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using MyPortal.Database.Models.Identity;

namespace MyPortal.Database.Models
{
    [Table("SystemArea")]
    public class SystemArea
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SystemArea()
        {
            Reports = new HashSet<Report>();
            SubAreas = new HashSet<SystemArea>();
        }

        [DataMember]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [DataMember]
        [Required]
        [StringLength(128)]
        public string Description { get; set; }

        [DataMember]
        public Guid? ParentId { get; set; }

        public virtual SystemArea Parent { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Report> Reports { get; set; }

        public virtual ICollection<SystemArea> SubAreas { get; set; }

        public virtual ICollection<ApplicationPermission> Permissions { get; set; }
    }
}