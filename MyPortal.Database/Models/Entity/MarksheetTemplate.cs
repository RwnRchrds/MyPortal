using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using MyPortal.Database.Interfaces;

namespace MyPortal.Database.Models.Entity
{
    [Table("MarksheetTemplates")]
    public class MarksheetTemplate : BaseTypes.Entity, IActivatable
    {
        [Column(Order = 2)] 
        public string Name { get; set; }

        [Column(Order = 3)]
        public string Notes { get; set; }

        [Column(Order = 4)]
        public bool Active { get; set; }

        public virtual ICollection<Marksheet> Marksheets { get; set; }
        public virtual ICollection<MarksheetColumn> Columns { get; set; }
    }
}