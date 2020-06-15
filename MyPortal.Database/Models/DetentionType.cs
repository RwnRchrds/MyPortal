﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using MyPortal.Database.BaseTypes;

namespace MyPortal.Database.Models
{
    [Table("DetentionType")]
    public class DetentionType : LookupItem
    {
        public DetentionType()
        {
            Detentions = new HashSet<Detention>();
        }

        [DataMember]
        [Column(TypeName = "time(2)")]
        public TimeSpan StartTime { get; set; }

        [DataMember]
        [Column(TypeName = "time(2)")]
        public TimeSpan EndTime { get; set; }

        public virtual ICollection<Detention> Detentions { get; set; }
    }
}
