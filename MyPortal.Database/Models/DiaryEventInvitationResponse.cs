using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MyPortal.Database.Models
{
    [Table("DiaryEventInvitationResponse")]
    public class DiaryEventInvitationResponse
    {
        public DiaryEventInvitationResponse()
        {
            DiaryEventAttendees = new HashSet<DiaryEventAttendee>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(256)]
        public string Description { get; set; }

        public virtual ICollection<DiaryEventAttendee> DiaryEventAttendees { get; set; }
    }
}
