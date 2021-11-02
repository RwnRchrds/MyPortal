using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Models.Data;

namespace MyPortal.Logic.Models.Entity
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