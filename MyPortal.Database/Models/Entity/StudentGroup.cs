using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models.Entity
{
    [Table("StudentGroups")]
    public class StudentGroup : BaseTypes.Entity
    {
        [Column(Order = 1)]
        public Guid GroupType { get; set; }

        [Column(Order = 2)]
        public Guid BaseGroupId { get; set; }

        public virtual ICollection<MarksheetTemplateGroup> MarksheetTemplates { get; set; }
    }
}
