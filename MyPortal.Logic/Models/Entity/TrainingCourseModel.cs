using System.ComponentModel.DataAnnotations;
using MyPortal.Logic.Models.Data;

namespace MyPortal.Logic.Models.Entity
{
    public class TrainingCourseModel : BaseModel
    {
        [Required]
        [StringLength(128)]
        public string Code { get; set; }
        
        [Required]
        [StringLength(128)]
        public string Name { get; set; }
    }
}