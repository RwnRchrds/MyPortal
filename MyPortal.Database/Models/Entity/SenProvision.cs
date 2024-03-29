﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models.Entity
{
    [Table("SenProvisions")]
    public class SenProvision : BaseTypes.Entity
    {
        [Column(Order = 2)] public Guid StudentId { get; set; }

        [Column(Order = 3)] public Guid ProvisionTypeId { get; set; }

        [Column(Order = 4, TypeName = "date")] public DateTime StartDate { get; set; }

        [Column(Order = 5, TypeName = "date")] public DateTime EndDate { get; set; }

        [Column(Order = 6)] [Required] public string Note { get; set; }

        public virtual Student Student { get; set; }

        public virtual SenProvisionType Type { get; set; }
    }
}