﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MyPortal.Database.Interfaces;

namespace MyPortal.Database.Models.Entity
{
    [Table("ReportCards")]
    public class ReportCard : BaseTypes.Entity, IActivatable
    {
        [Column(Order = 2)] public Guid StudentId { get; set; }

        [Column(Order = 3)] public Guid BehaviourTypeId { get; set; }

        [Column(Order = 4, TypeName = "date")] public DateTime StartDate { get; set; }

        [Column(Order = 5, TypeName = "date")] public DateTime EndDate { get; set; }

        [Column(Order = 6)]
        [StringLength(256)]
        public string Comments { get; set; }

        [Column(Order = 7)] public bool Active { get; set; }

        public virtual Student Student { get; set; }
        public virtual IncidentType BehaviourType { get; set; }
        public virtual ICollection<ReportCardTarget> Targets { get; set; }
        public virtual ICollection<ReportCardEntry> Submissions { get; set; }
    }
}