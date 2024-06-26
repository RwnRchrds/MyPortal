﻿using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models.Entity
{
    [Table("DiaryEventAttendees")]
    public class DiaryEventAttendee : BaseTypes.Entity
    {
        [Column(Order = 2)] public Guid EventId { get; set; }

        [Column(Order = 3)] public Guid PersonId { get; set; }

        [Column(Order = 4)] public Guid? ResponseId { get; set; }

        [Column(Order = 5)] public bool Required { get; set; }

        [Column(Order = 6)] public bool? Attended { get; set; }

        [Column(Order = 7)] public bool CanEditEvent { get; set; }

        public virtual DiaryEvent Event { get; set; }
        public virtual Person Person { get; set; }
        public virtual DiaryEventAttendeeResponse Response { get; set; }
    }
}