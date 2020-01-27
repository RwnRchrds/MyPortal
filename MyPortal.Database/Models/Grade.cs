using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models
{
    [Table("Grade")]
    public class Grade
    {
        public Grade()
        {
            Results = new HashSet<Result>();
        }

        public int Id { get; set; }

        public int GradeSetId { get; set; }

        [Required]
        [StringLength(128)]
        public string Code { get; set; }

        public int Value { get; set; }

        public bool System { get; set; }

        public virtual GradeSet GradeSet { get; set; }

        public virtual ICollection<Result> Results { get; set; }
    }
}
