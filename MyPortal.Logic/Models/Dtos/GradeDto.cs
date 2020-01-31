using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyPortal.Logic.Models.Dtos
{
    public class GradeDto
    {
        public int Id { get; set; }

        public int GradeSetId { get; set; }

        [Required]
        [StringLength(128)]
        public string Code { get; set; }

        public int Value { get; set; }

        public bool System { get; set; }

        public virtual GradeSetDto GradeSet { get; set; }
    }
}
