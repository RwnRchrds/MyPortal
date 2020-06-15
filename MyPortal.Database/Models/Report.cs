using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace MyPortal.Database.Models
{
    [Table("Report")]
    public class Report
    {

        [DataMember]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [DataMember]
        public Guid AreaId { get; set; }

        [DataMember]
        [Required]
        [StringLength(128)]
        public string Name { get; set; }

        [DataMember]
        [Required]
        [StringLength(128)]
        public string Description { get; set; }

        [DataMember]
        public bool Restricted { get; set; }

        public virtual SystemArea SystemArea { get; set; }
    }
}