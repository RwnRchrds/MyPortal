using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Models.Structures;

namespace MyPortal.Logic.Models.Data.Students
{
    public class EnrolmentStatusModel : LookupItemModel
    {
        public EnrolmentStatusModel(EnrolmentStatus model) : base(model)
        {
            Code = model.Code;
        }
        
        public string Code { get; set; }
    }
}