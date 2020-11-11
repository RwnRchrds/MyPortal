using System;
using System.Collections.Generic;
using System.Text;
using MyPortal.Logic.Models.Data;

namespace MyPortal.Logic.Models.Entity
{
    public class MarksheetColumnModel : BaseModel
    {
        public Guid TemplateId { get; set; }
        public Guid AspectId { get; set; }
        public Guid ResultSetId { get; set; }
        public int DisplayOrder { get; set; }
        public bool ReadOnly { get; set; }

        public virtual MarksheetTemplateModel Template { get; set; }
        public virtual AspectModel Aspect { get; set; }
        public virtual ResultSetModel ResultSet { get; set; }
    }
}
