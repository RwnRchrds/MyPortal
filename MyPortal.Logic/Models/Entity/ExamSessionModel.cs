using System;
using MyPortal.Logic.Models.Data;

namespace MyPortal.Logic.Models.Entity
{
    public class ExamSessionModel : LookupItemModel
    {
        public TimeSpan StartTime { get; set; }
    }
}