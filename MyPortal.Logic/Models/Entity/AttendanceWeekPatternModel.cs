using System;
using System.ComponentModel.DataAnnotations;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Models.Data;

namespace MyPortal.Logic.Models.Entity
{
    public class AttendanceWeekPatternModel : BaseModel
    {
        public AttendanceWeekPatternModel(AttendanceWeekPattern model) : base(model)
        {
            Description = model.Description;
        }

        [Required]
        [StringLength(128)]
        public string Description { get; set; }
    }
}