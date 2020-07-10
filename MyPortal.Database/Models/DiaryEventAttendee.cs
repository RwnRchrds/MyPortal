using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text;
using MyPortal.Database.Interfaces;

namespace MyPortal.Database.Models
{
    [Table("DiaryEventAttendee")]
    public class DiaryEventAttendee : IEntity
    {
        [Column(Order = 0)]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Column(Order = 1)]
        public Guid EventId { get; set; }

        [Column(Order = 2)]
        public Guid PersonId { get; set; }

        [Column(Order = 3)]
        public Guid? ResponseId { get; set; }

        [Column(Order = 4)]
        public bool Required { get; set; }

        [Column(Order = 5)]
        public bool Attended { get; set; }

        public virtual DiaryEvent Event { get; set; }
        public virtual Person Person { get; set; }
        public virtual DiaryEventAttendeeResponse Response { get; set; }
    }
}
