using System.ComponentModel.DataAnnotations;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Models.Structures;

namespace MyPortal.Logic.Models.Data.Attendance
{
    public class AttendanceCodeTypeModel : BaseModel
    {
        public AttendanceCodeTypeModel(AttendanceCodeType model) : base(model)
        {
            Description = model.Description;
        }

        [Required] [StringLength(256)] public string Description { get; set; }
    }
}