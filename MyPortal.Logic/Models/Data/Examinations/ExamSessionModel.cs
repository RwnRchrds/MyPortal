using System;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Models.Structures;

namespace MyPortal.Logic.Models.Data.Examinations
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