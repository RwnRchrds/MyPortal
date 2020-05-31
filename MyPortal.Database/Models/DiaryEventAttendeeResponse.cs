using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using MyPortal.Database.BaseTypes;

namespace MyPortal.Database.Models
{
    [Table("DiaryEventAttendeeResponse")]
    public class DiaryEventAttendeeResponse : LookupItem
    {
        public DiaryEventAttendeeResponse()
        {
            DiaryEventAttendees = new HashSet<DiaryEventAttendee>();
        }

        public virtual ICollection<DiaryEventAttendee> DiaryEventAttendees { get; set; }
    }
}
