using System.ComponentModel.DataAnnotations;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Models.Structures;

namespace MyPortal.Logic.Models.Data.School
{
    public class IntakeTypeModel : LookupItemModel
    {
        public IntakeTypeModel(IntakeType model) : base(model)
        {
            Code = model.Code;
        }
        
        [Required]
        [StringLength(10)]
        public string Code { get; set; }
    }
}
