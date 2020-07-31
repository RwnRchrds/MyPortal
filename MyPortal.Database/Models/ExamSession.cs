using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using MyPortal.Database.BaseTypes;

namespace MyPortal.Database.Models
{
    [Table("ExamSessions")]
    public class ExamSession : LookupItem
    {
        // TODO: Populate Data

        [Column(Order = 3)]
        public TimeSpan StartTime { get; set; }

        public virtual ICollection<ExamComponent> Components { get; set; }
    }
}
