using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models.Entity
{
    [Table("Grades")]
    public class Grade : BaseTypes.Entity
    {
        public Grade()
        {
            Results = new HashSet<Result>();
        }

        [Column(Order = 2)]
        public Guid GradeSetId { get; set; }

        [Column(Order = 3)]
        [Required]
        [StringLength(25)]
        public string Code { get; set; }

        [Column(Order = 4)]
        [StringLength(50)]
        public string Description { get; set; }

        [Column(Order = 5, TypeName = "decimal(10,2)")]
        public decimal Value { get; set; }

        public virtual GradeSet GradeSet { get; set; }

        public virtual ICollection<Result> Results { get; set; }
    }
}
