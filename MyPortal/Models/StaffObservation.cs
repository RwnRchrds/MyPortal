using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Models
{
    [Table("Personnel_Observations")]
    public class StaffObservation
    {
        [Display(Name = "ID")] public int Id { get; set; }

        [Column(TypeName = "date")] public DateTime Date { get; set; }

        [Display(Name = "Observee")] 
        [Required]
        public int ObserveeId { get; set; }

        [Display(Name = "Observer")] 
        [Required]
        public int ObserverId { get; set; }

        [Required] [StringLength(255)] public string Outcome { get; set; }

        public virtual Staff Observee { get; set; }

        public virtual Staff Observer { get; set; }
    }
}