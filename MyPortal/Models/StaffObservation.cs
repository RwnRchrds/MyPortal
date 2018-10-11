namespace MyPortal.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class StaffObservation
    {
        [Display(Name = "ID")]
        public int Id { get; set; }

        [Column(TypeName = "date")]
        public DateTime Date { get; set; }

        [Display(Name = "Observee")]
        public int ObserveeId { get; set; }

        [Display(Name = "Observer")]
        public int ObserverId { get; set; }

        [Required]
        [StringLength(255)]
        public string Outcome { get; set; }

        public virtual Staff Staff { get; set; }

        public virtual Staff Staff1 { get; set; }
    }
}
