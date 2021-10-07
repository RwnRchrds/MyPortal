using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Models.Data;

namespace MyPortal.Logic.Models.Entity
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
