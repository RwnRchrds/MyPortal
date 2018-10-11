using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Dtos
{
    public class TrainingCertificateDto
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CourseId { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(3)]
        public int StaffId { get; set; }

        [Required] public int StatusId { get; set; }

        public TrainingCourseDto TrainingCourse { get; set; }
        public TrainingStatusDto TrainingStatus { get; set; }
    }
}