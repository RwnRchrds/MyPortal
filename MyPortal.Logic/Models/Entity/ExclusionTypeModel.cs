using System;
using System.Collections.Generic;
using System.Text;
using MyPortal.Logic.Models.Data;

namespace MyPortal.Logic.Models.Entity
{
    public class ExclusionTypeModel : LookupItemModel
    {
        public string Code { get; set; }
        public bool System { get; set; }
    }
}
