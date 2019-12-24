using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortal.Data.Models
{
    [Table("IncidentDetention", Schema = "behaviour")]
    public class IncidentDetention
    {
        public int Id { get; set; }
        public int IncidentId { get; set; }
        public int DetentionId { get; set; }

        public virtual Incident Incident { get; set; }
        public virtual Detention Detention { get; set; }
    }
}
