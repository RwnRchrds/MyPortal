using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortal.Data.Models
{
    [Table("TrainingCertificateStatus", Schema = "personnel")]
    public class TrainingCertificateStatus
    {
        public TrainingCertificateStatus()
        {
            Certificates = new HashSet<TrainingCertificate>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(256)]
        public string Description { get; set; }

        public virtual ICollection<TrainingCertificate> Certificates { get; set; }
    }
}
