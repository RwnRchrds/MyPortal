using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text;
using MyPortal.Database.BaseTypes;

namespace MyPortal.Database.Models
{
    [Table("DiaryEventTemplate")]
    public class DiaryEventTemplate : LookupItem
    {
        [DataMember]
        public Guid EventTypeId { get; set; }

        [DataMember]
        public int Minutes { get; set; }

        [DataMember]
        public int Hours { get; set; }

        [DataMember]
        public int Days { get; set; }

        public virtual DiaryEventType DiaryEventType { get; set; }
    }
}
