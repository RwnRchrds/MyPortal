using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using MyPortal.Database.BaseTypes;

namespace MyPortal.Database.Models
{
    [Table("MarksheetTemplateGroups")]
    public class MarksheetTemplateGroup : Entity
    {
        [Column(Order = 1)]
        public Guid MarksheetTemplateId { get; set; }

        [Column(Order = 2)]
        public Guid StudentGroupId { get; set; }

        public virtual MarksheetTemplate Template { get; set; }
        public virtual StudentGroup StudentGroup { get; set; }
    }
}
