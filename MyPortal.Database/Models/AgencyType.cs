using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using MyPortal.Database.BaseTypes;

namespace MyPortal.Database.Models
{
    [Table("AgencyTypes")]
    public class AgencyType : LookupItem
    {
        // TODO: Populate Data

        public AgencyType()
        {
            Agencies = new HashSet<Agency>();
        }

        public virtual ICollection<Agency> Agencies { get; set; }
    }
}
