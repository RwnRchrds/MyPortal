using System.ComponentModel.DataAnnotations;
using MyPortal.Database.BaseTypes;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Models.Data;

namespace MyPortal.Logic.Models.Entity
{
    public class TrainingCourseModel : LookupItemModel
    {
        public TrainingCourseModel(TrainingCourse model) : base(model)
        {
            Code = model.Code;
            Name = model.Name;
        }
        
        [Required]
        [StringLength(128)]
        public string Code { get; set; }
        
        [Required]
        [StringLength(128)]
        public string Name { get; set; }
    }
}