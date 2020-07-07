using System;
using System.ComponentModel.DataAnnotations;

namespace MyPortal.Logic.Models.Entity
{
    public class GradeModel
    {
        public Guid Id { get; set; }

        public Guid GradeSetId { get; set; }

        [Required]
        [StringLength(128)]
        public string Code { get; set; }

        public int Value { get; set; }

        public bool System { get; set; }

        public virtual GradeSetModel GradeSet { get; set; }
    }
}
