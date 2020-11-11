using System;
using System.Collections.Generic;
using System.Text;
using MyPortal.Logic.Models.Data;

namespace MyPortal.Logic.Models.Entity
{
    public class MarksheetTemplateModel : BaseModel
    {
        public string Name { get; set; }

        public bool Active { get; set; }
    }
}
