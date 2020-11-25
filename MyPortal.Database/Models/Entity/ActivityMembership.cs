using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models.Entity
{
    [Table("ActivityMemberships")]
    public class ActivityMembership : BaseTypes.Entity
    {
        [Column(Order = 1)]
        public Guid ActivityId { get; set; }

        [Column(Order = 2)]
        public Guid StudentId { get; set; }

        public virtual Activity Activity { get; set; }
        public virtual Student Student { get; set; }
    }
}
