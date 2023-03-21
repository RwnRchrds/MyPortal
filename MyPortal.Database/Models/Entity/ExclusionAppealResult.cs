using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using MyPortal.Database.BaseTypes;
using MyPortal.Database.Interfaces;

namespace MyPortal.Database.Models.Entity
{
    public class ExclusionAppealResult : LookupItem, ISystemEntity
    {
        public ExclusionAppealResult()
        {
            Exclusions = new HashSet<Exclusion>();
        }

        [Column(Order = 4)]
        public bool System { get; set; }

        public virtual ICollection<Exclusion> Exclusions { get; set; }
    }
}
