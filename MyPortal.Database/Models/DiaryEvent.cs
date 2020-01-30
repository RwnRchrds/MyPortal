using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models
{
    [Table("DiaryEvent")]
    public class DiaryEvent
    {
        public DiaryEvent()
        {
            Attendees = new HashSet<DiaryEventAttendee>();
        }

        public int Id { get; set; }

        public int EventTypeId { get; set; }

        [Required]
        [StringLength(256)]
        public string Subject { get; set; }

        [StringLength(256)]
        public string Description { get; set; }

        [StringLength(256)]
        public string Location { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public bool IsAllDay { get; set; }

        public bool IsBlock { get; set; }

        public bool IsPublic { get; set; }

        public bool IsStudentVisible { get; set; }

        public virtual Detention Detention { get; set; }
        public virtual DiaryEventType EventType { get; set; }
        public virtual ICollection<DiaryEventAttendee> Attendees { get; set; }
    }
}
