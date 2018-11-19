using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Models
{
    public class Grade
    {
        public int Id { get; set; }

        public int GradeSetId { get; set; }

        [Column("Grade")]
        [Required]
        [StringLength(255)]
        public string GradeValue { get; set; }

        public virtual GradeSet GradeSet { get; set; }
    }
}