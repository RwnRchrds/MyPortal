using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyPortal.Logic.Models.Business
{
    public class AspectModel
    {
        public Guid Id { get; set; }

        public Guid TypeId { get; set; }

        public Guid? GradeSetId { get; set; }

        public decimal? MaxMark { get; set; }

        [Required]
        [StringLength(128)]
        public string Name { get; set; }

        [Required]
        [StringLength(256)]
        public string Description { get; set; }

        public virtual AspectTypeModel Type { get; set; }

        public virtual GradeSetModel GradeSet { get; set; }

        public bool Active { get; set; }
    }
}
