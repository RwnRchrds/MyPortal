using System;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Models.Data;

namespace MyPortal.Logic.Models.Entity
{
    public class DetentionTypeModel : LookupItemModel
    {
        public TimeSpan StartTime { get; set; }
        
        public TimeSpan EndTime { get; set; }

        public DetentionTypeModel(DetentionType model) : base(model)
        {
            StartTime = model.StartTime;
            EndTime = model.EndTime;
        }
    }
}