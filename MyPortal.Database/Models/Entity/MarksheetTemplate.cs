using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models.Entity
{
    [Table("MarksheetTemplates")]
    public class MarksheetTemplate : BaseTypes.Entity
    {
        [Column(Order = 1)] 
        public string Name { get; set; }

        [Column(Order = 2)]
        public bool Active { get; set; }

        public virtual ICollection<MarksheetTemplateGroup> TemplateGroups { get; set; }
        public virtual ICollection<MarksheetColumn> Columns { get; set; }
    }
}