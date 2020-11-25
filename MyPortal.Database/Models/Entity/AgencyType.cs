using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using MyPortal.Database.BaseTypes;

namespace MyPortal.Database.Models.Entity
{
    [Table("AgencyTypes")]
    public class AgencyType : LookupItem
    {
        public AgencyType()
        {
            Agencies = new HashSet<Agency>();
        }

        public virtual ICollection<Agency> Agencies { get; set; }
    }
}
