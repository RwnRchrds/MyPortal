using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyPortal.Logic.Models.Dtos
{
    public class AspectDto
    {
        public Guid Id { get; set; }

        public Guid TypeId { get; set; }

        public Guid GradeSetId { get; set; }

        [Required]
        [StringLength(256)]
        public string Description { get; set; }

        public virtual AspectTypeDto Type { get; set; }

        public virtual GradeSetDto GradeSet { get; set; }

        public bool Active { get; set; }
    }
}
