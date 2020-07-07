using System;
using MyPortal.Logic.Models.Data;

namespace MyPortal.Logic.Models.Entity
{
    public class DetentionTypeModel : LookupItemModel
    {
        public TimeSpan StartTime { get; set; }
        
        public TimeSpan EndTime { get; set; }
    }
}