﻿using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models.Entity
{
    [Table("MarksheetTemplateGroups")]
    public class MarksheetTemplateGroup : BaseTypes.Entity
    {
        [Column(Order = 1)]
        public Guid MarksheetTemplateId { get; set; }

        [Column(Order = 2)]
        public Guid GroupTypeId { get; set; }

        [Column(Order = 3)] 
        public Guid GroupId { get; set; }

        public virtual MarksheetTemplate Template { get; set; }
    }
}
