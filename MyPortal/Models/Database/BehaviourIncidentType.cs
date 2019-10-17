using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyPortal.Models.Database
{
    /// <summary>
    /// Category of behaviour incident.
    /// </summary>
    [Table("Behaviour_IncidentTypes")]
    public class BehaviourIncidentType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BehaviourIncidentType()
        {
            Incidents = new HashSet<BehaviourIncident>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(256)]
        public string Description { get; set; }

        public bool System { get; set; }

        public int DefaultPoints { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BehaviourIncident> Incidents { get; set; }
    }
}