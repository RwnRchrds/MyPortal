using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MyPortal.Database.Interfaces;

namespace MyPortal.Database.Models.Entity
{
    [Table("Activities")]
    public class Activity : BaseTypes.Entity
    {
        public Guid StudentGroupId { get; set; }

        public virtual StudentGroup StudentGroup { get; set; }
        public virtual ICollection<ActivityEvent> Events { get; set; }
    }
}
