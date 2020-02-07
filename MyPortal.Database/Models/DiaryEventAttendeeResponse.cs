using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MyPortal.Database.Models
{
    [Table("DiaryEventAttendeeResponse")]
    public class DiaryEventAttendeeResponse
    {
        public DiaryEventAttendeeResponse()
        {
            DiaryEventAttendees = new HashSet<DiaryEventAttendee>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        [StringLength(256)]
        public string Description { get; set; }

        public virtual ICollection<DiaryEventAttendee> DiaryEventAttendees { get; set; }
    }
}
