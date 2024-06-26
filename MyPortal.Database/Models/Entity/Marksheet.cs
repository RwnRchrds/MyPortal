﻿using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models.Entity
{
    [Table("Marksheets")]
    public class Marksheet : BaseTypes.Entity
    {
        [Column(Order = 2)] public Guid MarksheetTemplateId { get; set; }

        [Column(Order = 3)] public Guid StudentGroupId { get; set; }

        [Column(Order = 4)] public bool Completed { get; set; }

        public virtual MarksheetTemplate Template { get; set; }
        public virtual StudentGroup StudentGroup { get; set; }
    }
}