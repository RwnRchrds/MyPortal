using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyPortal.Models.Database
{
    /// <summary>
    /// A record of a medical event/emergency.
    /// </summary>
    [Table("Medical_Events")]
    public class MedicalEvent
    {
        public int Id { get; set; }

        public int StudentId { get; set; }

        public int RecordedById { get; set; }

        [Column(TypeName = "date")]
        public DateTime Date { get; set; }

        [Required]
        [StringLength(100)]
        public string Note { get; set; }

        public virtual StaffMember RecordedBy { get; set; }

        public virtual Student Student { get; set; }
    }
}