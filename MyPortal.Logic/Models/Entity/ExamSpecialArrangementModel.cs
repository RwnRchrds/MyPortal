using MyPortal.Logic.Models.Data;

namespace MyPortal.Logic.Models.Entity
{
    public class ExamSpecialArrangementModel : BaseModel
    {
        public string Description { get; set; }
        
        public bool ExtraTime { get; set; }
    }
}