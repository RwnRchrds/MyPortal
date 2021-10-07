using System;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Models.Data;

namespace MyPortal.Logic.Models.Entity
{
    public class ExamSessionModel : LookupItemModel
    {
        public ExamSessionModel(ExamSession model) : base(model)
        {
            StartTime = model.StartTime;
        }
        
        public TimeSpan StartTime { get; set; }
    }
}