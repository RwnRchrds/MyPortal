using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MyPortal.Models.Validation;

namespace MyPortal.Models
{
    [Table("Assessment_Results")]
    public class Result
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Result Set")]
        public int ResultSetId { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Student")]
        public int StudentId { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Subject")]
        public int SubjectId { get; set; }

        [Required] 
        [StringLength(50)]
//        [ActiveGradeSet]
        public string Value { get; set; }

        public virtual ResultSet ResultSet { get; set; }

        public virtual Student Student { get; set; }

        public virtual Subject Subject { get; set; }
    }
}