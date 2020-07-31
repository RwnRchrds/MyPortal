﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using MyPortal.Database.BaseTypes;
using MyPortal.Database.Interfaces;

namespace MyPortal.Database.Models
{
    [Table("Detentions")]
    public class Detention : Entity
    {
        public Detention()
        {
            Incidents = new HashSet<IncidentDetention>();
        }

        [Column(Order = 1)]
        public Guid DetentionTypeId { get; set; }

        [Column(Order = 2)]
        public Guid EventId { get; set; }

        [Column(Order = 3)]
        public Guid? SupervisorId { get; set; }

        public virtual DetentionType Type { get; set; }
        public virtual DiaryEvent Event { get; set; }
        public virtual StaffMember Supervisor { get; set; }
        public virtual ICollection<IncidentDetention> Incidents { get; set; }
    }
}
