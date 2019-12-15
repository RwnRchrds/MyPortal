using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortal.BusinessLogic.Dtos
{
    public class GradeDto
    {
        public int Id { get; set; }

        public int GradeSetId { get; set; }

        [Required]
        [StringLength(128)]
        public string Code { get; set; }

        public int Value { get; set; }

        public virtual GradeSetDto GradeSet { get; set; }
    }
}
