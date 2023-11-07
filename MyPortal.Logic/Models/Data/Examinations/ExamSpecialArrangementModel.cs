using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Models.Structures;

namespace MyPortal.Logic.Models.Data.Examinations
{
    public class ExamSpecialArrangementModel : BaseModel
    {
        public ExamSpecialArrangementModel(ExamSpecialArrangement model) : base(model)
        {
            Description = model.Description;
            System = model.System;
        }

        public string Description { get; set; }
        public bool System { get; set; }
    }
}