using System;
using System.Collections.Generic;
using System.Text;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Models.Data;

namespace MyPortal.Logic.Models.Entity
{
    public class MarksheetTemplateModel : BaseModel
    {
        public MarksheetTemplateModel(MarksheetTemplate model) : base(model)
        {
            Name = model.Name;
            Active = model.Active;
            Notes = model.Notes;
        }

        public string Notes { get; set; }

        public string Name { get; set; }

        public bool Active { get; set; }
    }
}
