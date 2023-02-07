using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MyPortal.Database.Interfaces;

namespace MyPortal.Database.Models.Entity
{
    [Table("DiaryEvents")]
    public class DiaryEvent : BaseTypes.Entity, ICreatableSystemEntity
    {
        public DiaryEvent()
        {
            Attendees = new HashSet<DiaryEventAttendee>();
        }

        [Column(Order = 1)]
        public Guid EventTypeId { get; set; }
        
        [Column(Order = 2)] 
        public Guid? CreatedById { get; set; }
        
        [Column(Order = 3)] 
        public DateTime CreatedDate { get; set; }

        [Column(Order = 4)]
        public Guid? RoomId { get; set; }

        [Column(Order = 5)]
        [Required]
        [StringLength(256)]
        public string Subject { get; set; }

        [Column(Order = 6)]
        [StringLength(256)]
        public string Description { get; set; }

        [Column(Order = 7)]
        [StringLength(256)]
        public string Location { get; set; }

        [Column(Order = 8)]
        public DateTime StartTime { get; set; }

        [Column(Order = 9)]
        public DateTime EndTime { get; set; }

        [Column(Order = 10)]
        public bool AllDay { get; set; }

        /// <summary>
        /// Public events are visible to all users on the school diary
        /// </summary>
        [Column(Order = 11)]
        public bool Public { get; set; }

        [Column(Order = 12)]
        public bool System { get; set; }
        
        
        public virtual DiaryEventType EventType { get; set; }
        public virtual User CreatedBy { get; set; }
        public virtual Room Room { get; set; }
        public virtual ICollection<Detention> Detentions { get; set; }
        public virtual ICollection<ParentEvening> ParentEvenings { get; set; }
        public virtual ICollection<DiaryEventAttendee> Attendees { get; set; }
    }
}
