using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace MyPortal.Database.Models
{
    [Table("Grade")]
    public class Grade
    {
        public Grade()
        {
            Results = new HashSet<Result>();
        }

        [DataMember]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [DataMember]
        public Guid GradeSetId { get; set; }

        [DataMember]
        [Required]
        [StringLength(128)]
        public string Code { get; set; }

        [DataMember]
        public int Value { get; set; }

        [DataMember]
        public bool System { get; set; }

        public virtual GradeSet GradeSet { get; set; }

        public virtual ICollection<Result> Results { get; set; }
    }
}
