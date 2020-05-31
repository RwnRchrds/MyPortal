using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using MyPortal.Database.BaseTypes;

namespace MyPortal.Database.Models
{
    [Table("DiaryEventType")]
    public class DiaryEventType : LookupItem
    {
        public DiaryEventType()
        {
            DiaryEventTemplates = new HashSet<DiaryEventTemplate>();
            DiaryEvents = new HashSet<DiaryEvent>();
        }

        [StringLength(128)]
        public string ColourCode { get; set; }

        public virtual  ICollection<DiaryEventTemplate> DiaryEventTemplates { get; set; }
        public virtual  ICollection<DiaryEvent> DiaryEvents { get; set; }
    }
}
