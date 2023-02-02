using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Models.Structures;

namespace MyPortal.Logic.Models.Data.School
{
    public class GovernanceTypeModel : LookupItemModel
    {
        public GovernanceTypeModel(GovernanceType model) : base(model)
        {
            Code = model.Code;
        }
        
        public string Code { get; set; }
    }
}
