﻿using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models.Entity
{
    [Table("CommunicationLogs")]
    public class CommunicationLog : BaseTypes.Entity
    {
        [Column(Order = 1)]
        public Guid ContactId { get; set; }

        [Column(Order = 2)]
        public Guid CommunicationTypeId { get; set; }

        [Column(Order = 3)]
        public DateTime Date { get; set; }

        [Column(Order = 4)]
        public string Note { get; set; }

        [Column(Order = 5)] 
        public bool Outgoing { get; set; }

        public virtual Contact Contact { get; set; }
        public virtual CommunicationType Type { get; set; }
    }
}