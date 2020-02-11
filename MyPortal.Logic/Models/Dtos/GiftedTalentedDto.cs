using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyPortal.Logic.Models.Dtos
{
    public class GiftedTalentedDto
    {
        public Guid Id { get; set; }
        public Guid StudentId { get; set; }
        public Guid SubjectId { get; set; }

        [Required]
        public string Notes { get; set; }

        public virtual StudentDto Student { get; set; }
        public virtual SubjectDto Subject { get; set; }
    }
}
