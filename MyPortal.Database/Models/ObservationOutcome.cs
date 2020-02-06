using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models
{
    [Table("ObservationOutcome")]
    public class ObservationOutcome
    {
        public ObservationOutcome()
        {
            Observations = new HashSet<Observation>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        [StringLength(256)]
        public string Description { get; set; }

        [StringLength(128)]
        public string ColourCode { get; set; }

        public virtual ICollection<Observation> Observations { get; set; }
    }
}
