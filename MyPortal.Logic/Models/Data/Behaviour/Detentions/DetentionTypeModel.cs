using System;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Models.Structures;

namespace MyPortal.Logic.Models.Data.Behaviour.Detentions
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