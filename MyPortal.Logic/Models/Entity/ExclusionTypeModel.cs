using System;
using System.Collections.Generic;
using System.Text;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Models.Data;

namespace MyPortal.Logic.Models.Entity
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
