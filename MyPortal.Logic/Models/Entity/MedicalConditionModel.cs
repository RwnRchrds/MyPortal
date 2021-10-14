using System;
using System.Collections.Generic;
using System.Text;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Models.Data;

namespace MyPortal.Logic.Models.Entity
{
    public class MedicalConditionModel : LookupItemModel
    {
        public MedicalConditionModel(MedicalCondition model) : base(model)
        {
            
        }
    }
}
