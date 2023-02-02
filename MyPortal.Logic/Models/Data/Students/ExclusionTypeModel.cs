using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Models.Structures;

namespace MyPortal.Logic.Models.Data.Students
{
    public class ExclusionTypeModel : LookupItemModel
    {
        public ExclusionTypeModel(ExclusionType model) : base(model)
        {
            Code = model.Code;
            System = model.System;
        }
        
        public string Code { get; set; }
        public bool System { get; set; }
    }
}
