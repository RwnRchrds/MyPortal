using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models.Entity
{
    [Table("DiaryEvents")]
    public class DiaryEvent : BaseTypes.Entity
    {
        public DiaryEvent()
        {
            Attendees = new HashSet<DiaryEventAttendee>();
        }

        [Column(Order = 1)]
        public Guid EventTypeId { get; set; }

        [Column(Order = 2)]
        public Guid? RoomId { get; set; }

        [Column(Order = 3)]
        [Required]
        [StringLength(256)]
        public string Subject { get; set; }

        [Column(Order = 4)]
        [StringLength(256)]
        public string Description { get; set; }

        [Column(Order = 5)]
        [StringLength(256)]
        public string Location { get; set; }

        [Column(Order = 6)]
        public DateTime StartTime { get; set; }

        [Column(Order = 7)]
        public DateTime EndTime { get; set; }

        [Column(Order = 8)]
        public bool IsAllDay { get; set; }

        [Column(Order = 9)]
        public bool IsPublic { get; set; }
        
        
        public virtual DiaryEventType EventType { get; set; }

        public virtual Room Room { get; set; }
        public virtual ICollection<Detention> Detentions { get; set; }
        public virtual ICollection<ActivityEvent> ActivityEvents { get; set; }
        public virtual ICollection<ParentEvening> ParentEvenings { get; set; }
        public virtual ICollection<DiaryEventAttendee> Attendees { get; set; }
    }
}
