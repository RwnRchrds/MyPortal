using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortal.Data.Models
{
    [Table("DetentionAttendanceStatus")]
    public class DetentionAttendanceStatus
    {
        public DetentionAttendanceStatus()
        {
            Detentions = new HashSet<IncidentDetention>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(256)]
        public string Description { get; set; }

        public bool Attended { get; set; }

        public virtual ICollection<IncidentDetention> Detentions { get; set; }
    }
}
