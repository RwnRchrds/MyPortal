using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using MyPortal.Database.BaseTypes;

namespace MyPortal.Database.Models
{
    [Table("TrainingCertificateStatus")]
    public class TrainingCertificateStatus : LookupItem
    {
        public TrainingCertificateStatus()
        {
            Certificates = new HashSet<TrainingCertificate>();
        }

        [Column(Order = 3)]
        [StringLength(128)]
        public string ColourCode { get; set; }

        public virtual ICollection<TrainingCertificate> Certificates { get; set; }
    }
}
