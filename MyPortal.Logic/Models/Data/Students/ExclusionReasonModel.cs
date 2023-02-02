using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Models.Structures;

namespace MyPortal.Logic.Models.Data.Students
{
    public class ExclusionReasonModel : LookupItemModel
    {
        public ExclusionReasonModel(ExclusionReason model) : base(model)
        {
            Code = model.Code;
            System = model.System;
        }
        
        public string Code { get; set; }

        public bool System { get; set; }
    }
}
