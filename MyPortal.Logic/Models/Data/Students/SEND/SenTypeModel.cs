using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Models.Structures;

namespace MyPortal.Logic.Models.Data.Students.SEND
{
    public class SenTypeModel : LookupItemModel
    {
        public SenTypeModel(SenType model) : base(model)
        {
            Code = model.Code;
        }
        
        public string Code { get; set; }
    }
}