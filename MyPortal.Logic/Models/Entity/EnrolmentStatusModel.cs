using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Models.Data;

namespace MyPortal.Logic.Models.Entity
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