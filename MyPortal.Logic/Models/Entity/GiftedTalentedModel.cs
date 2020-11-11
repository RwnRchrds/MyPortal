using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using MyPortal.Logic.Models.Data;

namespace MyPortal.Logic.Models.Entity
{
    public class GiftedTalentedModel : BaseModel
    {
        public Guid StudentId { get; set; }
        public Guid SubjectId { get; set; }

        [Required]
        public string Notes { get; set; }

        public virtual StudentModel Student { get; set; }
        public virtual SubjectModel Subject { get; set; }
    }
}
