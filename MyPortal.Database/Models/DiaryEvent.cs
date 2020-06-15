using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace MyPortal.Database.Models
{
    [Table("DiaryEvent")]
    public class DiaryEvent
    {
        public DiaryEvent()
        {
            Attendees = new HashSet<DiaryEventAttendee>();
        }

        [DataMember]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [DataMember]
        public Guid EventTypeId { get; set; }

        [DataMember]
        [Required]
        [StringLength(256)]
        public string Subject { get; set; }

        [DataMember]
        [StringLength(256)]
        public string Description { get; set; }

        [DataMember]
        [StringLength(256)]
        public string Location { get; set; }

        [DataMember]
        public DateTime StartTime { get; set; }

        [DataMember]
        public DateTime EndTime { get; set; }

        [DataMember]
        public bool IsAllDay { get; set; }

        [DataMember]
        public bool IsBlock { get; set; }

        [DataMember]
        public bool IsPublic { get; set; }

        [DataMember]
        public bool IsStudentVisible { get; set; }

        public virtual Detention Detention { get; set; }
        public virtual DiaryEventType EventType { get; set; }
        public virtual ICollection<DiaryEventAttendee> Attendees { get; set; }
    }
}
