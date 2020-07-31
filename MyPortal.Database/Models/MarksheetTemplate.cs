using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using MyPortal.Database.BaseTypes;
using MyPortal.Database.Interfaces;

namespace MyPortal.Database.Models
{
    [Table("MarksheetTemplates")]
    public class MarksheetTemplate : Entity
    {
        [Column(Order = 1)] 
        public string Name { get; set; }

        [Column(Order = 2)]
        public bool Active { get; set; }

        public virtual ICollection<MarksheetTemplateGroup> TemplateGroups { get; set; }
        public virtual ICollection<MarksheetColumn> Columns { get; set; }
    }
}