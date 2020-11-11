using System;
using MyPortal.Logic.Models.Data;

namespace MyPortal.Logic.Models.Entity
{
    public class DiaryEventTemplateModel : LookupItemModel
    {
        public Guid EventTypeId { get; set; }
        
        public int Minutes { get; set; }
        
        public int Hours { get; set; }
        
        public int Days { get; set; }
        
        public virtual DiaryEventTypeModel DiaryEventType { get; set; }
    }
}