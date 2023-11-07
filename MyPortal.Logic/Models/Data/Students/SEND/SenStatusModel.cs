using System.ComponentModel.DataAnnotations;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Models.Structures;

namespace MyPortal.Logic.Models.Data.Students.SEND
{
    public class SenStatusModel : LookupItemModel
    {
        public SenStatusModel(SenStatus model) : base(model)
        {
            Code = model.Code;
        }

        [Required] [StringLength(1)] public string Code { get; set; }
    }
}