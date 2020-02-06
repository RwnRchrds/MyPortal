using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models
{
    [Table("TrainingCertificateStatus")]
    public class TrainingCertificateStatus
    {
        public TrainingCertificateStatus()
        {
            Certificates = new HashSet<TrainingCertificate>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        [StringLength(256)]
        public string Description { get; set; }

        [StringLength(128)]
        public string ColourCode { get; set; }

        public virtual ICollection<TrainingCertificate> Certificates { get; set; }
    }
}
