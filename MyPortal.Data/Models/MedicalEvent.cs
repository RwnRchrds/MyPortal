using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Data.Models
{
    /// <summary>
    /// A record of a medical event/emergency.
    /// </summary>
    [Table("MedicalEvent")]
    public class MedicalEvent
    {
        public int Id { get; set; }

        public int StudentId { get; set; }

        public int RecordedById { get; set; }

        [Column(TypeName = "date")]
        public DateTime Date { get; set; }

        [Required]
        public string Note { get; set; }

        public virtual StaffMember RecordedBy { get; set; }

        public virtual Student Student { get; set; }
    }
}