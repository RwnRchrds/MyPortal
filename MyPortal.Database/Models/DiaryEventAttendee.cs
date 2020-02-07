using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MyPortal.Database.Models
{
    [Table("DiaryEventAttendee")]
    public class DiaryEventAttendee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public Guid EventId { get; set; }
        public Guid PersonId { get; set; }
        public Guid? ResponseId { get; set; }
        public bool Required { get; set; }
        public bool Attended { get; set; }

        public virtual DiaryEvent Event { get; set; }
        public virtual Person Person { get; set; }
        public virtual DiaryEventAttendeeResponse Response { get; set; }
    }
}
