using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using MyPortal.Logic.Models.Data;

namespace MyPortal.Logic.Models.Entity
{
    public class MedicalEventModel : BaseModel
    {
        public Guid StudentId { get; set; }

        public Guid RecordedById { get; set; }

        public DateTime Date { get; set; }

        [Required]
        public string Note { get; set; }

        public virtual UserModel RecordedBy { get; set; }

        public virtual StudentModel Student { get; set; }
    }
}
