using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortal.BusinessLogic.Dtos
{
    public class GiftedTalentedDto
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int SubjectId { get; set; }

        [Required]
        public string Notes { get; set; }

        public virtual StudentDto Student { get; set; }
        public virtual SubjectDto Subject { get; set; }
    }
}
