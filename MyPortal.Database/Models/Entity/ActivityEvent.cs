using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models.Entity
{
    [Table("ActivityEvent")]
    public class ActivityEvent : BaseTypes.Entity
    {
        [Column(Order = 1)]
        public Guid ActivityId { get; set; }

        [Column(Order = 2)]
        public Guid EventId { get; set; }

        public Activity Activity { get; set; }
        public DiaryEvent Event { get; set; }
    }
}
