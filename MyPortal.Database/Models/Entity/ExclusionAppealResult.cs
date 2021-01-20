using System;
using System.Collections.Generic;
using System.Text;
using MyPortal.Database.BaseTypes;

namespace MyPortal.Database.Models.Entity
{
    public class ExclusionAppealResult : LookupItem
    {
        public ExclusionAppealResult()
        {
            Exclusions = new HashSet<Exclusion>();
        }

        public virtual ICollection<Exclusion> Exclusions { get; set; }
    }
}
