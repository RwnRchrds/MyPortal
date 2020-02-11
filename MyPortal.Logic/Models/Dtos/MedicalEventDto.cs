using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyPortal.Logic.Models.Dtos
{
    public class MedicalEventDto
    {
        public Guid Id { get; set; }

        public Guid StudentId { get; set; }

        public Guid RecordedById { get; set; }

        public DateTime Date { get; set; }

        [Required]
        public string Note { get; set; }

        public virtual StaffMemberDto RecordedBy { get; set; }

        public virtual StudentDto Student { get; set; }
    }
}
