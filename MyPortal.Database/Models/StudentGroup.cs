using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using MyPortal.Database.BaseTypes;

namespace MyPortal.Database.Models
{
    [Table("StudentGroups")]
    public class StudentGroup : Entity
    {
        [Column(Order = 1)]
        public Guid GroupType { get; set; }

        [Column(Order = 2)]
        public Guid GroupId { get; set; }

        public virtual ICollection<MarksheetTemplateGroup> MarksheetTemplates { get; set; }
    }
}
