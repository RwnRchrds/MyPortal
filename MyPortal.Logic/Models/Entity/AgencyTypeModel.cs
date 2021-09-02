using System;
using System.Collections.Generic;
using System.Text;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Models.Data;

namespace MyPortal.Logic.Models.Entity
{
    public class AgencyTypeModel : LookupItemModel
    {
        public AgencyTypeModel(AgencyType model) : base(model)
        {
        }
    }
}
