using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using MyPortal.Database.BaseTypes;

namespace MyPortal.Database.Models
{
    [Table("DiaryEventTemplate")]
    public class DiaryEventTemplate : LookupItem
    { 
        public Guid EventTypeId { get; set; }
        public int Minutes { get; set; }
        public int Hours { get; set; }
        public int Days { get; set; }

        public virtual DiaryEventType DiaryEventType { get; set; }
    }
}
