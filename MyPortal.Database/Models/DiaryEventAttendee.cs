using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text;

namespace MyPortal.Database.Models
{
    [Table("DiaryEventAttendee")]
    public class DiaryEventAttendee
    {
        [DataMember]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [DataMember]
        public Guid EventId { get; set; }

        [DataMember]
        public Guid PersonId { get; set; }

        [DataMember]
        public Guid? ResponseId { get; set; }

        [DataMember]
        public bool Required { get; set; }

        [DataMember]
        public bool Attended { get; set; }

        public virtual DiaryEvent Event { get; set; }
        public virtual Person Person { get; set; }
        public virtual DiaryEventAttendeeResponse Response { get; set; }
    }
}
