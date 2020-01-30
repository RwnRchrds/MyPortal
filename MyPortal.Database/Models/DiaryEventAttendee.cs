using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MyPortal.Database.Models
{
    [Table("DiaryEventAttendee")]
    public class DiaryEventAttendee
    {
        public int EventId { get; set; }
        public int PersonId { get; set; }
        public int ResponseId { get; set; }
        public bool Required { get; set; }
        public bool Attended { get; set; }

        public virtual DiaryEvent Event { get; set; }
        public virtual Person Person { get; set; }
        public virtual DiaryEventInvitationResponse Response { get; set; }
    }
}
