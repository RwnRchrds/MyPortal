using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using MyPortal.Database.BaseTypes;

namespace MyPortal.Database.Models
{
    [Table("ActivityEvent")]
    public class ActivityEvent : Entity
    {
        [Column(Order = 1)]
        public Guid ActivityId { get; set; }

        [Column(Order = 2)]
        public Guid EventId { get; set; }

        public Activity Activity { get; set; }
        public DiaryEvent Event { get; set; }
    }
}
