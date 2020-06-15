using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace MyPortal.Database.Models
{
    [Table("SenProvision")]
    public class SenProvision
    {
        [DataMember]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [DataMember]
        public Guid StudentId { get; set; }

        [DataMember]
        public Guid ProvisionTypeId { get; set; }

        [DataMember]
        [Column(TypeName = "date")]
        public DateTime StartDate { get; set; }

        [DataMember]
        [Column(TypeName = "date")]
        public DateTime EndDate { get; set; }

        [DataMember]
        [Required]
        public string Note { get; set; }

        public virtual Student Student { get; set; }

        public virtual SenProvisionType Type { get; set; }
    }
}