using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Dtos
{
    public class TrainingCertificateDto
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Course { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(3)]
        public string Staff { get; set; }

        [Required] public int Status { get; set; }

        public TrainingCourseDto TrainingCourse { get; set; }
    }
}