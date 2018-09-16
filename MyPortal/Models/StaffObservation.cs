using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Models
{
    public class StaffObservation
    {
        public int Id { get; set; }

        [Column(TypeName = "date")] 
        public DateTime Date { get; set; }

        [Required] 
        [StringLength(3)] 
        public string Observee { get; set; }

        [Required] 
        [StringLength(3)] 
        public string Observer { get; set; }

        [Required] 
        [StringLength(255)] 
        public string Outcome { get; set; }

        public virtual Staff Staff { get; set; }

        public virtual Staff Staff1 { get; set; }
    }
}