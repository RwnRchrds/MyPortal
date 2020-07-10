using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using MyPortal.Database.Interfaces;

namespace MyPortal.Database.Models
{
    [Table("Grade")]
    public class Grade : IEntity
    {
        public Grade()
        {
            Results = new HashSet<Result>();
        }

        [Column(Order = 0)]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Column(Order = 1)]
        public Guid GradeSetId { get; set; }

        [Column(Order = 2)]
        [Required]
        [StringLength(128)]
        public string Code { get; set; }

        [Column(Order = 3)]
        public int Value { get; set; }

        [Column(Order = 4)]
        public bool System { get; set; }

        public virtual GradeSet GradeSet { get; set; }

        public virtual ICollection<Result> Results { get; set; }
    }
}
