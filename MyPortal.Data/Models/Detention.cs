using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortal.Data.Models
{
    [Table("Detention")]
    public class Detention
    {
        public Detention()
        {
            Incidents = new HashSet<IncidentDetention>();
        }

        public int Id { get; set; }
        public int DetentionTypeId { get; set; }
        public int EventId { get; set; }
        public int? SupervisorId { get; set; }

        public virtual DetentionType Type { get; set; }
        public virtual DiaryEvent Event { get; set; }
        public virtual StaffMember Supervisor { get; set; }
        public virtual ICollection<IncidentDetention> Incidents { get; set; }
    }
}
