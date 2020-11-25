using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using MyPortal.Database.BaseTypes;

namespace MyPortal.Database.Models.Entity
{
    [Table("DetentionTypes")]
    public class DetentionType : LookupItem
    {
        public DetentionType()
        {
            Detentions = new HashSet<Detention>();
        }

        [Column(Order = 3, TypeName = "time(2)")]
        public TimeSpan StartTime { get; set; }

        [Column(Order = 4, TypeName = "time(2)")]
        public TimeSpan EndTime { get; set; }

        public virtual ICollection<Detention> Detentions { get; set; }
    }
}
