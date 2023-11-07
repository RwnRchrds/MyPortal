using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using MyPortal.Database.BaseTypes;

namespace MyPortal.Database.Models.Entity
{
    [Table("IncidentTypes")]
    public class IncidentType : LookupItem
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage",
            "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public IncidentType()
        {
            Incidents = new HashSet<Incident>();
        }

        [Column(Order = 4)] public int DefaultPoints { get; set; }


        public virtual ICollection<Incident> Incidents { get; set; }

        public virtual ICollection<ReportCard> ReportCards { get; set; }
    }
}