using System;
using System.Collections.Generic;
using System.Text;
using MyPortal.Logic.Models.Data;

namespace MyPortal.Logic.Models.Entity
{
    public class MarksheetTemplateGroupModel : BaseModel
    {
        public Guid MarksheetTemplateId { get; set; }

        public Guid StudentGroupId { get; set; }

        public virtual MarksheetTemplateModel Template { get; set; }
        public virtual StudentGroupModel StudentGroup { get; set; }
    }
}
