using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MyPortal.Database.BaseTypes;
using MyPortal.Database.Interfaces;

namespace MyPortal.Database.Models.Entity
{
    [Table("DiaryEventTypes")]
    public class DiaryEventType : LookupItem, ISystemEntity
    {
        public DiaryEventType()
        {
            DiaryEventTemplates = new HashSet<DiaryEventTemplate>();
            DiaryEvents = new HashSet<DiaryEvent>();
        }

        [Column(Order = 3)]
        [StringLength(128)]
        public string ColourCode { get; set; }

        [Column(Order = 4)] 
        public bool System { get; set; }

        public virtual  ICollection<DiaryEventTemplate> DiaryEventTemplates { get; set; }
        public virtual  ICollection<DiaryEvent> DiaryEvents { get; set; }
    }
}
