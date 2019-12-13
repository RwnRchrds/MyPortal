using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Data.Models
{
    /// <summary>
    /// Grade assigned to results.
    /// </summary>
    [Table("Grade", Schema = "assessment")]
    public class Grade
    {
        public int Id { get; set; }

        public int GradeSetId { get; set; }

        [Required]
        [StringLength(128)]
        public string Code { get; set; }

        public int Value { get; set; }

        public virtual GradeSet GradeSet { get; set; }
    }
}
