using System;
using System.ComponentModel.DataAnnotations;
using MyPortal.Database.BaseTypes;

namespace MyPortal.Logic.Models.Entity
{
    public class AspectModel : LookupItem
    {
        public Guid TypeId { get; set; }

        public Guid? GradeSetId { get; set; }

        public decimal? MinMark { get; set; }

        public decimal? MaxMark { get; set; }

        [Required]
        [StringLength(128)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string ColumnHeading { get; set; }

        public bool StudentVisible { get; set; }

        public virtual AspectTypeModel Type { get; set; }

        public virtual GradeSetModel GradeSet { get; set; }
    }
}
