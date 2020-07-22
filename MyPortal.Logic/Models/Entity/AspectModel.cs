using System;
using System.ComponentModel.DataAnnotations;

namespace MyPortal.Logic.Models.Entity
{
    public class AspectModel
    {
        public Guid Id { get; set; }

        public Guid TypeId { get; set; }

        public Guid? GradeSetId { get; set; }

        public decimal? MaxMark { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(128)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        [StringLength(256)]
        public string Description { get; set; }

        public virtual AspectTypeModel Type { get; set; }

        public virtual GradeSetModel GradeSet { get; set; }

        public bool Active { get; set; }
    }
}
