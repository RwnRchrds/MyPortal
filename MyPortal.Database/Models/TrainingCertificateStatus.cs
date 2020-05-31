using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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

        [StringLength(128)]
        public string ColourCode { get; set; }

        public virtual ICollection<TrainingCertificate> Certificates { get; set; }
    }
}
