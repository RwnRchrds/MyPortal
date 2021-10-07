using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Models.Data;

namespace MyPortal.Logic.Models.Entity
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