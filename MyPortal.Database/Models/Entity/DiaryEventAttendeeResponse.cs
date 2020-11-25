using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using MyPortal.Database.BaseTypes;

namespace MyPortal.Database.Models.Entity
{
    [Table("DiaryEventAttendeeResponses")]
    public class DiaryEventAttendeeResponse : LookupItem
    {
        public DiaryEventAttendeeResponse()
        {
            DiaryEventAttendees = new HashSet<DiaryEventAttendee>();
        }

        public virtual ICollection<DiaryEventAttendee> DiaryEventAttendees { get; set; }
    }
}
