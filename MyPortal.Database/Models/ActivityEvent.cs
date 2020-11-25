using System;
using System.Collections.Generic;
using System.Text;
using MyPortal.Database.BaseTypes;

namespace MyPortal.Database.Models
{
    public class ActivityEvent : Entity
    {
        public Guid ActivityId { get; set; }
        public Guid EventId { get; set; }

        public Activity Activity { get; set; }
        public DiaryEvent Event { get; set; }
    }
}
