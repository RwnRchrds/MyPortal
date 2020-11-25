using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MyPortal.Database.BaseTypes;

namespace MyPortal.Database.Models.Entity
{
    [Table("ObservationOutcomes")]
    public class ObservationOutcome : LookupItem
    {
        public ObservationOutcome()
        {
            Observations = new HashSet<Observation>();
        }

        [Column(Order = 3)]
        [StringLength(128)]
        public string ColourCode { get; set; }

        public virtual ICollection<Observation> Observations { get; set; }
    }
}
