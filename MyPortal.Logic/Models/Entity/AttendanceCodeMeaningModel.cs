using System;
using System.ComponentModel.DataAnnotations;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Models.Data;

namespace MyPortal.Logic.Models.Entity
{
    public class AttendanceCodeMeaningModel : BaseModel
    {
        public AttendanceCodeMeaningModel(AttendanceCodeMeaning model) : base(model)
        {
            Description = model.Description;
        }

        [Required]
        [StringLength(256)]
        public string Description { get; set; }
    }
}
