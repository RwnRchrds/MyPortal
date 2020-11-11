using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text;
using MyPortal.Database.BaseTypes;

namespace MyPortal.Database.Models
{
    [Table("DiaryEventTemplates")]
    public class DiaryEventTemplate : LookupItem
    {
        [Column(Order = 3)]
        public Guid EventTypeId { get; set; }

        [Column(Order = 4)]
        public int Minutes { get; set; }

        [Column(Order = 5)]
        public int Hours { get; set; }

        [Column(Order = 6)]
        public int Days { get; set; }

        public virtual DiaryEventType DiaryEventType { get; set; }
    }
}
